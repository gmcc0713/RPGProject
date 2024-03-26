using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BossRockBullet : MonoBehaviour,IPoolingObject
{
    protected float m_fDamage;

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            RemoveStone();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            ThirdPersonMovement.Instance.GetDamaged(m_fDamage);
            RemoveStone();
        }
    }
    public virtual void RemoveStone() { } 
}
