using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
   Locked,
   UnLocked,
   Active,
}
public class Skill : MonoBehaviour
{
    private int m_iSkillID;
    private Image m_SkillImage;
    private string m_sSkillName;
    private float m_fCoolTime;
    private int m_beforeSkillID;

    private int m_iActiveTime;

    public virtual void Use() { }

}
