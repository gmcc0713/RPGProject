using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
     [SerializeField]  private int m_weaponDamage;
    [SerializeField] private int m_playerDamage;
    [SerializeField] private int m_critical;

    
    public void SetPlayerDamage(int playerDamage)
    {
        m_playerDamage = playerDamage;
    }
    public void SetWeaponDamage(int weaponDamage)
    {
        m_weaponDamage = weaponDamage;
    }
    public void SetCritical(int critical)
    {
        m_critical = critical;
        
    }
    public int CalculateDamage()
    {
        int sumDamage = 0;
        if (Random.Range(0, 100) < m_critical)
        {
            sumDamage += m_playerDamage;
        }
        sumDamage += m_playerDamage;
        sumDamage += m_weaponDamage;
        return sumDamage;
    }
    
}
