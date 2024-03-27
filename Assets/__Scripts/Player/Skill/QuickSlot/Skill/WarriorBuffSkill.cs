using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBuffSkill : Skill
{
    public Transform m_target;
    
    public override void Use()
    {
        ThirdPersonMovement.Instance._PlayerSkill.SkillUse(1);
    }
}
