using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Inventory
{
    public List<Item> inventoryItems;       //현재 내가 가지고 있는 아이템
    [SerializeField] private int itemMaxAmount = 64;
    [SerializeField] private int SlotmaxCount = 24;
    [SerializeField] private int playerGold = 10000;
    public void initialize()
    {
        inventoryItems = new List<Item>();
        UIManager.Instance._InventoryUI.UpdateGoldUI(playerGold);
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
        UIManager.Instance._InventoryUI.UpdateGoldUI(playerGold);
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
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (!inventoryItems[i].isEquipment)
            {
                if(inventoryItems[i] is PortionItem cItem)
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

        PortionItem item = new PortionItem();

        item.SetItem(id, amount, UIManager.Instance._InventoryUI.FindFrontEmptySlotIndex());
        inventoryItems.Add(item);
        UpdateSlot(item);
        QuickSlotManager.Instance.ItemAddQuickSlotUpdate(id);



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

        UIManager.Instance._InventoryUI.UpdateSlotUI(pitem);

        QuickSlotManager.Instance.UpdateQuickSlotByQuickSlotNum(quickSlotNum);

        if (pitem.amount<=0)
        {
            UIManager.Instance._QuickSlotUI.RemoveItem(quickSlotNum);
            RemoveItem(ivSlotNum);
        }
    }
    public void AddCountableItem(int id, int amount)                    //갯수 샐수있는 아이템을 얻었을때
    {
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
            item.SetItem(id, amount, UIManager.Instance._InventoryUI.FindFrontEmptySlotIndex());
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
        item.SetItem(id, upgrade, UIManager.Instance._InventoryUI.FindFrontEmptySlotIndex(), true);       //비어있는 가장 첫번쨰 위치찾기
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
        UIManager.Instance._InventoryUI.ClearSlot(slotNum);                                         //아이템 슬롯에 들어있는 데이터 삭제
    }

    public int FindIndexBySlotNum(int slotNum)                      //슬롯번호로 인벤토리가 가지고 있는 아이템 위치 찾기
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
    public int FindItemIdBySlotNum(int num)
    {
       ;
        return inventoryItems[FindIndexBySlotNum(num)].id;
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

            item.SetItem(item.id, overAmount, UIManager.Instance._InventoryUI.FindFrontEmptySlotIndex());       //아이템 세팅 후
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
            UIManager.Instance._InventoryUI.UpdateSlotUI((EquipmentItem)newItem);
            return;
        }

        UIManager.Instance._InventoryUI.UpdateSlotUI((CountableItem)newItem);
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

        if (UIManager.Instance._InventoryUI.EmptyCheck(ChangeItem))               //만약 비어있는곳에 드래그 앤 드롭되었을때
        {
            ChangeSlotNum(index1, ChangeItem);               //기존에 있던 위치에 새로바뀐 슬롯번호 추가
            UpdateSlot(inventoryItems[index1]);             //기존 아이템 슬롯 업데이트

            UIManager.Instance._InventoryUI.ClearSlot(baseitem);                //기존의 슬롯번호아이템 리셋
            return;
        }
        
        if(inventoryItems[index1].id == inventoryItems[index2].id) 
        {
            if (inventoryItems[index1] is CountableItem cItem1 && inventoryItems[index2] is CountableItem cItem2)
            {
                if(itemMaxAmount > cItem2.amount + cItem1.amount)
                {
                    cItem2.amount += cItem1.amount;
                    RemoveItem(baseitem);
                    UpdateSlot(cItem2);

                }
                else
                {
                    cItem2.amount = itemMaxAmount;
                    cItem1.amount = cItem2.amount + cItem1.amount - itemMaxAmount;
                    UpdateSlot(cItem1);
                    UpdateSlot(cItem2);
                }
            }
            return;
        }
        ChangeSlotNum(index1, ChangeItem);
        ChangeSlotNum(index2, baseitem);

    }
    public void SwapSlot(int index1,int index2)
    {
        int slotTmp = inventoryItems[index1].slotNum;
        Item tmp = inventoryItems[index1];

        inventoryItems[index1] = inventoryItems[index2];
        inventoryItems[index1].slotNum = inventoryItems[index2].slotNum;

        inventoryItems[index2] = tmp;
        inventoryItems[index2].slotNum = slotTmp;


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

        UIManager.Instance._InventoryUI.UpdateGoldUI(playerGold);
        foreach(var a in inventoryItems)
        {
            UIManager.Instance._InventoryUI.UpdateSlotUI(a);
        }
    }
    public void Divide(int slotNum,int amount)
    {
        if(inventoryItems[FindIndexBySlotNum(slotNum)] is CountableItem cItem)
        {
            cItem.amount -= amount;
            if (InventoryFullCheck())
            {
                return;
            }

            CountableItem item = new CountableItem();
            item.SetItem(cItem.id, amount, UIManager.Instance._InventoryUI.FindFrontEmptySlotIndex());
            inventoryItems.Add(item);
            UpdateSlot(item);
            UpdateSlot(cItem);  
        }
    }
}
