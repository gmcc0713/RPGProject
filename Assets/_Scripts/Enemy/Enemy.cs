using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum RotateType { RotToTarget,RotToSpawnPos}
public class Enemy : MonoBehaviour,IPoolingObject
{
    private EnemyType type;
    public EnemyType _type => type;
    private float maxHealth = 100f;
    private float curHealth = 100f;
    private float searchRange;
    private float attackRange;
    [SerializeField] private Slider enemyHealthBar;

    public bool isMoving;

    public Transform target;
    public Vector3 defaultPos;

    Rigidbody rigid;

    
    public NavMeshAgent agent;
    EnemyFSM fsm;
    StateData stateData = null;
    EnemyData data;
    EnemySpawner enemySpawner;
    Animator animator;
    public int getDamagedCombo;
    public Animator _animator => animator;
    bool isDie = false;
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    public void ResetData()
    {
        if (fsm != null)
        {
            fsm.ChangeState(stateData.IdleState);
        }
        curHealth = maxHealth;
        enemyHealthBar.value = 1;
        getDamagedCombo = 0;


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
            Debug.Log("enemyData �� null");
            return false;
        }
        if (null == fsm) fsm = new EnemyFSM(this);
        if (!fsm.SetCurrState(stateData.IdleState))
        {
            Debug.Log("stateData �� null");
            return false;
        }
        
        maxHealth = enemyData._maxHealth;
        searchRange = enemyData._searchRange;
        attackRange = enemyData._attackRange;


        gameObject.SetActive(true);

        defaultPos = transform.position;
        ResetData();

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
        if (DistanceToTarget() > 0 && DistanceToTarget() <= searchRange)
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
        if (DistanceToTarget() < attackRange)       //���ݻ�Ÿ� �ȿ� Ÿ���� ������
        {
            fsm.ChangeState(stateData.AttackState);
            return;
        }
        else if (DistanceToTarget() < searchRange)  //Ž����Ÿ� �ȿ� Ÿ���� ������
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
        if (DistanceToTarget() > 0 && DistanceToTarget() <= searchRange)    //Ÿ���� �����ȿ� ��������
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

        animator.SetBool("IsAttack",true);
        if (DistanceToTarget() > attackRange)
        {
            animator.SetBool("IsAttack", false);
            fsm.ChangeState(stateData.MoveState);
        }
    }
    public void SetDieState()
    {
        isDie = true;
        animator.SetBool("IsDie",true);
    }
    public void Die()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f &&animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            fsm.ChangeState(stateData.IdleState);
            enemySpawner.DieEnemy(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            AttackCollider weapon = other.GetComponent<AttackCollider>();
            if (weapon.AttackComboCheck(getDamagedCombo))
            {
                Debug.Log("AttackSuccess");
                getDamagedCombo = weapon._weaponCombo;
                curHealth -= weapon._weaponDamage;
                enemyHealthBar.value = curHealth / maxHealth;
            }
            if(!weapon.isAttack)
            {
                getDamagedCombo = 0;
            }
        }

        if(curHealth <=0)
        {
            fsm.ChangeState(stateData.DieState);

        }
    }
    
    
}
