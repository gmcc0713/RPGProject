using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryManager : MonoBehaviour
{
    //================== ΩÃ±€≈Ê==========================================
    public static InventoryManager Instance { get; private set; }

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    //====================================================================
    [SerializeField] private GameObject inventorySlot;
    [SerializeField] private Transform slotPos;

    public ItemSlotUI MakeInventorySlot(int itemNum, int itemAmount)
    {
        GameObject clone = Instantiate(inventorySlot, slotPos);
        ItemSlotUI slot = clone.GetComponent<ItemSlotUI>();

        return slot;
    }

}
