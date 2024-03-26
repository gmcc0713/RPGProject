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
[System.Serializable]
public class ItemData 
{
    public ItemType itemtype;           //아이템 타입
    public int itemId;                  //아이템 아이디
    public string itemName;             //아이템 이름
    public Sprite itemIcon;             //아이템 아이콘
    public bool isEquipmentItem;        //아이템이 착용 가능 인지
    public int price;                   //아이템 판매가격
    public bool m_bCanUse;
    public int detailDataID;            //아이템 상세 정보 아이디(장비,포션 등 다른 테이블에서 아이디로 가져온다)
    public virtual void SetData(string[] datas)
    {
        itemId = int.Parse(datas[0]);
        itemtype = System.Enum.Parse<ItemType>(datas[1]);
        itemName = datas[2];
        //로드를 여러번 할 경우 확인 해야된다(dictionary에 저장 후 키값으로 확인)
        //더이상 사용 안 할때 해제 필수(이때 dictionary도 같이 해제)
        itemIcon = Resources.Load<Sprite>(datas[3]);   

        isEquipmentItem = bool.Parse(datas[4]);
        m_bCanUse = bool.Parse(datas[5]);
        price = int.Parse(datas[6]);
        detailDataID = int.Parse(datas[7]);
    }
}
