using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Weapon")]
public class WeaponData : EquipmentData
{
    public WeaponType weaponType;       //��������(��,Ȱ)
    public float AttackDamage;          //���� ������
}