using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Armor")]
public class EquipmentData : ItemData
{
    public EquipmentType equipmentType;         //��� Ÿ��(ex) ����, ����, ���� ��)
    public int equipmentLevel;                  //��� ���� ���� ( ex) 20Lv�̻�, 30LV �̻� ��)
    public int appearanceID;

}
