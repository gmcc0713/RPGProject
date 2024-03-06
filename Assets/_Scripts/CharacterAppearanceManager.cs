using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AppearanceType
{
    Axe = 0,
    Mace,
    Sword
}

public class CharacterAppearanceManager : MonoBehaviour
{
    [SerializeField] public GameObject[] charecterEquipmentParts = new GameObject [(int)EquipmentType.Count];
    public int[] charecterPartsCurIndexs = new int[(int)EquipmentType.Count];
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        for (int i = 0; i < charecterPartsCurIndexs.Length;i++)
        {
            charecterPartsCurIndexs[i] = 0;
        }
    }
    public void ChangeParts(EquipmentType type,int appearanceID)
    {
        int EquipmentIndex = (int)type;
        charecterEquipmentParts[EquipmentIndex].transform.GetChild(charecterPartsCurIndexs[EquipmentIndex]).gameObject.SetActive(false);
        charecterEquipmentParts[EquipmentIndex].transform.GetChild(appearanceID).gameObject.SetActive(true);
        charecterPartsCurIndexs[EquipmentIndex] = appearanceID;
    }
}
