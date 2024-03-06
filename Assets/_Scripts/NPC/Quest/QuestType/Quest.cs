using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    protected Quest_State m_eState;
    protected Quest_Data m_sData;
    protected int m_iProgressCount;
    public int _iProgressCount => m_iProgressCount;
    public Quest_Data _sData => m_sData;
    public Quest_State _eState => m_eState;
    public void Start()
    {
        m_eState = Quest_State.Begin;
        m_iProgressCount = 0;
    }
    public bool IsClearQuest()
    {
        return m_sData.isClear;
    }
    public void SetQuestState(Quest_State state) { m_eState = state; }
    public void CountAdd(int count)
    {
        m_iProgressCount += count;
    }
    public bool QuestIDCheck(int id)
    {
        if (m_sData.m_iQuestID == id)
        {
            return true;
        }
        return false;
    }
    protected void update(Quest_Type type, int id, int count)
    {
        if (CheckQuestUpdate(type, id))
        {
            Debug.Log("QuestUpdate");
            QuestUpdate(type, id, count);
        }
    }
    public virtual void SetData(Quest_Data data)
    {
        m_sData = data;
    }
    protected bool CheckQuestUpdate(Quest_Type type, int id)
    {
        if (type != m_sData.m_eType || id != m_sData.m_iQuestID)
        {
            return false;
        }
        return true;
    }

    public void UpdateQuest(Quest_Type type,int id,int amount)
    {
        if (m_sData.m_eType != type || m_eState == Quest_State.Clear)
        {
            return;
        }
        if (id == m_sData.m_sRequire.id)
        {
            m_iProgressCount += amount;
            UIManager.Instance._QuestUI.UpdateAnnounce(m_sData.m_sTitle,m_iProgressCount,m_sData.m_sRequire.count);
            if (m_iProgressCount >= m_sData.m_sRequire.count)
            {
                m_eState = Quest_State.Clear;
            }
        }

    }

    public virtual void QuestUpdate(Quest_Type type, int id, int count) { }

    public virtual string GetRequireText() { return ""; }

    public virtual void ClearQuest() { }
}
