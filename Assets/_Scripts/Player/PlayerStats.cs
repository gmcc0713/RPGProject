using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatsPoint
{
    public int m_iStatsStrengthPoint;
    public int m_iStatsIntelligencePoint;
    public int m_iStatsHealthPoint;
    public int m_iStatsLuckeyPoint;
    public int m_iRemainStatsPoint;
}
public struct PlayerStatsData
{
    public float HP;
    public int MP;
    public int Defence;
    public int AttackDamage;
    public int Speed;
    public int Critical;
}

// 세이브 할때 세이브용 데이터 클래스 새로 생성 후 해당 데이터로 처리
// 
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private StatsPoint m_StatsPoint;
    private PlayerStatsData m_StatsData;
    private PlayerStatsData m_AdditionalPlayerStats;
    [SerializeField] private StatsUI m_UISet;
    public StatsPoint _StatsPoint => m_StatsPoint;
    public PlayerStatsData _StatsData => m_StatsData;
    public void UpdatePlayerData(PlayerData data)
    {
        m_UISet.PlayerDataUpdate(data);
    }
    public void Initialize()
    {
        
        m_AdditionalPlayerStats = new PlayerStatsData();
        m_StatsData = new PlayerStatsData();

        m_StatsPoint.m_iStatsStrengthPoint = 5;
        m_StatsPoint.m_iStatsHealthPoint = 5;
        m_StatsPoint.m_iStatsIntelligencePoint = 5;
        m_StatsPoint.m_iStatsLuckeyPoint = 5;
        m_StatsPoint.m_iRemainStatsPoint = 5;

        ChangeStatsPointToStatsData();
        SaveLoadManager.Instance.LoadStats();
        m_UISet.UpdateUI(m_StatsData, m_StatsPoint);

    }
    public void ChangeStatsPointToStatsData()
    {
        m_StatsData.HP = m_StatsPoint.m_iStatsHealthPoint * 100 + m_StatsPoint.m_iStatsStrengthPoint * 30;
        m_StatsData.MP = m_StatsPoint.m_iStatsIntelligencePoint * 100 + m_StatsPoint.m_iStatsLuckeyPoint*10;
        m_StatsData.Defence = m_StatsPoint.m_iStatsHealthPoint * 7;
        m_StatsData.AttackDamage = m_StatsPoint.m_iStatsStrengthPoint * 30 + m_StatsPoint.m_iStatsIntelligencePoint * 5;
        m_StatsData.Speed = m_StatsPoint.m_iStatsLuckeyPoint + m_StatsPoint.m_iStatsStrengthPoint ;
        m_StatsData.Critical = m_StatsPoint.m_iStatsLuckeyPoint * 2;
    }

    public void ChangeAdditionalStats(float hp = 0, int mp = 0, int Defence = 0, int AttackDamage = 0, int Speed = 0, int Critical = 0)
    {
        
    }
    public void SaveStatsData()
    {
        SaveLoadManager.Instance.SavePlayerStatsData(m_StatsData,m_StatsPoint);
    }
    public void LoadStatsData(PlayerStatsData statsData,StatsPoint statsPoint)
    {
        m_StatsData = statsData;
        m_StatsPoint = statsPoint;
        m_UISet.UpdateUI(m_StatsData, m_StatsPoint);
    }
    public void UseStatsPoint(int type)
    {
        switch((StatsPointType)type)
        {
            case StatsPointType.Strength:
                m_StatsPoint.m_iStatsStrengthPoint++;
                break;
            case StatsPointType.Health:
                m_StatsPoint.m_iStatsHealthPoint++;
                break;
            case StatsPointType.Int:
                m_StatsPoint.m_iStatsIntelligencePoint++;
                break;
            case StatsPointType.Luk:
                m_StatsPoint.m_iStatsLuckeyPoint++;
                break;
        }
        m_StatsPoint.m_iRemainStatsPoint--;
        ChangeStatsPointToStatsData();
        m_UISet.UpdateUI(m_StatsData, m_StatsPoint);
        if (m_StatsPoint.m_iRemainStatsPoint <= 0)
        {
            m_UISet.UpdateStatsPointUpUI(false);
            return;
        }
    }
    public void StatsPointUp(int count = 5)
    {
        m_StatsPoint.m_iRemainStatsPoint += count;
        m_UISet.UpdateUI(m_StatsData, m_StatsPoint);
        m_UISet.UpdateStatsPointUpUI(true);
    }

}
