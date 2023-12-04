using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IState
{
    public void Enter(Enemy owner);
    public void Excute(Enemy owner);
    public void Exit(Enemy owner);
}
public class StateData : ScriptableObject
{
    public IState IdleState { get; private set; }
    public IState MoveState { get; private set; }
    public IState AttackState { get; private set; } 
    public IState ReturnPosState { get; private set; }
    public IState DieState { get; private set; }
    public void SetData(IState idle, IState move, IState attack, IState returnPos,IState die)
    {
        IdleState = idle;
        MoveState = move;
        AttackState = attack;
        ReturnPosState = returnPos;
        DieState = die;
    }

}



