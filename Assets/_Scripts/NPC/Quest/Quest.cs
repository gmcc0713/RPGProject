using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Quest_Data
{
    public int questID;
    public string title;
    public string description;
    public List<string[]> dialog;
    public Quest_Reward rewards;
}
public struct Quest_Reward
{
     public int rewardGold;
     public int rewardXp;
     public Dictionary<int, int> rewardItem;    //æ∆¿Ã≈€ id,∞πºˆ
}
public class Quest : MonoBehaviour
{
    [SerializeField] private Quest_Data quest;

    [SerializeField] private NPCType m_eNPCType;
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {

    }
    public void ClearQuest()
    {

       
    }
}
