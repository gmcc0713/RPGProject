using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private List<int> m_iNPCQuestID;
    public NPCData data;
    public NPCInteractionUI npcUI;
    public List<Quest_Data> quests;

    private void Start()
    {
        initialize();
    }
    public void initialize()
    {
        for (int i = 0; i < m_iNPCQuestID.Count; i++)
        {
           quests.Add(QuestManager.Instance.GetQuestData(m_iNPCQuestID[i]));
        }
    }

    private void OnTriggerEnter(Collider other)             //플레이어가 NPC의 범위 안에 들어갔을때
    {
        if (other.CompareTag("Player"))
        {
            npcUI.ShowPressInteractionKey();
            UIManager.Instance.ShowInteractionText();
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            npcUI.UpdateUI(data);
            UIManager.Instance.ShowPanel(dialogPanel);
        }
    }

    private void OnTriggerExit(Collider other)             //플레이어가 NPC의 범위 안에 들어갔을때
    {
        if (other.CompareTag("Player"))
        {
            npcUI.HidePressInteractionKey();
            UIManager.Instance.HideInteractionText();
        }
    }
}
