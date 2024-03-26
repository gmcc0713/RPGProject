using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Non = -1,
    Weapon = 0,
    Helmet,
    Armor,
    Pants,
    Shoes,
    Hand,
    Ring,
    Nacklace,

    Count   //enum 갯수 확인
}

public class Equipment : MonoBehaviour
{
    public static Equipment Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public EquipmentItem[] equipmentItems = new EquipmentItem[(int)EquipmentType.Count];       //총 장착할수 있는 아이템 갯수
    public EquipmentUI equipmentUI;
    [SerializeField] private CharacterAppearanceManager characterAppearanceManager;
    private void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        SaveLoadManager.Instance.LoadEquipment();
    }
    public void Equipping(Item item)
    {
        if(!item.isEquipment)   
        {
            return;
        }
        EquipmentItem equipmentItem = (EquipmentItem)item;
        EquipmentType type = ItemDataManager.Instance.CheckEquipmenttype(equipmentItem.id);  //해당 아이템의 타입 확인(무기, 투구 등)

        if (!equipmentUI.EmptyCheckSlot((int)type))    //만약 해당 슬롯이 비어있지않다면
        {
            ThirdPersonMovement.Instance._Inventory.GetItem(equipmentItems[(int)type].id, 1, equipmentItems[(int)type].upgrade);
            
           Debug.Log("해당 슬롯 비어있지 않아 인벤토리와 바꿔야함");
        }

        ThirdPersonMovement.Instance._Inventory.RemoveItem(item.slotNum);  //인벤토리 슬롯의 아이템 삭제
        equipmentItems[(int)type] = equipmentItem;      //해당 아이템의 추가
        equipmentItems[(int)type].slotNum = (int)type;  //해당 아이템 슬롯번호 추가
        characterAppearanceManager.ChangeParts(type, ItemDataManager.Instance.FindApperanceID(item.id));//외형변화
        equipmentUI.UpdateItem(item.id, (int)type);

    }
    void ChangeEquipmentItemAndApplyAdditionalStats()
    {
    }
    public void UnEquipping(int slotNum)
    {
        ThirdPersonMovement.Instance._Inventory.GetItem(equipmentItems[(int)slotNum].id, 1, equipmentItems[(int)slotNum].upgrade);

        equipmentItems[slotNum] = null;
        characterAppearanceManager.ChangeParts((EquipmentType)slotNum, 0);
        equipmentUI.ClearEquipmentSlot(slotNum);

    }
    public void SaveEquipmentItem()
    {
        List<EquipmentItem> itemData = new List<EquipmentItem>();
        for(int i =0;i<equipmentItems.Length;i++)
        {
            Debug.Log("장비창에 아이템 있나 확인");
            if(equipmentItems[i] != null)
            {
                itemData.Add(equipmentItems[i]);
                Debug.Log("추가");
            }
        }
        SaveLoadManager.Instance.SaveEquipment(itemData);

    }
    public void LoadEquipmentItemData(List<EquipmentItem> equipmentItemsData)
    {

        foreach(var data in equipmentItemsData)
        {
            
            equipmentItems[data.slotNum] = data;
            Debug.Log(data);
            equipmentUI.UpdateItem(data.id, data.slotNum);
            characterAppearanceManager.ChangeParts(ItemDataManager.Instance.CheckEquipmenttype(data.id), ItemDataManager.Instance.FindApperanceID(data.id));//외형변화
          
        }
    }
}