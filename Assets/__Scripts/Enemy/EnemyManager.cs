using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EnemyType {Turtle = 0,Slime = 1, Skeleton = 2, Golam = 3}
[Serializable]
public struct DropItemData
{
    public int itemID;
    public int dropPercent;
}
[Serializable]
public struct EnemyDropItemDatas
{
    public List<DropItemData> dropItems;
    public int gold;
    public int exp;

}

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

    [SerializeField] private List<EnemyData> m_enemyDatas = new List<EnemyData>();
    [SerializeField] public List<EnemyData> _enemyData => m_enemyDatas;
    [SerializeField] DropItemLoader dropitemLoader;
    [SerializeField]  List<string[]> enemyDatas;
    private void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        LoadMonsterDatas();
    }
    void LoadMonsterDatas()
    {
        enemyDatas = SCVLoadManager.Instance.Load("CSV/Enemy/MonsterData");
        for (int i =0;i<enemyDatas.Count;i++)
        {

            EnemyData data = new EnemyData();
            data.SetData(enemyDatas[i]);
            m_enemyDatas.Add(data);

        }
    }
    public EnemyData GetEnemyData(int enemyID)
    {
        return m_enemyDatas[enemyID];
    }
    public void EnemyInit(BaseEnemy enemy)
    {
        IState idle = (IState)Resources.Load("ScriptableObject/EnemyState/IdleState");
        IState move = (IState)Resources.Load("ScriptableObject/EnemyState/MoveState");
        IState attack = (IState)Resources.Load("ScriptableObject/EnemyState/AttackState");
        IState returnPos = (IState)Resources.Load("ScriptableObject/EnemyState/ReturnPosState");
        IState die = (IState)Resources.Load("ScriptableObject/EnemyState/DieState");
        IState bossAttack1 = (IState)Resources.Load("ScriptableObject/EnemyState/BossAttackStoneThrowState");
        IState bossAttack2 = (IState)Resources.Load("ScriptableObject/EnemyState/BossAttackFootAttackState");
        IState bossAttack3 = (IState)Resources.Load("ScriptableObject/EnemyState/BossAttackStoneRainState");

        StateData data = ScriptableObject.CreateInstance<StateData>();

        data.SetData(idle, move, attack, returnPos,die,bossAttack1,bossAttack2,bossAttack3);
        enemy.SetData(data, m_enemyDatas[(int)enemy._EnemyData._type]);
    }
    public EnemyDropItemDatas GetEnemyDropItemDatas(int dropID)
    {
        return dropitemLoader.GetDropItemDatas(dropID);
    }
}
