using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NPCType
{
    Hunter = 0,
    Smith,
}
public class NPCManager : MonoBehaviour
{
    //================== ΩÃ±€≈Ê==========================================
    public static NPCManager Instance { get; private set; }

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    //====================================================================
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private List<NPCController> m_NPCs;

    public int GetQuestID(int idx)
    {
        return m_NPCs[idx].GetQuestId();
    }
    public Quest_Data GetQuest(int idx)
    {
        return m_NPCs[idx].GetQuestData();
    }
    void Initialize()
    {

    }

}
