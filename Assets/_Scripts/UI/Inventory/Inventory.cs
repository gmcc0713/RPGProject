using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public List<Item> inventoryItems;       //현재 내가 가지고 있는 아이템
    [SerializeField] private int itemMaxAmount = 64;
    [SerializeField] private int SlotmaxCount = 24;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int playerGold;
    [SerializeField] private PlayerQuickSlot m_playerQuickSlot;
    private void Start()
    {
        initialize();
    }
    void initialize()
    {
        inventoryItems = new List<Item>();
        inventoryUI.UpdateGoldUI(playerGold);
        SaveLoadManager.Instance.LoadInventory();
    }
    public bool BuyItem(int id, int gold)
    {
        
        if (playerGold < gold || SlotmaxCount <= inventoryItems.Count)
        {
            Debug.Log("골드 부족 이거나 인벤토리 가득 참");
            return false;
        }

        AddGold(-gold);

        GetItem(id);
        return true;
    }
    public void AddGold(int addGold)
    {
        playerGold += addGold;
        inventoryUI.UpdateGoldUI(playerGold);
        if (playerGold < 0)
        {
            playerGold = 0;
        }
    }

    public void GetItem(int itemID, int amount = 1, int upgrade = 0)    //아이템을 얻었을때 장비아이템인지 아닌지 판별
    {
        QuestManager.Instance.NotifyQuestUpdate(Quest_Type.Type_Collect, itemID, amount);
        if (ItemDataManager.Instance.CheckIsEquipmentItem(itemID))      //해당 아이템이 장비아이템이여서 겹치기가 불가능할때
        {
            AddEquipmentItem(itemID, upgrade);
            return;
        }
        else if(ItemDataManager.Instance.CheckCanUseItem(itemID))
        {
            AddPortionItem(itemID, amount);
            return;
        }
        AddCountableItem(itemID, amount);


    }
    public void AddPortionItem(int id, int amount)
    {
        Debug.Log("AddPortion");
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (!inventoryItems[i].isEquipment)
            {
                if(inventoryItems[i] is PortionItem cItem)
                {
                    if (cItem.id == id && cItem.amount < itemMaxAmount)        //해당아이템을 이미 보유하고 있다면 
                    {
                        ChangeAmount(i, amount);            //아이템 갯수 추가
                        m_playerQuickSlot.UpdateQuickSlot(i);
                        ItemAmountOverCheck(i);                 //해당아이템이 최대 갯수 넘었는지 확인
                        return;
                    }
                }
            }
        }

        if (InventoryFullCheck())
        {
            return;
        }

        PortionItem item = new PortionItem();
        item.SetItem(id, amount, inventoryUI.FindFrontEmptySlotIndex());
        inventoryItems.Add(item);
        UpdateSlot(item);



    }
    public void ItemUse(int ivSlotNum,int quickSlotNum)
    {
        PortionItem pitem = inventoryItems[ivSlotNum] as PortionItem;
        if (pitem == null)
        {
            return;
        }
        pitem.ItemUse();
        pitem.amount--;

        inventoryUI.UpdateSlotUI(pitem);
        m_playerQuickSlot.UpdateQuickSlot(ivSlotNum);

        if (pitem.amount<=0)
        {
            m_playerQuickSlot.RemoveItem(quickSlotNum);
            RemoveItem(ivSlotNum);
        }
    }
    public void AddCountableItem(int id, int amount)                    //갯수 샐수있는 아이템을 얻었을때
    {
        Debug.Log("AddCountableItem");
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (!inventoryItems[i].isEquipment)
            {
                if (inventoryItems[i] is CountableItem cItem)
                {
                    if (cItem.id == id && cItem.amount < itemMaxAmount)        //해당아이템을 이미 보유하고 있다면 
                    {
                        ChangeAmount(i, amount);            //아이템 갯수 추가
                        ItemAmountOverCheck(i);                 //해당아이템이 최대 갯수 넘었는지 확인
                        return;
                    }
                }
            }
        }

        if (InventoryFullCheck())
        {
            return;
        }

            CountableItem item = new CountableItem();
            item.SetItem(id, amount, inventoryUI.FindFrontEmptySlotIndex());
            inventoryItems.Add(item);
            UpdateSlot(item);
       


    }
    public void AddEquipmentItem(int id, int upgrade)               //장비아이템을 얻었을때
    {
        if (InventoryFullCheck())
        {
            Debug.Log("full");
            return;
        }
        EquipmentItem item = new EquipmentItem();
        item.SetItem(id, upgrade, inventoryUI.FindFrontEmptySlotIndex(), true);       //비어있는 가장 첫번쨰 위치찾기
        inventoryItems.Add(item);

        UpdateSlot(item);
    }
    public bool InventoryFullCheck()                            //인벤토리가 가득 찼을때
    {
        if (SlotmaxCount <= inventoryItems.Count)
        {
            return true;
        }
        return false;
    }
    public void FindAndRemoveItem(int itemid, int amount)              //아이템 삭제
    {
        if (amount.Equals(-1))
        {
            Debug.Log("아이템 삭제 실패");
            return;
        }
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].id == itemid)
            {
                int remain = ChangeAmount(i, -amount);
                if(remain<0)
                {
                    amount = -remain;
                }
                else
                {
                    RemoveItem(inventoryItems[i].slotNum);
                    break;
                }
            }
        }
    }
    
    public void RemoveItem(int slotNum)                             //해당 슬롯번호를 가지는 아이템 삭제
    {

        inventoryItems.Remove(inventoryItems[FindIndexBySlotNum(slotNum)]);     //아이템 데이터 삭제
        inventoryUI.ClearSlot(slotNum);                                         //아이템 슬롯에 들어있는 데이터 삭제
    }

    public int FindIndexBySlotNum(int slotNum)                      //슬롯번호로 아이템 위치 찾기
    {
        int index = -1;
        for(int i = 0;i< inventoryItems.Count;i++)
        {
            if (inventoryItems[i].slotNum == slotNum)
            {
                index = i;
                break;
            }
        }

        return index;
    }
    public Item FindItemBySlotNum(int num)                          //슬롯번호로 아이템 찾기
    {
        Debug.Log("Item " + inventoryItems.Find(x => x.slotNum.Equals(num)));
        return inventoryItems.Find(x => x.slotNum.Equals(num));
    }
    public int FindItemIndexBySlotNum(int slotNum) //해당 슬롯 번호에 있는 위치를 찾기(해당 슬롯 번호를 가지는아이템이없으면 -1 반환)
    {
        for (int i = 0; i < inventoryItems.Count; i++)                          //해당 슬롯이 가지고 있는 아이템의 데이터 위치 찾기
        {
            if (inventoryItems[i].slotNum == slotNum)
            {
                return i;
            }
        }
        return -1;
    }
    public int ChangeAmount(int index, int changeValue)
    {
        Debug.Log(index);
        CountableItem updatedItem = (CountableItem)inventoryItems[index];
        //해당 아이템이 0보다 적어질때 적어진 만큼 반환한다.
        if (updatedItem.amount + changeValue < 0)
        {
            return updatedItem.amount + changeValue;
        }
        updatedItem.amount += changeValue;

        inventoryItems[index] = updatedItem;
        UpdateSlot(inventoryItems[index]);
        return 0;
    }
    public void ChangeSlotNum(int index, int changeValue)               //슬롯번호 변경(아이템 위치,바뀔 슬롯번호)
    {
        Debug.Log(index +"  "+ changeValue);
        inventoryItems[index].slotNum = changeValue;

        UpdateSlot(inventoryItems[index]);
    }

    public void ItemAmountOverCheck(int index)                  //아이템이 겹치기 가능최대갯수를 넘었는지 확인
    {
        CountableItem item = (CountableItem)inventoryItems[index];
        int overAmount = item.amount - itemMaxAmount;           //최대갯수를 넘었을때의 넘은 수(총 아이템의 갯수 - 최대갯수 = 오버되는 갯수)

        if (overAmount > 0)                                     //최대갯수를 넘었을때
        {
            ChangeAmount(index, -overAmount);                   //최대갯수를 넘은만큼 감소 = 최대갯수가 된다

            item.SetItem(item.id, overAmount, inventoryUI.FindFrontEmptySlotIndex());       //아이템 세팅 후
            inventoryItems.Add(item);                                                          //아이템이 다차서 추가
            UpdateSlot(item);

            Debug.Log("Over");
            ItemAmountOverCheck(inventoryItems.Count-1);        //오버된 수만큼 다시 오버되었는지 체크

            item.amount = overAmount;
        }
    }
    public void UpdateSlot(Item newItem)                        // 슬롯 업데이트
    {
        if (newItem.isEquipment)
        {
            inventoryUI.UpdateSlotUI((EquipmentItem)newItem);
            return;
        }

        inventoryUI.UpdateSlotUI((CountableItem)newItem);
    }

    
    public void Swapitem(int ChangeItem, int baseitem)
    {
        /*
          기존의아이템과 세로 들어갈 위치 받아오기
          기존 아이템이 장비창에서의 이동 일때는 인벤토리 슬롯이 비어있거나 장비 아이템일때만 교환가능
          만약 다른 아이템일 시 교환 실패
          만약 빈칸일시 아이템 변화 가능
        
          기존아이템이 인벤토리내에서의 이동일때는 교환
         */
        int index1, index2;                                    //가지고 있는 아이템 위치

        index1 = FindItemIndexBySlotNum(baseitem);            
        index2 = FindItemIndexBySlotNum(ChangeItem);          //해당 슬롯 번호에 있는 아이템 찾기

        if (inventoryUI.EmptyCheck(ChangeItem))               //만약 비어있는곳에 드래그 앤 드롭되었을때
        {
            ChangeSlotNum(index1, ChangeItem);               //기존에 있던 위치에 새로바뀐 슬롯번호 추가
            UpdateSlot(inventoryItems[index1]);             //기존 아이템 슬롯 업데이트

            inventoryUI.ClearSlot(baseitem);                //기존의 슬롯번호아이템 리셋
            return;
        }
        

        ChangeSlotNum(index1, ChangeItem);
        ChangeSlotNum(index2, baseitem);
        UpdateSlot(inventoryItems[index1]);
        UpdateSlot(inventoryItems[index2]);
    }

    // 인벤토리 관련 메서드 추가 (정렬, 아이템 사용 등)
    public void SaveInventoryData()
    {
        SaveLoadManager.Instance.SaveInventory(inventoryItems, playerGold);

    }
    public void LoadInventoryData(List<Item> items, int gold)
    {
        inventoryItems = items;
        playerGold = gold;

        inventoryUI.UpdateGoldUI(playerGold);
        foreach(var a in inventoryItems)
        {
            inventoryUI.UpdateSlotUI(a);
        }
    }
}
