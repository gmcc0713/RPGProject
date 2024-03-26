using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNode : MonoBehaviour,IPoolingObject
{
    [SerializeField] TextMeshProUGUI questTitle;

    private int m_iQuestIdx;
    public int _iQuestIdx => m_iQuestIdx;
    public void SetPosition(Vector3 pos)
    {

    }
    public void SetQuestIdx(int idx)
    {
        m_iQuestIdx = idx;
        
        questTitle.text = QuestManager.Instance.GetQuestDataByIdx(m_iQuestIdx).m_sTitle;
    }
    public void Initialize()
    {
        questTitle.text = QuestManager.Instance.GetQuestDataByIdx(m_iQuestIdx).m_sTitle;
    }
    public void OpenInfoUI()
    {
        UIManager.Instance._QuestUI.OpenQuestInfo(m_iQuestIdx);
    }
}
