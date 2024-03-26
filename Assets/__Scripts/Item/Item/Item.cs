using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public void ItemUse();
}

public class Item : IItem
{
    public int id;
    public int slotNum;
    public bool isEquipment;
    public virtual void ItemUse()
    {

    }



}