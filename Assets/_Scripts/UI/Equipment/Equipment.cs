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
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public EquipmentItem[] equipmentItems = new EquipmentItem[(int)EquipmentType.Count];       //총 장착할수 있는 아이템 갯수
    public EquipmentUI equipmentUI;
    [SerializeField] private CharacterAppearanceManager characterAppearanceManager;
    public void Equipping(Item item)
    {
        if(!item.isEquipment)   
        {
            return;
        }
        EquipmentItem equipmentItem = (EquipmentItem)item;
        EquipmentType type = ItemDataManager.Instance.CheckEquipmenttype(item.id);

        if (!equipmentUI.EmptyCheckSlot((int)type))    //만약 해당 슬롯이 비어있지않다면
        {
           Inventory.Instance.GetItem(equipmentItems[(int)type].id, 1, equipmentItems[(int)type].upgrade);
            
           Debug.Log("해당 슬롯 비어있지 않아 인벤토리와 바꿔야함");
        }

        Inventory.Instance.RemoveItem(item.slotNum);
        equipmentItems[(int)type] = equipmentItem;
        equipmentItems[(int)type].slotNum = (int)type;
        characterAppearanceManager.ChangeParts(type, ItemDataManager.Instance.FindApperanceID(item.id));
        equipmentUI.UpdateItem(equipmentItem.id, (int)type);
    }
    public void UnEquipping(int slotNum)
    {
        Debug.Log((int)slotNum);
        Inventory.Instance.GetItem(equipmentItems[(int)slotNum].id, 1, equipmentItems[(int)slotNum].upgrade);

        equipmentItems[slotNum] = null;
        characterAppearanceManager.ChangeParts((EquipmentType)slotNum, 0);
        equipmentUI.ClearEquipmentSlot(slotNum);

    }
}
 
/* 
 만약 인베토리의 슬롯에서 장비창으로 드래그가되었을때
 드래그 중인 아이템의 종류가 장비일때
 드롭된 슬롯의 번호와 아이템의 장비 타입이 같으면
 아이템 장착실시
{
    기존의 아이템 을 잠시 저장
    기존의 아이템이 있던 자리에 새로운 아이템 장착
    기존의 아이템 리턴
    이때 기존의 아이템이 비어있지 않을경우
    기존의 아이템 인벤토리에 저장
}
*/