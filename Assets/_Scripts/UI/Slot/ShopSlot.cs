using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private ItemToolTipUpdate toolTipUpdate;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI buyPriceText;
    [SerializeField] private Image itemImage;

    private int itemID =0;
    private int buyPrice;

    public void SetToolTipUpdater(ItemToolTip toolTip)
    {
        toolTipUpdate.SetToolTip(toolTip);
    }
    public void UpdateSlot()
    {

    }
    public void SetData(int id)         //상점 슬롯 초기 세팅
    {
        itemID = id;
        ItemData data = ItemDataManager.Instance.FindItem(itemID);
        nameText.text = data.itemName;
        buyPriceText.text = data.price.ToString();

        itemImage.sprite = data.itemIcon;

        buyPrice = data.price;
        toolTipUpdate.SetData(itemID, false);
    }

    public void BuyItem()
    {
        if(Inventory.Instance.BuyItem(itemID,buyPrice))
        {
            return;
        }
        Debug.Log("구매 실패");
    }
}
