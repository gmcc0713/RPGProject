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



    public void AttackComboSet(int comboNum)        //������ �Ҷ����޺�����
    {
        weaponCombo = comboNum;
    }
    public bool AttackComboCheck(int comboNum)      //���Ͱ� ������ �޾Ҵ� �޺��� ������ �޺��� ������ Ȯ��(�ѹ��� �޺����ι����� ���ϴ� �� ����)
    {

        if(isAttack&&comboNum != weaponCombo)
        {
                return true;
        }
        return false;

        
    }
}
