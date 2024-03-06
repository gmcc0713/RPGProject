using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmorData : EquipmentData
{
    public float defenseValue;              //추가 방어력
    public float healthValue;               //추가 체력
    public void SetArmorData(string[] datas)
    {

        defenseValue = float.Parse(datas[0]);
        healthValue = float.Parse(datas[1]);
    }

}
