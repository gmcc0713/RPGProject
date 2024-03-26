using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move State", menuName = "ScriptableObject/FSM State/Move", order = 2)]
public class MoveState : ScriptableObject, IState
{
    public void Enter(BaseEnemy owner)
    {
        owner.StartMove();
        owner.RotateToTarget(RotateType.RotToTarget);
      
    }
    public void Excute(BaseEnemy owner)
    {
        owner.MoveToTarget();
    }
    public void Exit(BaseEnemy owner)
    {

    }
};