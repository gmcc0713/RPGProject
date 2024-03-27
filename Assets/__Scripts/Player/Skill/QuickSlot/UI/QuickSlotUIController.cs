using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUIController : MonoBehaviour
{
   [SerializeField] private ItemQuickSlotUI[] m_quickSlotItems;
   [SerializeField] private SkillQuickSlotUI[] m_quickSlotSkills;
    
    public void CoolDownFillImage(QuickSlotType type,int slotNum,float fillAmount)
    {
        switch (type)
        {
            case QuickSlotType.Skill:
                m_quickSlotSkills[slotNum].SetCoolDownFillImage(fillAmount);
                break;
            case QuickSlotType.Item:
                m_quickSlotItems[slotNum].SetCoolDownFillImage(fillAmount);
                break;
        }


    }
    public void CoolDownTextUpdate(QuickSlotType type,int slotNum, string curCooltime)
    {
        switch (type)
        {
            case QuickSlotType.Skill:
                m_quickSlotSkills[slotNum].SetCoolDownText(curCooltime);
                break;
            case QuickSlotType.Item:
                m_quickSlotItems[slotNum].SetCoolDownText(curCooltime);
                break;
        }

    }


    public void UpdateQuickSlot(QuickSlotType type,int slotNum,int id,int amount)
    {
        switch (type)
        {
            case QuickSlotType.Skill:
                m_quickSlotSkills[slotNum].SetUI(id);
                break;
            case QuickSlotType.Item:
                m_quickSlotItems[slotNum].SetItemUI(id, amount);
                break;
        }

    }

    //퀵슬롯창에서 아이템이 삭제되었을때
    public void RemoveItem(int slotNum)
    {
        m_quickSlotItems[slotNum].SetEmpty();
    }
   
}
