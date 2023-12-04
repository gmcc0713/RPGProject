using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType {Turtle = 0,Slime = 1 }
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    [SerializeField] private List<EnemyData> enemyData = new List<EnemyData>();

    public void EnemyInit(Enemy enemy)
    {
        
        IState idle = (IState)Resources.Load("ScriptableObject/EnemyState/IdleState");
        IState move = (IState)Resources.Load("ScriptableObject/EnemyState/MoveState");
        IState attack = (IState)Resources.Load("ScriptableObject/EnemyState/AttackState");
        IState returnPos = (IState)Resources.Load("ScriptableObject/EnemyState/ReturnPosState");
        IState die = (IState)Resources.Load("ScriptableObject/EnemyState/DieState");
        StateData data = ScriptableObject.CreateInstance<StateData>();
        data.SetData(idle, move, attack, returnPos,die);

        enemy.SetData(data,enemyData[(int)enemy._type]);
    }
    public void EnemyStateDataInit()
    {

    }
}
