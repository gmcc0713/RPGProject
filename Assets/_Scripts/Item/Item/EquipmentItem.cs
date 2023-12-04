using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public int upgrade;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        id = -1;
        slotNum = -1;
        upgrade = 0;
    }
    public void SetItem(int _id, int _upgrade, int _slotNum = -1,bool _isEquipment= false)
    {
        id = _id;
        upgrade = _upgrade;
        slotNum = _slotNum;
        isEquipment = _isEquipment;
    }
}
