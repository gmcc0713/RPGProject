using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI talkText;
    [SerializeField] private Image npcImage;

    [SerializeField] private GameObject interactionHelpText;
    [SerializeField] private GameObject interactionBtn;
    [SerializeField] private GameObject m_QuestAcceptCancleBtn;
    [SerializeField] private GameObject m_QuestNextBtn;
    [SerializeField] private Button[] interactionbuttons;
    [SerializeField] private NPCType m_eNpcTypes;

    private int m_iDialogIndex;

    void Initialize()
    {
        interactionHelpText.SetActive(false);
        m_iDialogIndex = 0;
    }
    public void ShowPressInteractionKey()
    {
        UIManager.Instance.ShowPanel(interactionHelpText);

    }
    public void HidePressInteractionKey()
    {
        UIManager.Instance.HidePanel(interactionHelpText);

    }
    public void UpdateUI(NPCData data)
    {
        nameText.text = data._npcName;
        talkText.text = data._npcTalk;
        npcImage.sprite = data._npcImage;
        NPCInteractButtonSet(data._type);
        m_eNpcTypes = data._type;
    }

    public void ShowInteractionText()
    {
        interactionHelpText.SetActive(true);
    }
    public void HideInteractionText()
    { 
        interactionHelpText.SetActive(false);
        m_iDialogIndex = 0;
    }
    public void NPCInteractButtonSet(NPCType type)
    {
        interactionbuttons[0].gameObject.SetActive(true);
        switch (type)
        {
            case NPCType.Hunter:
                interactionbuttons[1].gameObject.SetActive(false);
                interactionbuttons[2].gameObject.SetActive(false);
                break;
            case NPCType.Smith:
                interactionbuttons[1].gameObject.SetActive(true);
                interactionbuttons[2].gameObject.SetActive(true);
                break;
        }
    }

    public void ClickQuest()
    {
        interactionBtn.SetActive(false);
        if (!QuestManager.Instance.CheckAlreadyClearQuest(NPCManager.Instance.GetQuestID((int)m_eNpcTypes)))
        {
            SetNPCQuestDialog(NPCManager.Instance.GetQuest((int)m_eNpcTypes));
            m_QuestNextBtn.SetActive(true);
        }
        else
        {
            talkText.text = NPCManager.Instance.GetQuest((int)m_eNpcTypes).m_arrAnnouncement[(int)Announcement_Type.Non];
            m_QuestNextBtn.SetActive(false);
        }
        

    }

    public void SetNPCQuestDialog(Quest_Data data)
    {
        talkText.text = data.m_lDialog[m_iDialogIndex];
        m_iDialogIndex++;
        if (data.m_lDialog.Count <= m_iDialogIndex)
        {
            m_QuestAcceptCancleBtn.SetActive(true);
            m_QuestNextBtn.SetActive(false);
            m_iDialogIndex = 0;
            return;
        }
    }
    public void QuestAccept()
    {
        QuestManager.Instance.QuestAddByNPCID((int)m_eNpcTypes);
        m_QuestAcceptCancleBtn.SetActive(false);
        talkText.text = NPCManager.Instance.GetQuest((int)m_eNpcTypes).m_arrAnnouncement[(int)Announcement_Type.Accept];
        //SetQuestState()
    }
    public void QuestRefuse()
    {
        m_QuestAcceptCancleBtn.SetActive(false);
        talkText.text = NPCManager.Instance.GetQuest((int)m_eNpcTypes).m_arrAnnouncement[(int)Announcement_Type.Refuse];
    }
    public void ClickNextDialog()
    {
        SetNPCQuestDialog(NPCManager.Instance.GetQuest((int)m_eNpcTypes));
    }
}
