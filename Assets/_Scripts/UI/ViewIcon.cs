using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewIcon : MonoBehaviour
{
    static public ViewIcon instance;
    public InventorySlot viewIcon;
    [SerializeField] private Image image;
    public int viewSlotNum;
    private void Start()
    {
        instance = this;
       
    }
    public void DragSetImage(Sprite _itemImage)
    {
        image.sprite = _itemImage;
        SetColor(1);
        viewSlotNum = viewIcon._slotNum;
    }

    public void SetColor(float _alpha)
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }
}
