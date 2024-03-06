using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public enum StatsPointType
{
    Strength = 0,
    Health,
    Int,
    Luk,

}

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI []m_PlayerData;
    [SerializeField] private TextMeshProUGUI []m_StatsTexts;
    [SerializeField] private TextMeshProUGUI [] m_StatsPointTexts;
    [SerializeField] private Button [] m_StatPointUpBtns;

   public void Initalize()
    {
    }
    public void PlayerDataUpdate(PlayerData data)
    {
        m_PlayerData[0].text = data._sUserName;
        m_PlayerData[1].text = data._LV.ToString();
        m_PlayerData[2].text = data._sJob;
        m_PlayerData[3].text = data._sTitle;
    }
    public void UpdateStatsPointUpUI(bool b)
    {
        foreach (Button btn in m_StatPointUpBtns)
        {
            btn.gameObject.SetActive(b);
        }
    }
    public void ClickStatsPointBtn()
    {
        
    }
    public void UpdateStatsDataUI(PlayerStatsData statsData)
    {
        m_StatsTexts[0].text = statsData.HP.ToString();
        m_StatsTexts[1].text = statsData.MP.ToString();
        m_StatsTexts[2].text = statsData.AttackDamage.ToString();
        m_StatsTexts[3].text = statsData.Defence.ToString();
        m_StatsTexts[4].text = statsData.Speed.ToString();
        m_StatsTexts[5].text = statsData.Critical.ToString();
    }
    public void UpdateStatsPointUI(StatsPoint statsPoint)
    {
        m_StatsPointTexts[0].text = statsPoint.m_iStatsStrengthPoint.ToString();
        m_StatsPointTexts[1].text = statsPoint.m_iStatsHealthPoint.ToString();
        m_StatsPointTexts[2].text = statsPoint.m_iStatsIntelligencePoint.ToString();
        m_StatsPointTexts[3].text = statsPoint.m_iStatsLuckeyPoint.ToString();
        m_StatsPointTexts[4].text = statsPoint.m_iRemainStatsPoint.ToString();

     
    }
    public void UpdatePlayerInfoUI(string name, string job, string title, int Lv)
    {
        m_PlayerData[0].text = name;
        m_PlayerData[1].text = Lv.ToString();
        m_PlayerData[2].text = job;
        m_PlayerData[3].text = title;
    }
    public void UpdateUI(PlayerStatsData statsData,StatsPoint statsPoint)
    {
        UpdateStatsDataUI(statsData);
        UpdateStatsPointUI(statsPoint);
    }

}
