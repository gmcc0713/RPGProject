using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private GameObject movebar;
    [SerializeField] private TextMeshProUGUI inventoryGold;

    public void Initialize()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i]._slotNum = i;
        }
    }
    public int FindFrontEmptySlotIndex()    //���° ������ ����ִ� ��������
    {
        for(int i = 0; i<inventorySlots.Count;i++)
        {
            if (inventorySlots[i]._isEmpty)       //������ ���������
            {
                return i;
            }
        }
        return -1;

    }
    public void UpdateSlotUI(Item newitem)    //���� ������Ʈ
    {
        if (newitem.isEquipment)
        {
            inventorySlots[newitem.slotNum].SetItemUI(newitem.id);
            return;
        }
        if (newitem is CountableItem cItem)
            inventorySlots[cItem.slotNum].SetItemUI(cItem.id, cItem.amount);
        
    }
 
    public void ClearSlot(int slotIndex)    //���� ����
    {
        if (inventorySlots[slotIndex])
        inventorySlots[slotIndex].SetEmpty();
    }
    public bool EmptyCheck(int index)
    {
        return inventorySlots[index]._isEmpty;
    }
    public void UpdateGoldUI(int gold)
    {
        inventoryGold.text = gold.ToString();
    }
}
