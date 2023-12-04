using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack State", menuName = "ScriptableObject/FSM State/Attack", order = 3)]
public class AttackState : ScriptableObject, IState
{
    public void Enter(Enemy owner)
    {
        owner.RotateToTarget(RotateType.RotToTarget);
        owner.StopMove();
    }
    public void Excute(Enemy owner)
    {
        owner.AttackTarget();
    }
    public void Exit(Enemy owner)
    {

    }
};
