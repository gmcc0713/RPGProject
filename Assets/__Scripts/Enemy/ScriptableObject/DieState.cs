using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Die State", menuName = "ScriptableObject/FSM State/Die", order = 5)]
public class DieState : ScriptableObject, IState
{
    public void Enter(BaseEnemy owner)
    {
        owner.SetDieState();

    }
    public void Excute(BaseEnemy owner)
    {
        owner.Die();
    }
    public void Exit(BaseEnemy owner)
    {

    }
}
