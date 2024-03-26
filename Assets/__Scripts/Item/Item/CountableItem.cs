using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableItem : Item
{
    public int amount;
    public bool m_bCanUse;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        id = -1;
        slotNum = -1;
        amount = 0;
        isEquipment = false;
    }
    public void SetItem(int _id,int _amount, int _slotNum = -1)
    {
        id = _id;
        slotNum = _slotNum;
        amount = _amount;
    }
    public override void ItemUse()
    {

    }
}
