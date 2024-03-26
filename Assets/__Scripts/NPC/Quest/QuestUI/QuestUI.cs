using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{

    [SerializeField] private GameObject m_QuestInfo;
    [SerializeField] TextMeshProUGUI m_Title;
    [SerializeField] TextMeshProUGUI m_Info;
    [SerializeField] TextMeshProUGUI m_RequireText;
    [SerializeField] TextMeshProUGUI m_Reward;
    [SerializeField] Button m_ClearQuestBtn;
    [SerializeField] private QuestAnnounceSetting m_questAnnounceSetting;
    public QuestAnnounceSetting _questAnnounceSetting => m_questAnnounceSetting;

    [SerializeField] private List<QuestNode> questNodes;
    [SerializeField] private ObjectPool<QuestNode> pools;
    [SerializeField] private Transform parent;
    private int m_iQuestIdx;

    public void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        pools.Initialize();
    }

    public void QuestAddAndMakeQuestNode(int idx)
    {
        QuestNode node = new QuestNode();
        pools.GetObject(out node);

        node.Initialize();
        node.transform.SetParent(parent);
        node.SetQuestIdx(idx);
        questNodes.Add(node);
    }
    public void SetQuestInfoData(Quest data)
    {
       
         //m_questNode = data;
         m_Title.text = data._sData.m_sTitle;
         m_Info.text = data._sData.m_Description;
        
        
         m_RequireText.text = data.GetRequireText();
         m_RequireText.text = AddString(m_RequireText.text, "   " ,data._iProgressCount.ToString());
         m_RequireText.text = AddString(m_RequireText.text, " / ", data._sData.m_sRequire.count.ToString());

        m_Reward.text = AddString("<color=green>XP</color>         ", data._sData.m_sReward.rewardXp.ToString());
        m_Reward.text = AddString(m_Reward.text,"\n<color=yellow>GOLD</color>     ", data._sData.m_sReward.rewardGold.ToString());
        m_ClearQuestBtn.gameObject.SetActive(false);
        if (data._eState == Quest_State.Clear)
        {
            m_ClearQuestBtn.gameObject.SetActive(true);
        }
    }

    public string AddString(string str1, string str2, string str3 = "")
    {
        System.Text.StringBuilder baseString = new System.Text.StringBuilder(str1);
        baseString.Append(str2);
        baseString.Append(str3);
        return baseString.ToString();
    }
    public void ClickClearBtn()
    {
        QuestManager.Instance.ClearQuest(m_iQuestIdx);
        gameObject.SetActive(false);
    }
    public void UpdateAnnounce(string title, int curcount, int maxcount)
    {
        System.Text.StringBuilder baseString = new System.Text.StringBuilder(title);
        baseString.Append("   (");
        baseString.Append(curcount);
        baseString.Append("  /  ");
        baseString.Append(maxcount);
        baseString.Append(")");
        m_questAnnounceSetting.AddQuestAnnounceText(baseString.ToString());
    }
    public void OpenQuestInfo(int idx)
    {
        m_QuestInfo.SetActive(true);
        m_iQuestIdx = idx;
        SetQuestInfoData(QuestManager.Instance.GetQuestByIdx(idx));
    }
    public void RemoveQuest()
    {
        QuestNode node = questNodes.Find(x => x._iQuestIdx == m_iQuestIdx);
        questNodes.Remove(node);
        pools.PutInPool(node);
    }
    public void ClearQuest()
    {
        QuestManager.Instance.ClearQuest(m_iQuestIdx);
    }
}
