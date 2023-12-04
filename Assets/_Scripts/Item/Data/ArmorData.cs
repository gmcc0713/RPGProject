using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Armor")]
public class ArmorData : EquipmentData
{
    public float defenseValue;              //추가 방어력
    public float healthValue;               //추가 체력

}
