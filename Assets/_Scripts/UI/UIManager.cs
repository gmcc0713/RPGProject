    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class UIManager : MonoBehaviour
{
    //================== ½Ì±ÛÅæ==========================================
    public static UIManager Instance { get; private set; }

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
    public Slider mouseSensivitySlider;
    public CinemachineFreeLook cinemachineFreeLook;


    [SerializeField] private InventoryUI m_InventoryUI;
    [SerializeField] private EquipmentUI m_EquipmentUI;
    [SerializeField] private StatsUI m_StatsUI;
    [SerializeField] private PlayerDataUI m_PlayerDataUI;
    [SerializeField] private QuestUI m_QuestUI;

    public QuestUI _QuestUI => m_QuestUI;
    void Start()
    {
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = mouseSensivitySlider.value * 300f;

        m_InventoryUI.Initialize();
        m_EquipmentUI.Initialize();
        m_StatsUI.Initalize();
        //m_PlayerDataUI.Initialize();
    }

    // Update is called once per frame

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void ChangeMouseSensivity()
    {
      cinemachineFreeLook.m_XAxis.m_MaxSpeed =100 +  mouseSensivitySlider.value* 200f;
    }
    public void SetCanAct(bool act)
    {
        GameMgr.Instance._player.SetCanAct(act);
    }
    public void UpdatePlayerData(string name, string job, string title, int Lv)
    {
        m_StatsUI.UpdatePlayerInfoUI(name,job,title,Lv);
    }
    public void QusetInfoSet(Quest data)
    {
        m_QuestUI.SetQuestInfoData(data);
    }
    public void QusetAnnounceUIUpdate()
    {

    }
}
