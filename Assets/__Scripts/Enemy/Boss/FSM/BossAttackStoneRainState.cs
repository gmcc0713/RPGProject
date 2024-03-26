using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossAttackStoneRainState", menuName = "ScriptableObject/FSM State/Boss/BossAttackStoneRainState", order = 2)]
public class BossAttackStoneRainState : ScriptableObject, IState
{
    public void Enter(BaseEnemy owner)
    {
        if (owner is BossEnemy bOwner)
        {
            bOwner.RotateToTarget(RotateType.RotToTarget);
            bOwner.StopMove();
            bOwner.BossAttackRockRainRun();

        }
        //돌 던지기
    }
    public void Excute(BaseEnemy owner)
    {
        //돌 던지고 상태 바뀌기 까지 딜레이
    }
    public void Exit(BaseEnemy owner)
    {

    }
}
