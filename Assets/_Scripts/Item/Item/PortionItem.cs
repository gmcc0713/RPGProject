using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionItem : CountableItem
{
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        id = -1;
        slotNum = -1;
        isEquipment = false;
        amount = 0;
    }

}
