using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

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
    //----------------------------------------------------------
    private List<Quest> m_lQuests;  //모든 퀘스트 데이터 SCV파일에서 불러옴
    


    private bool m_bIsFirst = true;
    private List<Item> m_iInventoryItems;
    private List<EquipmentItem> m_equipmentItems;
    private PlayerStatsData m_PlayerStatsData;
    private StatsPoint m_PlayerStatsPoint;
    private PlayerData m_PlayerData;
    private int m_iGold;

    private List<Quest> m_lProgressQuest;   //플레이어가 가지고 있는 퀘스트(진행중,클리어);
    [SerializeField] private int[] m_iPlayerCustomizeIdx;

    public bool _bIsFirst => m_bIsFirst;

    public int[] LoadCustomizeIdx()
    {
        return m_iPlayerCustomizeIdx;
    }
    public void SaveInventory(List<Item> inventoryItems,int gold)
    {
        m_iInventoryItems = inventoryItems;
        m_iGold = gold;
        m_bIsFirst = false;
    }
    public void SavePlayerCustomizeIdx(int hair, int ear,int face)
    {
        m_iPlayerCustomizeIdx[0] = hair;
        m_iPlayerCustomizeIdx[1] = ear;
        m_iPlayerCustomizeIdx[2] = face;
    }
    public void SaveEquipment(List<EquipmentItem> equipmentItems)
    {
        m_equipmentItems = equipmentItems;
    }
    public void SaveQuest(List<Quest> quests)
    {
        m_lProgressQuest = quests;
    }
    public void SavePlayerStatsData(PlayerStatsData statsData,StatsPoint statsPoint)
    {
        m_PlayerStatsData = statsData;
        m_PlayerStatsPoint = statsPoint;
    }
    public void SavePlayerData(PlayerData data)
    {
        m_PlayerData = data;
    }
    public void LoadInventory()
    {
        if(m_bIsFirst)
        {
            return;
        }
        ThirdPersonMovement.Instance._Inventory.LoadInventoryData(m_iInventoryItems, m_iGold);
    }
    public void LoadEquipment()
    {
        if (m_bIsFirst)
        {
            return;
        }
        Equipment.Instance.LoadEquipmentItemData(m_equipmentItems);
    }
    public void LoadQuests()
    {
        if (m_bIsFirst)
        {
            return;
        }
        QuestManager.Instance.LoadQuset(m_lProgressQuest);
    }
    public void LoadStats()
    {
        if (m_bIsFirst)
        {
            return;
        }
        PlayerStats.Instance.LoadStatsData(m_PlayerStatsData,m_PlayerStatsPoint);
    }
    public PlayerData LoadPlayerData()
    {
        if (m_bIsFirst)
        {
            return null;
        }
        return m_PlayerData;
    }
    public void Save()
    {
        ThirdPersonMovement.Instance._Inventory.SaveInventoryData();
        Equipment.Instance.SaveEquipmentItem();
        PlayerStats.Instance.SaveStatsData();
        QuestManager.Instance.SaveQuest();

    }
}
