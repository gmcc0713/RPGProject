using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSystem : MonoBehaviour
{
   [SerializeField] GameObject m_AttackCollider;

    public void Attack()
    {
        m_AttackCollider.SetActive(true);
    }

    public void AttackEnd()
    {
        m_AttackCollider.SetActive(false);
    }
}
