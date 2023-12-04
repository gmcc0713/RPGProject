using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
   Weapon = 0,           //무기아이템
   Armor ,              //방어구아이템
   Portion ,            //포션아이템
   Etc ,                //기타아이템
}
public class ItemData : ScriptableObject
{
    public ItemType itemtype;           //아이템 타입
    public int itemId = -1;             //아이템 아이디
    public string itemName;             //아이템 이름

    public Sprite itemIcon;             //아이템 아이콘
    public bool isEquipmentItem;        //아이템이 착용 가능 인지


    public int price;               //아이템 판매가격

}



