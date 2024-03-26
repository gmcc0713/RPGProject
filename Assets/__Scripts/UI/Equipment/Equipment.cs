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
            return;
        }
        Destroy(gameObject);
    }

    public EquipmentItem[] equipmentItems = new EquipmentItem[(int)EquipmentType.Count];       //�� �����Ҽ� �ִ� ������ ����
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
        EquipmentType type = ItemDataManager.Instance.CheckEquipmenttype(equipmentItem.id);  //�ش� �������� Ÿ�� Ȯ��(����, ���� ��)

        if (!equipmentUI.EmptyCheckSlot((int)type))    //���� �ش� ������ ��������ʴٸ�
        {
            ThirdPersonMovement.Instance._Inventory.GetItem(equipmentItems[(int)type].id, 1, equipmentItems[(int)type].upgrade);
            
           Debug.Log("�ش� ���� ������� �ʾ� �κ��丮�� �ٲ����");
        }

        ThirdPersonMovement.Instance._Inventory.RemoveItem(item.slotNum);  //�κ��丮 ������ ������ ����
        equipmentItems[(int)type] = equipmentItem;      //�ش� �������� �߰�
        equipmentItems[(int)type].slotNum = (int)type;  //�ش� ������ ���Թ�ȣ �߰�
        characterAppearanceManager.ChangeParts(type, ItemDataManager.Instance.FindApperanceID(item.id));//������ȭ
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
            Debug.Log("���â�� ������ �ֳ� Ȯ��");
            if(equipmentItems[i] != null)
            {
                itemData.Add(equipmentItems[i]);
                Debug.Log("�߰�");
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
            characterAppearanceManager.ChangeParts(ItemDataManager.Instance.CheckEquipmenttype(data.id), ItemDataManager.Instance.FindApperanceID(data.id));//������ȭ
          
        }
    }
}