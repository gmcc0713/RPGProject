using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortionItem : CountableItem
{
    
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        id = -1;
        slotNum = -1;
        isEquipment = false;
        amount = 0;
    }
    public override void ItemUse()
    {
        Debug.Log("ItemUse");
        ItemData data = ItemDataManager.Instance.m_lItemDatas[id];
        ThirdPersonMovement.Instance.UsePortionItem(((PortionData)data).m_eType, ((PortionData)data).m_iValue);
    }
}
//Use 만들고 사용 하지않는 아이템자체는 비어있는 Use로 사용(system 메세지 출력시 해당 Use 함수에 구현)