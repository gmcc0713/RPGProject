using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : Quest
{
    public override void QuestUpdate(Quest_Type type, int id, int count)
    {

    }

    public override void SetData(Quest_Data data)
    {
        m_sData = data;
    }
    public override void ClearQuest()
    {

    }
}
