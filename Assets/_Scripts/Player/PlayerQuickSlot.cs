using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuickSlot : MonoBehaviour
{
   [SerializeField] private QuickSlot[] m_quickSlotItems;

    private void Update()
    {
        foreach (QuickSlot slot in m_quickSlotItems)
        {
            slot.IsPressedKey();

        }

    }
    public void UpdateQuickSlot(int slotNum)
    {
        foreach(InventorySlot slot in m_quickSlotItems)
        {
            if(!slot._isEmpty && slot._slotNum == slotNum)
            {
                slot.UpdateSlot(slotNum);
            }
        }
    }
    public void RemoveItem(int slotNum)
    {
        m_quickSlotItems[slotNum].SetEmpty();
    }
    public void SetQuickSlot(int num,int ivNum)
    {
        m_quickSlotItems[num].SetIVSlotNum(ivNum);
    }
}
