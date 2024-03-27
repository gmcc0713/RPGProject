using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
   [SerializeField] private ParticleSystem m_warriorBuff;
    [SerializeField] private Animator m_animator;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
    }
    public void SkillUse(int id)
    {
        m_animator.SetInteger("SkillNum", id);
        switch (id)
        {
            case 1:
                WarriorBuffSkillID1();
                break;
        }

    }
    public void WarriorBuffSkillID1()
    {

        //m_warriorBuff.Play();
        ThirdPersonMovement.Instance.Buff();
    }
}
