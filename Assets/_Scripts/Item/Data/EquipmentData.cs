using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentData : ItemData
{
    public EquipmentType equipmentType;         //장비 타입(ex) 무기, 갑옷, 바지 등)
    public int equipmentLevel;                  //장비 착용 레벨 ( ex) 20Lv이상, 30LV 이상 등)
    public int appearanceID;
    public int detailID;                        //장비의 디테일 아이디
    
    public EquipmentData()
    {


    }
    public void SetEquipmentData(string[] datas)
    {
        equipmentType = System.Enum.Parse<EquipmentType>(datas[1]);
        equipmentLevel = int.Parse(datas[2]);
        appearanceID = int.Parse(datas[3]);
        detailID = int.Parse(datas[4]);
    }

}
