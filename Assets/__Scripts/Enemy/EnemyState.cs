using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IState
{
    public void Enter(BaseEnemy owner);
    public void Excute(BaseEnemy owner);
    public void Exit(BaseEnemy owner);
}
public class StateData : ScriptableObject
{
    public IState IdleState { get; private set; }
    public IState MoveState { get; private set; }
    public IState AttackState { get; private set; } 
    public IState ReturnPosState { get; private set; }
    public IState DieState { get; private set; }
    public IState BossStoneThrowState { get; private set; }
    public IState BossFootAttackState { get; private set; }
    public IState BossStoneRainState { get; private set; }
    public void SetData(IState idle, IState move, IState attack, IState returnPos,IState die, IState bossAttackThrowStone, IState bossAttackFoot, IState bossAttackStoneRain)
    {
        IdleState = idle;
        MoveState = move;
        AttackState = attack;
        ReturnPosState = returnPos;
        DieState = die;
        BossStoneThrowState = bossAttackThrowStone;
        BossFootAttackState = bossAttackFoot;
        BossStoneRainState = bossAttackStoneRain;
    }

}



