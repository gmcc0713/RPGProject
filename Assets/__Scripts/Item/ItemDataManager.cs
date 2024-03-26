using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager Instance { get; private set; }

    public List<ItemData> m_lItemDatas;

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        m_lItemDatas = new List<ItemData>();
        LoadItemData();
        
    }
    public void LoadItemData()
    {
        List< string[]> itemDatas = SCVLoadManager.Instance.Load("CSV/Item/ItemDatas");
        List< string[]> equipmentDatas = SCVLoadManager.Instance.Load("CSV/Item/Equipment/EquipmentDatas");
        List< string[]> weaponDatas = SCVLoadManager.Instance.Load("CSV/Item/Equipment/WeaponDatas");
        List< string[]> armorDatas = SCVLoadManager.Instance.Load("CSV/Item/Equipment/ArmorDatas");
        List< string[]> potionDatas = SCVLoadManager.Instance.Load("CSV/Item/PotionDatas");
        List< string[]> etcDatas = SCVLoadManager.Instance.Load("CSV/Item/ETCDatas");
        for( int i =0; i < itemDatas.Count; i++)
        {
            switch (itemDatas[i][1])
            {
                case "Weapon":
                    {

                        WeaponData data = new WeaponData();
                        data.SetData(itemDatas[i]);
                        data.SetEquipmentData(equipmentDatas[data.detailDataID]);
                        data.SetWeaponData(weaponDatas[data.detailID]);
                        m_lItemDatas.Add(data);
                        break;
                    }
                case "Armor":
                    {
                        ((EquipmentData)m_lItemDatas[i]).SetEquipmentData(equipmentDatas[m_lItemDatas[i].detailDataID]);
                        ((ArmorData)m_lItemDatas[i]).SetArmorData(armorDatas[((EquipmentData)m_lItemDatas[i]).detailID]);
                        break;
                    }
                case "Portion":
                    {
                        PortionData data = new PortionData();
                        data.SetData(itemDatas[i]);
                        data.SetPortionData(potionDatas[data.detailDataID]);
                        m_lItemDatas.Add(data);
                        break;
                    }
                case "Etc":
                    {
                        EtcItemData data = new EtcItemData();
                        data.SetData(itemDatas[i]);
                        data.SetETCData(etcDatas[data.detailDataID]);
                        m_lItemDatas.Add(data);
                        break;
                    }
                default:
                    Debug.Log("¾ÆÀÌÅÛ È¹µæ ¿À·ù");
                    break;
            }
        }
    }

    public ItemData FindItem(int num)
    {
        return m_lItemDatas[num];
    }
    public Sprite FindItemImage(int num)
    {
        Debug.Log(num);
        return m_lItemDatas[num].itemIcon;
    }
    public bool CheckIsEquipmentItem(int num)
    {
        return m_lItemDatas[num].isEquipmentItem;
    }
    public bool CheckCanUseItem(int num)
    {
        return m_lItemDatas[num].m_bCanUse;
    }
    public EquipmentType CheckEquipmenttype(int num)
    {

        if (!m_lItemDatas[num].isEquipmentItem)
        {
            return EquipmentType.Non;
        }
        EquipmentData equipmentData = (EquipmentData)m_lItemDatas[num];
        return equipmentData.equipmentType;
    }
    public int FindApperanceID(int num)
    {

        if (!m_lItemDatas[num].isEquipmentItem)
        {
            return -1;
        }
        EquipmentData equipmentData = (EquipmentData)m_lItemDatas[num];
        return equipmentData.appearanceID;
    }

}
