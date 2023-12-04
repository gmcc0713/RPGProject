using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private float weaponDamage = 50;
    public float _weaponDamage => weaponDamage;
    public int _weaponCombo => weaponCombo;
    private int weaponCombo;
    public bool isAttack;



    public void AttackComboSet(int comboNum)        //공격을 할때의콤보설정
    {
        weaponCombo = comboNum;
    }
    public bool AttackComboCheck(int comboNum)      //몬스터가 이전에 받았던 콤보와 현재의 콤보가 같은지 확인(한번의 콤보에두번공격 당하는 것 방지)
    {

        if(isAttack&&comboNum != weaponCombo)
        {
                return true;
        }
        return false;

        
    }
}
