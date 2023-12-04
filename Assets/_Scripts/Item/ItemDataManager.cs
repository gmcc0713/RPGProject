using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ItemDataManager : MonoBehaviour
{

    public static ItemDataManager Instance { get; private set; }

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    public ItemData[] itemDatas;

    public void Start()
    {
        LoadItemData();
    }
    public void LoadItemData()
    {
        itemDatas = Resources.LoadAll<ItemData>("ScriptableObject/Item");

    }
    public ItemData FindItem(int num)
    {
        return itemDatas[num];
    }
    public Sprite FindItemImage(int num)
    {
        return itemDatas[num].itemIcon;
    }
    public bool CheckIsEquipmentItem(int num)
    {
        
        return itemDatas[num].isEquipmentItem;
    }
    public EquipmentType CheckEquipmenttype(int num)
    {

        if (!itemDatas[num].isEquipmentItem)
        {
            return EquipmentType.Non;
        }
        EquipmentData equipmentData = (EquipmentData)itemDatas[num];
        return equipmentData.equipmentType;
    }
    public int FindApperanceID(int num)
    {

        if (!itemDatas[num].isEquipmentItem)
        {
            return -1;
        }
        EquipmentData equipmentData = (EquipmentData)itemDatas[num];
        return equipmentData.appearanceID;
    }
}
