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
    [SerializeField] private Button[] interactionbuttons;


    void Initialize()
    {
        interactionHelpText.SetActive(false);
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
    }

    public void ShowInteractionText()
    {
        interactionHelpText.SetActive(true);
    }
    public void HideInteractionText()
    { 
        interactionHelpText.SetActive(false);
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
    public void Quest()
    {
        
    }
}
