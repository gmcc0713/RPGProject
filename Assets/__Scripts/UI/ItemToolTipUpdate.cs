using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemToolTipUpdate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ItemToolTip itemToolTip;
    int itemNum;
    bool isEmpty = true;
    public void SetToolTip(ItemToolTip toolTip)
    {
        itemToolTip = toolTip;
    }
    public void SetData(int num,bool emptyCheck = true)
    {
        itemNum = num;
        isEmpty = emptyCheck;
    }
    public void SetEmpty(bool empty)
    {
        isEmpty = empty;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemToolTip.SetPosition(eventData.position);
        ShowToolTip();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }
    public void ShowToolTip()
    {
        if(!isEmpty)
        {
            itemToolTip.UpdateToolTip(ItemDataManager.Instance.FindItem(itemNum));
            itemToolTip.gameObject.SetActive(true);
        }

    }
    public void HideToolTip()
    {
        if (itemToolTip)
        {
            itemToolTip.gameObject.SetActive(false);
        }
    }
}
