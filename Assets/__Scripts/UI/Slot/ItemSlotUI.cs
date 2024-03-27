using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] protected Sprite m_baseImage;
    [SerializeField] protected Image m_itemImage;
    [SerializeField] protected TextMeshProUGUI m_itemAmountText;
    [SerializeField] protected ItemToolTipUpdate itemToolTip;
    protected bool m_bIsEmpty;
    protected int m_iSlotNum;
    [SerializeField] protected SlotType m_eSlotType;
    [SerializeField] protected Button m_selectButton;

    private bool M_bCanDivide;
    public SlotType _eSlotType => m_eSlotType;
    public int _iSlotNum => m_iSlotNum;
    public bool _bIsEmpty => m_bIsEmpty;

    public void SetItemSlot(int num)
    {
        m_iSlotNum = num;
    }
     
    void Start()
    {
        Initialize();

    }
    void Initialize()
    {
        m_bIsEmpty = true;
    }
    public void OnBeginDrag(PointerEventData eventData)     // 드래그 시작
    {
        if (m_bIsEmpty)
        {
            return;
        }
        //m_bIsDragging = true;
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ViewIcon.instance.viewIcon = this;                              //드래그 할떄의 툴팁 슬롯에 자기자신 세팅
            ViewIcon.instance.DragSetImage(m_itemImage.sprite);               //드래그 되는 이미지 추가
            ViewIcon.instance.transform.position = eventData.position;      //마우스가 현재 있는 위치
        }

        if (ViewIcon.instance.viewIcon._eSlotType == SlotType.QuickSlot)
        {
            SetEmpty();
        }
    }
    public void OnDrag(PointerEventData eventData)  //드래그 도중
    {
        if (m_bIsEmpty)
        {
            return;
        }
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ViewIcon.instance.transform.position = eventData.position;
        }
        this.GetComponentInParent<ItemQuickSlotUI>();
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        ViewIcon.instance.SetColor(0);
        ViewIcon.instance.viewIcon = null;

    }

    public void OnDrop(PointerEventData eventData)      //드래그 후 해당 위치에 떨어졌을 때 호출(아이템이 자기자신 위에 떨어졌을때)
    {
        if (!ViewIcon.instance.viewIcon)
        {
            ViewIcon.instance.SetColor(0);
            ViewIcon.instance.viewIcon = null;
            return;
        }

        switch (m_eSlotType)        //자기자신이 무슨 슬롯인지
        {
            case SlotType.Inventory:
                if (ViewIcon.instance.viewIcon._eSlotType == SlotType.Inventory)                      //인벤토리 -> 인벤토리
                {
                    Debug.Log("Swap Iv to Iv -> new slotNum : " + m_iSlotNum + " base slotNum : " + ViewIcon.instance.viewIcon._iSlotNum);
                    ThirdPersonMovement.Instance._Inventory.Swapitem(m_iSlotNum, ViewIcon.instance.viewIcon.m_iSlotNum);
                }
                else if (ViewIcon.instance.viewIcon._eSlotType == SlotType.Equipment)                //장비창 -> 인벤토리
                {
                    Equipment.Instance.UnEquipping(ViewIcon.instance.viewIcon._iSlotNum);
                }
                break;
            case SlotType.Equipment: //인벤토리 -> 장비창
                Equipment.Instance.Equipping(ThirdPersonMovement.Instance._Inventory.FindItemBySlotNum(ViewIcon.instance.viewIcon._iSlotNum));
                break;
            case SlotType.QuickSlot: //인벤토리 -> 퀵슬롯
                {
                    //아이템이 장착할 수 있을 때 장착하기
                   if (ViewIcon.instance.viewIcon._eSlotType == SlotType.Inventory) //드래그 시작시의 슬롯 타입이 Inventory일때
                    {
                        Item item = ThirdPersonMovement.Instance._Inventory.FindItemBySlotNum(ViewIcon.instance.viewSlotNum);
                        if (ItemDataManager.Instance.FindItem(item.id).itemtype == ItemType.Portion)    //해당 하이템이 장착가능하면
                        {
                            Debug.Log("퀵슬롯에 장착");
                            if(item is PortionItem pItem)
                            SetItemUI(pItem.id, pItem.amount);
                            QuickSlotManager.Instance.SetItemInQuickSlot(m_iSlotNum, ViewIcon.instance.viewIcon._iSlotNum);

                        }

                    }
                    break;
                }
            default:

                ViewIcon.instance.SetColor(0);
                ViewIcon.instance.viewIcon = null;
                break;
        }
        ViewIcon.instance.SetColor(0);
        ViewIcon.instance.viewIcon = null;
    }
    public void SetEmpty()
    {
        m_itemImage.sprite = m_baseImage;
        m_bIsEmpty = true;

        m_itemAmountText.text = "";
        if (m_eSlotType != SlotType.QuickSlot)
            itemToolTip.SetEmpty(true);
    }
    public void SetItemUI(int id, int amount = -1)
    {
        Debug.Log("itemSetQuickSlot");
        m_itemImage.sprite = ItemDataManager.Instance.FindItemImage(id);
        m_bIsEmpty = false;

        if (m_eSlotType != SlotType.QuickSlot)
            itemToolTip.SetData(id, m_bIsEmpty);

        if (ItemDataManager.Instance.CheckIsEquipmentItem(id))
        {
            m_itemAmountText.text = "";
            return;
        }
        m_itemAmountText.text = amount.ToString();
    }

    public void SelectBtnOn(bool b)
    {
        if(b)
        {
            m_selectButton.gameObject.SetActive(true);
            Item item = ThirdPersonMovement.Instance._Inventory.FindItemBySlotNum(m_iSlotNum);
            if (item is CountableItem cItem)
            {
                if (cItem.amount > 1)
                {
                    m_selectButton.image.color = new Color(0, 1, 0, 0.2f);
                    M_bCanDivide = true;
                    return;
                }
            }
            m_selectButton.image.color = new Color(1, 0, 0, 0.2f);
            M_bCanDivide = false;
            return;
        }
        m_selectButton.gameObject.SetActive(false);

    }
    public void DivideBtnClickCheck()
    {
        if(!M_bCanDivide)
        {
            return;
        }
        Item item = ThirdPersonMovement.Instance._Inventory.FindItemBySlotNum(m_iSlotNum);
        if (item is CountableItem cItem && !m_bIsEmpty)
        {
            UIManager.Instance._InventoryUI.DivideItem(m_iSlotNum);
        }
    }
    /*public void UpdateSlot(int inventoryNumber)
    {
        Item item = Inventory.Instance.FindItemBySlotNum(slotNum);
        SetItemUI()
        itemNum = item.id;

    }*/
}
