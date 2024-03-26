using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    public List<int> m_EXPValueByLevel;

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

    public void Initialize()
    {
        m_EXPValueByLevel = new List<int>();
        LoadEXPData();
    }
    public void LoadEXPData()
    {
        List<string[]> expDatas = SCVLoadManager.Instance.Load("CSV/EXPDatas");
        for(int i =0;i<expDatas.Count;i++)
        {
            m_EXPValueByLevel.Add(int.Parse(expDatas[i][1]));
        }
    }
    public void LoadPlayerData()
    {

    }
}
