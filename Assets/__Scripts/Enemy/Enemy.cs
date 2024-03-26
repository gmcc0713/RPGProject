using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public enum RotateType { RotToTarget,RotToSpawnPos}
public class Enemy :BaseEnemy
{

    public override void AttackTarget()
    {
        StopMove();
        RotateToTarget(RotateType.RotToTarget);

        animator.SetBool("IsAttack",true);
        if (DistanceToTarget() > m_cEnemyData._attackRange)
        {
            animator.SetBool("IsAttack", false);
            fsm.ChangeState(stateData.MoveState);
        }
        if(m_bCanAttack)
        {
            target.GetComponent<ThirdPersonMovement>().GetDamaged(50);
            StartCoroutine(AttackDelay());
        }
    }
   
}
