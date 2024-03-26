using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewIcon : MonoBehaviour
{
    static public ViewIcon instance;
    public ItemSlotUI viewIcon;
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
        viewSlotNum = viewIcon._iSlotNum;
    }

    public void SetColor(float _alpha)
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }
    public void SetEmpty()
    {
           
    }
}
