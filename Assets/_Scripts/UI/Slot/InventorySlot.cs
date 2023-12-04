using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum SlotType
{
    Inventory,
    Equipment,
    QuickSlot,
}
public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private int slotNum;
    private Sprite baseImage;
    [SerializeField] private SlotType type;
    private bool isEmpty = true;

    private ItemToolTipUpdate itemToolTip;

    [SerializeField] private Image itemImage;
    private int itemNum;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    public bool _isEmpty=>isEmpty;
    public SlotType _type => type;
    public int _slotNum { get { return slotNum; } set { slotNum = value; } }
    Vector3 beginPos;
    void Start()
    {
        baseImage = itemImage.sprite;
        beginPos = transform.position;
        itemToolTip = GetComponent<ItemToolTipUpdate>();
        itemToolTip.SetData(itemNum, isEmpty);

    }

    public void OnBeginDrag(PointerEventData eventData)     // �巡�� ����
    {
        if (isEmpty)
        {
            return;
        }
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ViewIcon.instance.viewIcon = this;                              //�巡�� �ҋ��� ���� ���Կ� �ڱ��ڽ� ����
            ViewIcon.instance.DragSetImage(itemImage.sprite);
            ViewIcon.instance.transform.position = eventData.position;      //���콺�� ���� �ִ� ��ġ
        }

    }

    public void OnDrag(PointerEventData eventData)  //�巡�� ����
    {
        if (isEmpty)
        {
            return;
        }
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ViewIcon.instance.transform.position = eventData.position;
        }

    }


    public void OnEndDrag(PointerEventData eventData)   //�巡�� ����� ȣ��
    {
        ViewIcon.instance.SetColor(0);
        ViewIcon.instance.viewIcon = null;

    }

    public void OnDrop(PointerEventData eventData)      //�巡�� �� �ش� ��ġ�� �������� �� ȣ��(�������� �ڱ��ڽ� ���� ����������)
    {
        if(!ViewIcon.instance.viewIcon)  
        {
            return;
        }
        switch(type)        //�ڱ��ڽ��� ���� ��������
        {
            case SlotType.Inventory:
                if(ViewIcon.instance.viewIcon._type == SlotType.Inventory)                      //�κ��丮 -> �κ��丮
                {
                    Debug.Log("Swap Iv to Iv -> new slotNum : " + slotNum + " base slotNum : " + ViewIcon.instance.viewIcon.slotNum);
                    Inventory.Instance.Swapitem(slotNum, ViewIcon.instance.viewIcon.slotNum);
                }
                else if (ViewIcon.instance.viewIcon._type == SlotType.Equipment)                //���â -> �κ��丮
                {
                    Equipment.Instance.UnEquipping(ViewIcon.instance.viewIcon.slotNum);
                }
                break;
            case SlotType.Equipment:
                Equipment.Instance.Equipping(Inventory.Instance.FindItemBySlotNum(ViewIcon.instance.viewIcon.slotNum));
                break;
            case SlotType.QuickSlot:
                break;
        }

    }
    //������ ����

    //��������ġ��ȯ
    public void SetEmpty()
    {
        itemImage.sprite = baseImage;
        isEmpty = true;

        itemAmountText.text = "";
        itemToolTip.SetEmpty(true);                 
    }
    public void SetItemUI(int id,int amount = -1)
    {

        itemNum = id;
        itemImage.sprite = ItemDataManager.Instance.FindItemImage(itemNum);
        isEmpty = false;
        itemToolTip.SetData(itemNum,isEmpty);

        if(ItemDataManager.Instance.CheckIsEquipmentItem(itemNum))
        {
            itemAmountText.text = "";
            return;
        }
        itemAmountText.text = amount.ToString();
    }
}
