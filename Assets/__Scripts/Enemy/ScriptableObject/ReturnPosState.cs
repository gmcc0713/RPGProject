using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReturnPos State", menuName = "ScriptableObject/FSM State/ReturnPos", order = 4)]
public class ReturnPosState : ScriptableObject, IState
{
    public void Enter(BaseEnemy owner)
    {
        owner.StartMove();
        owner.RotateToTarget(RotateType.RotToSpawnPos);
    }
    public void Excute(BaseEnemy owner)
    {
        Debug.Log("return");
        owner.ReturnDefaultPos();
    }
    public void Exit(BaseEnemy owner)
    {

    }
};