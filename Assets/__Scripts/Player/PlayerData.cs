using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    [SerializeField] string m_sUserName;
    [SerializeField] string m_sJob;
    [SerializeField] string m_sTitle;
    [SerializeField] int m_LV;
    [SerializeField] float m_curEXP;
    [SerializeField] float m_EXP;

    public string _sUserName => m_sUserName;
    public string _sJob => m_sJob;
    public string _sTitle => m_sTitle;
    public int _LV => m_LV;
    public float _curEXP => m_curEXP;
    public float _EXP => m_EXP;

    public void SetPlayerData(string name,string job,string title,int lv,int exp)
    {
        m_sUserName = name;
        m_sJob = job;
        m_sTitle = title;
        m_LV = lv;
        m_curEXP = 0;
        m_EXP = PlayerDataManager.Instance.m_EXPValueByLevel[lv - 1];
    }
    public void AddExp(float exp)
    {
        m_curEXP += exp;
        if(m_curEXP>=m_EXP)
        {
            m_LV++;
            m_EXP = PlayerDataManager.Instance.m_EXPValueByLevel[m_LV - 1];
            m_curEXP = 0;
            ThirdPersonMovement.Instance.LVUP();
        }
    }

    public void LVUP()
    {
        UIManager.Instance.UpdatePlayerData(m_sUserName, m_sJob, m_sTitle, m_LV);
    }
}
