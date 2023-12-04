using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //=============================================================
    public static QuestManager Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    //=============================================================
    [SerializeField] private List<Quest_Data> m_lQuestDatas;
    
    [SerializeField] private QuestLoader m_questLoader;
     
    void Start()
    {
        m_lQuestDatas = new List<Quest_Data>();
        m_questLoader.RewardLoad();
        m_lQuestDatas.AddRange(m_questLoader.LoadQuests());
    }
    public Quest_Data GetQuestData(int questID)
    {
        return m_lQuestDatas[questID];
    }

}
