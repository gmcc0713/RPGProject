using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private List<int> m_iNPCQuestID;
    [SerializeField] private NPCData data;
    [SerializeField] private NPCInteractionUI npcUI;

    [SerializeField] private int curQuestIndex;  
    //public List<Quest_Data> quests;

    [SerializeField] private List<Quest_Data> quests;
    public List<Quest_Data> _quests => quests;

    private void Start()
    {
        initialize();
    }
    public int GetQuestId()
    {
        return quests[curQuestIndex].m_iQuestID;
    }
    public void QuestUpdate()
    {
        if(quests[curQuestIndex].isClear && quests.Count> curQuestIndex-1)
        {
            curQuestIndex++;
        }
    }
    public void AcceptQuest()
    {
        //플레이어 레벨과 퀘스트 요구레벨 비교후 수락 안되게 설정
        if(!quests[curQuestIndex].isClear)
        {
            QuestManager.Instance.QuestAdd(quests[curQuestIndex].m_iQuestID);  //퀘스트 아이디로 퀘스트 수락
        }
       
    }
    public void AddQuest(Quest_Data data)
    {
        quests.Add(data);
    }
    public Quest_Data GetQuestData()
    {
        return _quests[curQuestIndex];
    }
    public void initialize()
    {
        quests = new List<Quest_Data>();
        for (int i = 0; i < m_iNPCQuestID.Count; i++)
        {
            quests.Add(QuestManager.Instance.GetQuestData(m_iNPCQuestID[i]));
        }
        curQuestIndex = 0;
    }
    //플레이어가 눌렀을 때로 변경
    //해당 충돌 하는 테그 확인 후 특정 행동 실행


    public void UseInterationKey()
    {
        npcUI.UpdateUI(data);
        UIManager.Instance.ShowPanel(dialogPanel);
        GameMgr.Instance._player.SetCanAct(false);
    }

}
