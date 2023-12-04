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

    Count   //enum ���� Ȯ��
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

    public EquipmentItem[] equipmentItems = new EquipmentItem[(int)EquipmentType.Count];       //�� �����Ҽ� �ִ� ������ ����
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

        if (!equipmentUI.EmptyCheckSlot((int)type))    //���� �ش� ������ ��������ʴٸ�
        {
           Inventory.Instance.GetItem(equipmentItems[(int)type].id, 1, equipmentItems[(int)type].upgrade);
            
           Debug.Log("�ش� ���� ������� �ʾ� �κ��丮�� �ٲ����");
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
 ���� �κ��丮�� ���Կ��� ���â���� �巡�װ��Ǿ�����
 �巡�� ���� �������� ������ ����϶�
 ��ӵ� ������ ��ȣ�� �������� ��� Ÿ���� ������
 ������ �����ǽ�
{
    ������ ������ �� ��� ����
    ������ �������� �ִ� �ڸ��� ���ο� ������ ����
    ������ ������ ����
    �̶� ������ �������� ������� �������
    ������ ������ �κ��丮�� ����
}
*/