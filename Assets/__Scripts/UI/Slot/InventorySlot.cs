/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler, IDropHandler
{
    [SerializeField] protected int slotNum;
    [SerializeField] protected SlotType type;
    protected bool isEmpty = true;
    protected int itemNum;


    protected Sprite baseImage;
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemAmountText;
    protected ItemToolTipUpdate itemToolTip;

    public bool _isEmpty=>isEmpty;
    public SlotType _type => type;
    public int _slotNum { get { return slotNum; } set { slotNum = value; } }
    //private bool m_bIsDragging;
    protected void Initialize()
    {
        baseImage = itemImage.sprite;

        if (type != SlotType.QuickSlot)
        {
            itemToolTip = GetComponent<ItemToolTipUpdate>();
            itemToolTip.SetData(itemNum, isEmpty);
        }
    }
    protected void Start()
    {

        Initialize();
    }

    public void OnBeginDrag(PointerEventData eventData)     // 드래그 시작
    {

        if (isEmpty)
        {
            return;
        }
        //m_bIsDragging = true;
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ViewIcon.instance.viewIcon = this;                              //드래그 할떄의 툴팁 슬롯에 자기자신 세팅
            ViewIcon.instance.DragSetImage(itemImage.sprite);
            ViewIcon.instance.transform.position = eventData.position;      //마우스가 현재 있는 위치
        }

        if (ViewIcon.instance.viewIcon._type == SlotType.QuickSlot)
        {
            SetEmpty();
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
        this.GetComponentInParent<QuickSlotUI>();
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        ViewIcon.instance.SetColor(0);
        ViewIcon.instance.viewIcon = null;

    }

    public void OnDrop(PointerEventData eventData)      //드래그 후 해당 위치에 떨어졌을 때 호출(아이템이 자기자신 위에 떨어졌을때)
    {
        if(!ViewIcon.instance.viewIcon)  
        {
            ViewIcon.instance.SetColor(0);
            ViewIcon.instance.viewIcon = null;
            return;
        }

        switch(type)        //자기자신이 무슨 슬롯인지
        {
            case SlotType.Inventory:
                if (ViewIcon.instance.viewIcon._eSlotType == SlotType.Inventory)                      //인벤토리 -> 인벤토리
                {
                    Debug.Log("Swap Iv to Iv -> new slotNum : " + slotNum + " base slotNum : " + ViewIcon.instance.viewIcon.slotNum);
                    Inventory.Instance.Swapitem(slotNum, ViewIcon.instance.viewIcon.slotNum);
                }
                else if (ViewIcon.instance.viewIcon._eSlotType == SlotType.Equipment)                //장비창 -> 인벤토리
                {
                    Equipment.Instance.UnEquipping(ViewIcon.instance.viewIcon.slotNum);
                }
                break;
            case SlotType.Equipment: //인벤토리 -> 장비창
                Equipment.Instance.Equipping(Inventory.Instance.FindItemBySlotNum(ViewIcon.instance.viewIcon.slotNum));
                break;
            case SlotType.QuickSlot: //인벤토리 -> 퀵슬롯
                {
                    if (ViewIcon.instance.viewIcon._eSlotType == SlotType.Inventory)
                    {

                        Item item = Inventory.Instance.FindItemBySlotNum(ViewIcon.instance.viewSlotNum);
                        if (ItemDataManager.Instance.FindItem(item.id).itemtype == ItemType.Portion)
                        {
                            Debug.Log("퀵슬롯에 장착");
                            SetItemUI(item.id, ((CountableItem)item).amount);
                            if(this is QuickSlot qSlot)
                            {
                                qSlot.SetQuickSlot(slotNum);
                            }
                            
                        }

                    }
                    break;
                }
            default:
                Debug.Log("아이템을 다른곳에 떨어뜨렸을때");
                ViewIcon.instance.SetColor(0);
                ViewIcon.instance.viewIcon = null;
                break;
        }
        ViewIcon.instance.SetColor(0);
        ViewIcon.instance.viewIcon = null;
    }

    //아이템 장착

    //아이템위치교환
    public void SetEmpty()
    {
        Debug.Log("Empty");
        itemImage.sprite = baseImage;
        isEmpty = true;

        itemAmountText.text = "";
        if(type != SlotType.QuickSlot)
            itemToolTip.SetEmpty(true);
    }
    public void SetItemUI(int id,int amount = -1)
    {
        itemNum = id;
        itemImage.sprite = ItemDataManager.Instance.FindItemImage(itemNum);
        isEmpty = false;
        if (type != SlotType.QuickSlot)
            itemToolTip.SetData(itemNum,isEmpty);

        if (ItemDataManager.Instance.CheckIsEquipmentItem(itemNum))
        {
            itemAmountText.text = "";
            return;
        }
        itemAmountText.text = amount.ToString();
    }
    public void UpdateSlot(int slotNum)
    {
        Item item = Inventory.Instance.FindItemBySlotNum(slotNum);
        itemNum = item.id;
        if (item is CountableItem cItem)
        {
            itemAmountText.text = cItem.amount.ToString();
        }
    }
}
*/