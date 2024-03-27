using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private List<SkillUI> m_skills;
    public void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        foreach (SkillUI skill in m_skills)
        {
            if(skill._beforeSkillTreeNum == -1)
            {
                skill.SetUI(false);
                continue;
            }
            skill.SetUI(m_skills[skill._beforeSkillTreeNum]._Locked || m_skills[skill._beforeSkillTreeNum]._skillType != SkillType.Active);
        }
       
    }
}
