using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUI : MonoBehaviour
{
   [SerializeField] private QuickItemSlotUI[] m_quickSlotItems;
    
    public void CoolDownFillImage(int slotNum,float fillAmount)
    {
        m_quickSlotItems[slotNum].SetCoolDownFillImage(fillAmount);
    }
    public void CoolDownTextUpdate(int slotNum, string curCooltime)
    {
        m_quickSlotItems[slotNum].SetCoolDownText(curCooltime);
    }

    public void UpdateQuickSlot(int slotNum,int id,int amount)
    {
        m_quickSlotItems[slotNum].SetItemUI(id, amount);
    }

    //������â���� �������� �����Ǿ�����
    public void RemoveItem(int slotNum)
    {
        m_quickSlotItems[slotNum].SetEmpty();
    }
   
}
