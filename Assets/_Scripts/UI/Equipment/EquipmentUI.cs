using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> equipmentUISlots;
    [SerializeField] private ItemToolTip baseItemToolTip;
    public void Initialize()
    {
        int size = equipmentUISlots.Count;
        for(int i =0;i<size;i++)
        {
            equipmentUISlots[i]._slotNum = i;
        }
    }
    private void Equipping(Item item)
    {

    }
    public void UpdateItem(int num, int slotIndex)
    {

        equipmentUISlots[slotIndex].SetItemUI(num);
    }
    public void ClearEquipmentSlot(int slotIndex)
    {
        equipmentUISlots[slotIndex].SetEmpty();

    }
    public bool EmptyCheckSlot(int num)
    {
        return equipmentUISlots[num]._isEmpty;
    }
}
