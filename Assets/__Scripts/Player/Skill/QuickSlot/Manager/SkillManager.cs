using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    //==================================================================================
    List<Skill> m_SkillList;
    public void SkillUse(int id)
    {
        m_SkillList[id].Use();
    }
}
