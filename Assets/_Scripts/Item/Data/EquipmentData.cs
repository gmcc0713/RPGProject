using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Armor")]
public class EquipmentData : ItemData
{
    public EquipmentType equipmentType;         //장비 타입(ex) 무기, 갑옷, 바지 등)
    public int equipmentLevel;                  //장비 착용 레벨 ( ex) 20Lv이상, 30LV 이상 등)
    public int appearanceID;

}
