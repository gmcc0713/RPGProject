using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossAttackStoneThrowState", menuName = "ScriptableObject/FSM State/Boss/BossAttackStoneThrowState", order = 0)]
public class BossAttackStoneThrowState : ScriptableObject, IState
{
    public void Enter(BaseEnemy owner)
    {
        Debug.Log("ThrowEnter");
        if(owner is BossEnemy bOwner)
        {
            bOwner.RotateToTarget(RotateType.RotToTarget);
            bOwner.StopMove();
            bOwner.RockLauncherRun();
            
        }

    }
    public void Excute(BaseEnemy owner)
    {
       if(owner is BossEnemy bOwner) 
       {
            
       }
    }
    public void Exit(BaseEnemy owner)
    {

    }
}
