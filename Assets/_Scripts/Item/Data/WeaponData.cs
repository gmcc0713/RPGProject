using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Mace,
    Axe
}

public class WeaponData : EquipmentData
{
    public WeaponType weaponType;       //무기종류(검,활)
    public float AttackDamage;          //공격 데미지

    public void SetWeaponData(string[] datas)
    {
        weaponType = System.Enum.Parse<WeaponType>(datas[1]);
        AttackDamage = float.Parse(datas[2]);
        
    }
}