using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillQuickSlot : QuickSlot
{

    public override void Initialize(int i, string a)
    {
        m_iSlotNum = i;
        fillAmountValue = 0;
        m_bIsEmpty = true;
        code = a;

    }
    public void SetSkillDataInQuickSlot(int skillID)           //�������� ó�� �����Կ� �����Ǿ�����(�������� ���������)
    {
        m_iID = skillID;
    }
    public override void IsPressedKey()
    {
        Debug.Log("��ų ������ ����");
        if (m_bIsEmpty || !m_bCanUse)
        {
            return;
        }
        if (Input.GetKeyDown(code))
        {
            m_bCanUse = false;
            SkillManager.Instance.SkillUse(m_iID);
        }

    }

    public override void CoolTimeRunTextUIUpdate()
    {
        UIManager.Instance._QuickSlotUI.CoolDownTextUpdate(QuickSlotType.Skill, m_iSlotNum, ((int)m_iCurItemCoolTime).ToString());

    }
    public override void CoolTimeRunFillUIUpdate()
    {
        UIManager.Instance._QuickSlotUI.CoolDownFillImage(QuickSlotType.Skill,m_iSlotNum, fillAmountValue);
    }
    public override void CoolTimeRunUIUpdateEnd()
    {
        m_bCanUse = true;
        UIManager.Instance._QuickSlotUI.CoolDownFillImage(QuickSlotType.Skill,m_iSlotNum, 0);
        UIManager.Instance._QuickSlotUI.CoolDownTextUpdate(QuickSlotType.Skill, m_iSlotNum, "");

    }
}

