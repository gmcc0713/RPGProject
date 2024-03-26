using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle State", menuName = "ScriptableObject/FSM State/Idle", order = 1)]
public class IdleState : ScriptableObject, IState
{
    public void Enter(BaseEnemy owner)
    {
        owner.StopMove();
    }
    public void Excute(BaseEnemy owner)
    {
        owner.SearchTarget();
    }
    public void Exit(BaseEnemy owner)
    {

    }
};
