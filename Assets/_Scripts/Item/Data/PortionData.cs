using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PortionType
{
    HP = 0,
    MP,
    EXP
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/ConsumptionItem")]


public class PortionData : ItemData
{
    PortionType portionType;
    public float value;

}