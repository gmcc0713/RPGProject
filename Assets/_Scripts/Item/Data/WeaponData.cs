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
    public WeaponType weaponType;       //무기종류(검,활)
    public float AttackDamage;          //공격 데미지
}