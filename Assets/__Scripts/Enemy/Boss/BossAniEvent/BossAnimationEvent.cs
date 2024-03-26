using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvent : MonoBehaviour
{
    [SerializeField] ParticleSystem m_EnergyCharging;
    [SerializeField] ParticleSystem m_FootAttackSmoke;
    public void Charging(int i)
    {
        if (i == 0)
            m_EnergyCharging.Play();
        else if (i == 1)
            m_EnergyCharging.Stop();
    }
    public void FootAttackStart()
    {
        Debug.Log("파티클 시작");
        m_FootAttackSmoke.Play();
    }
}
