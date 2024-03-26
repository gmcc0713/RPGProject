using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack State", menuName = "ScriptableObject/FSM State/Attack", order = 3)]
public class AttackState : ScriptableObject, IState
{
    public void Enter(BaseEnemy owner)
    {
        owner.RotateToTarget(RotateType.RotToTarget);
        owner.StopMove();
    }
    public void Excute(BaseEnemy owner)
    {
        owner.AttackTarget();
    }
    public void Exit(BaseEnemy owner)
    {

    }
};
