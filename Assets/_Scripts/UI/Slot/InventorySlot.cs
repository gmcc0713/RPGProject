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

    public void OnBeginDrag(PointerEventData eventData)     // 드래그 시작
    {
        if (isEmpty)
        {
            return;
        }
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ViewIcon.instance.viewIcon = this;                              //드래그 할떄의 툴팁 슬롯에 자기자신 세팅
            ViewIcon.instance.DragSetImage(itemImage.sprite);
            ViewIcon.instance.transform.position = eventData.position;      //마우스가 현재 있는 위치
        }

    }

    public void OnDrag(PointerEventData eventData)  //드래그 도중
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


    public void OnEndDrag(PointerEventData eventData)   //드래그 종료시 호출
    {
        ViewIcon.instance.SetColor(0);
        ViewIcon.instance.viewIcon = null;

    }

    public void OnDrop(PointerEventData eventData)      //드래그 후 해당 위치에 떨어졌을 때 호출(아이템이 자기자신 위에 떨어졌을때)
    {
        if(!ViewIcon.instance.viewIcon)  
        {
            return;
        }
        switch(type)        //자기자신이 무슨 슬롯인지
        {
            case SlotType.Inventory:
                if(ViewIcon.instance.viewIcon._type == SlotType.Inventory)                      //인벤토리 -> 인벤토리
                {
                    Debug.Log("Swap Iv to Iv -> new slotNum : " + slotNum + " base slotNum : " + ViewIcon.instance.viewIcon.slotNum);
                    Inventory.Instance.Swapitem(slotNum, ViewIcon.instance.viewIcon.slotNum);
                }
                else if (ViewIcon.instance.viewIcon._type == SlotType.Equipment)                //장비창 -> 인벤토리
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
    //아이템 장착

    //아이템위치교환
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
