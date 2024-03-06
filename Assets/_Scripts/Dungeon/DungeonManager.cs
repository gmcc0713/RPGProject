using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    //=============================================================
    [SerializeField] private int [] m_StageMonsterCount;
    private int m_DungeonMonsterCount;
    private int m_curRemainMonsterCount;

    [SerializeField] private TextMeshProUGUI m_countText;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        Debug.Log("Init");
        m_DungeonMonsterCount = 0;
        foreach (int count in m_StageMonsterCount)
        {
            m_DungeonMonsterCount += count;
        }
        m_curRemainMonsterCount = m_DungeonMonsterCount;
        UpdateMonsterCount();
    }
    public void DieMonster()
    {
        m_curRemainMonsterCount--;
        UpdateMonsterCount();
    }
    public void UpdateMonsterCount()
    {
        string s = m_curRemainMonsterCount.ToString() + " / " + m_DungeonMonsterCount.ToString(); 
        m_countText.text = s;
    }
}

