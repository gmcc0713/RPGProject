using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public enum RotateType { RotToTarget,RotToSpawnPos}
public class Enemy : MonoBehaviour,IPoolingObject
{
    [SerializeField]private int EnemyID;
    private EnemyData m_cEnemyData;
    [SerializeField] EnemyDropItemDatas m_eEnemyDropItems;

    public EnemyData _EnemyData => m_cEnemyData;

    public NavMeshAgent agent;
    private EnemyFSM fsm;
    private StateData stateData = null;
    private EnemySpawner enemySpawner;
    private Animator animator;
    private float curHealth ;
    [SerializeField] private Slider enemyHealthBar;

    public bool isMoving;
    public Transform target;
    public Vector3 defaultPos;
    public Animator _animator => animator;
    [SerializeField] private bool m_bIsDamaged = false;
    private bool isDie = false;
    private bool m_bCanAttack = true;
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        m_bIsDamaged = false;

    }
    public void ResetData()
    {
        
        if (fsm != null)
        {
            fsm.ChangeState(stateData.IdleState);
        }
        if(m_cEnemyData == null)
        {
            m_cEnemyData = EnemyManager.Instance.GetEnemyData(EnemyID);
        }
        curHealth = m_cEnemyData._maxHealth;
        enemyHealthBar.value = 1;
        isDie = false;

    }
    public void SetPosition(Vector3 pos)
    {
        agent.gameObject.SetActive(false);
        transform.position = pos;
        agent.gameObject.SetActive(true);
    }
    public void SetSpawner (EnemySpawner spawner)           //ó�� ����
    {

        enemySpawner = spawner;

    }
    // Update is called once per frame
    public bool SetData(StateData data, EnemyData enemyData)            //���� �ʱ⿡ �ѹ� ������ ����
    {
        stateData = data;

        if (null == enemyData)
        {
            return false;
        }
        if (null == fsm) fsm = new EnemyFSM(this);
        if (!fsm.SetCurrState(stateData.IdleState))
        {
            return false;
        }

        isDie = false;
        gameObject.SetActive(true);

        defaultPos = transform.position;
        ResetData();

        m_eEnemyDropItems = EnemyManager.Instance.GetEnemyDropItemDatas(m_cEnemyData._dropItemID);
        return true;
    }

    public void Run()                           //update�ڷ�ƾ ����
    {
        gameObject.SetActive(true);
        StartCoroutine(OnUpdate());
    }
    IEnumerator OnUpdate()
    {
        target = GameObject.FindWithTag("Player").transform;
        while (true)
        {
            fsm.Update();
            
            yield return new WaitForSeconds(0.04f);
        }
    }

    public void SearchTarget()      // Ÿ���� ���� �ȿ� �ִ��� Ȯ��
    {
        if (DistanceToTarget() > 0 && DistanceToTarget() <= m_cEnemyData._searchRange)
        {
            fsm.ChangeState(stateData.MoveState);
        }
    }
    public float DistanceToTarget()     //Ÿ�ٰ��� �Ÿ� 
    {
        if (!target) return -1;
        return Vector3.Distance(target.position, transform.position);
    }
    public void MoveToTarget()          //Ÿ�ٿ��Է� �̵�
    {
        animator.SetBool("IsMoving",true);
        if (DistanceToTarget() < m_cEnemyData._attackRange)       //���ݻ�Ÿ� �ȿ� Ÿ���� ������
        {
            fsm.ChangeState(stateData.AttackState);
            return;
        }
        else if (DistanceToTarget() < m_cEnemyData._searchRange)  //Ž����Ÿ� �ȿ� Ÿ���� ������
        {
            agent.isStopped = false;
            
            agent.SetDestination(target.position);
            return;
        }
        fsm.ChangeState(stateData.ReturnPosState);  //Ž����Ÿ��� �������
    }
    public void RotateToTarget(RotateType type)     //Ÿ������ ȸ��
    {
        Vector3 tar = Vector3.zero;
        switch (type)
        {
            case RotateType.RotToTarget:
                tar = target.transform.position;
                break;
            case RotateType.RotToSpawnPos:
                tar = defaultPos;
                break;
        }
        Vector3 dir = Vector3.zero;
        if (tar == defaultPos)
            dir = tar;
        else
            dir = tar - transform.position;
        dir.y = 0f;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;
    }
    public void StopMove()      //������ ���߱�
    {
        animator.SetBool("IsMove", false);
        agent.isStopped = true;
        agent.ResetPath();
    }
    public void StartMove()
    {
        animator.SetBool("IsMove", true);
        agent.isStopped = false;
    }
    public void ReturnDefaultPos()      //������ �ڸ��� ���ư���
    {
        if (DistanceToTarget() > 0 && DistanceToTarget() <= m_cEnemyData._searchRange)    //Ÿ���� �����ȿ� ��������
        {
            fsm.ChangeState(stateData.MoveState);
            return;
        }
        else if (agent.remainingDistance > 1)       //������ �ڸ��� ���ư��� ���϶�
        {
            agent.SetDestination(defaultPos);
            return;
        }
        fsm.ChangeState(stateData.IdleState);
        
        
    }
    public void AttackTarget()
    {
        StopMove();
        RotateToTarget(RotateType.RotToTarget);
        animator.SetBool("IsAttack",true);
        if (DistanceToTarget() > m_cEnemyData._attackRange)
        {
            animator.SetBool("IsAttack", false);
            fsm.ChangeState(stateData.MoveState);
        }
        if(m_bCanAttack)
        {
            target.GetComponent<ThirdPersonMovement>().GetDamaged(50);
            StartCoroutine(AttackDelay());
        }
    }
    private IEnumerator AttackDelay()
    {
        m_bCanAttack = false;
        yield return new WaitForSeconds(3.0f);
        m_bCanAttack = true;
    }
    public void SetDieState()
    {
        isDie = true;
        animator.SetBool("IsDie",true);
        QuestManager.Instance.NotifyQuestUpdate(Quest_Type.Type_Kill, (int)m_cEnemyData._type, 1);
        if(DungeonManager.Instance)
             DungeonManager.Instance.DieMonster();
        DropItem();
    }
    public void Die()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f &&animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            fsm.ChangeState(stateData.IdleState);
            enemySpawner.DieEnemy(this);
        }
    }
    IEnumerator WaitForNextAttacked()
    {
        m_bCanAttack = true;
        yield return new WaitForSeconds(0.3f);
        m_bCanAttack = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AttackCollider")&&!m_bIsDamaged)
        {
           curHealth -= other.GetComponent<AttackCollider>().CalculateDamage();

            enemyHealthBar.value = curHealth / m_cEnemyData._maxHealth;
            StartCoroutine(WaitForNextAttacked());
        }

        if(curHealth <=0 && isDie == false)
        {
            fsm.ChangeState(stateData.DieState);
        }
    }
    public void DropItem()
    {
        for(int i =0;i< m_eEnemyDropItems.dropItems.Count;i++)
        {
            int rand = Random.RandomRange(0, 100);
            if(rand  < m_eEnemyDropItems.dropItems[i].dropPercent)
            {
                Inventory.Instance.GetItem(m_eEnemyDropItems.dropItems[i].itemID);
                //Inventory.Instance.GetItem(0);

            }
        }
        Inventory.Instance.AddGold(m_eEnemyDropItems.gold);
        ThirdPersonMovement.Instance.AddExp(m_eEnemyDropItems.exp);

    }
}
