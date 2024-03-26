using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EtcItemData : ItemData
{
    public string itemDescription;      //아이템 설명
    public void SetETCData(string[] datas)
    {
        itemDescription = datas[0];
    }
}

