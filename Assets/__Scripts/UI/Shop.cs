using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<int> items = new List<int>();             //상점에서 판매할 아이템 들
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private ItemToolTip toolTip;               

    private List<ShopSlot> slots = new List<ShopSlot>();

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MakeShopSlot(items.Count);
        Slotinitialize();
        SlotUpdate();
    }
    public void MakeShopSlot(int count)
    {
        for(int i = 0; i < count; i++)
        {
            //오브젝트 풀링으로
            GameObject clone = Instantiate(slotPrefab);
            clone.transform.SetParent(parent.transform, false);

            ShopSlot slot = clone.GetComponent<ShopSlot>();
            slots.Add(slot);
        }
    }
    public void Slotinitialize()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetToolTipUpdater(toolTip);

            slots[i].SetData(items[i]);     //상점 초기세팅
        }
    }
    public void SlotUpdate()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].UpdateSlot();
        }
    }
    //items에 들어있는 상점 슬롯 만드는 코드

    //만들어진 상점 슬롯에 아이템들을 넣는 코드




}
