using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private Slider m_PlayerHP;
    [SerializeField] private Slider m_PlayerMP;
    [SerializeField] private Slider m_PlayerEXP;
    [SerializeField] private TextMeshProUGUI m_PlayerName;


    public void SetPlayerHPUI(float value)
    {
        m_PlayerHP.value = value;
    }
    public void SetPlayeMPUI(float value)
    {
        m_PlayerMP.value = value;
    }
    public void SetPlayerEXPUI(float value)
    {
        m_PlayerEXP.value = value;
    }
    // Update is called once per frame
    public void SetNameUI(string data)
    {
        m_PlayerName.text = data;
    }
    public void UpdateUI(float HPValue, float MPValue, float EXPValue,string name)
    {
        m_PlayerMP.value = MPValue;
        m_PlayerHP.value = HPValue;
        m_PlayerEXP.value = EXPValue;
        m_PlayerName.text = name;
    }
}
