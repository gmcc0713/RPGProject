using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public static ThirdPersonMovement Instance { get; private set; }
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    private Inventory m_inventory;


    public Inventory _Inventory => m_inventory;

    public CharacterController controller;
    [SerializeField] PlayerDataUI m_playerDataUI;
    public Transform cam;
    private Vector2 input;
    [SerializeField] float speed = 0.8f;
    [SerializeField] float walkSpeed = 2f, runSpeed = 4f;
    private float gravity = -9.81f;
    private Vector3 moveDir;


    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private bool isGround;

    [SerializeField] private Animator animator;
    public Animator _playerAnim => animator;
    [SerializeField] private bool canAct;
    private bool isMove = false;
    private bool isAttack;
    public bool _isAttack { get; set; }
    public bool _isMove { get; set; }
    private int attackCombo = 0;
    [SerializeField] private AttackCollider m_attackCollider;
    public bool _canAct { get; set; }
    

    private PlayerData m_PlayerData;
    [SerializeField] private PlayerStats m_statsData;
    [SerializeField]  private float m_iCurPlayerHealth;
    [SerializeField]  private float m_iCurPlayerMP;
    [SerializeField] private Equipment m_Equipment;

    public void Start()
    {
        Debug.Log("Initialize");
        Initialize();
    }
    void Update()
    {
        if(canAct)
            Move();
        InputKey();

    }
   public void Initialize()
    {
        m_PlayerData = SaveLoadManager.Instance.LoadPlayerData();
        if(m_PlayerData == null)
        {
             m_PlayerData = new PlayerData();
            m_PlayerData.SetPlayerData("플레이어1", "무직", "초보자", 1, 0);
        }
        m_inventory = new Inventory();
        m_inventory.initialize();

        m_statsData.Initialize();
        m_statsData.UpdatePlayerData(m_PlayerData);
        m_iCurPlayerHealth = m_statsData._StatsData.HP;
        m_playerDataUI.UpdateUI(m_iCurPlayerHealth/m_statsData._StatsData.HP,m_iCurPlayerMP/ m_statsData._StatsData.MP, m_PlayerData._curEXP / m_PlayerData._EXP,m_PlayerData._sUserName);
        SetDamage();
        animator = GetComponent<Animator>();
    }
    
    private void Move()
    {
        if (isAttack)
        {
            return;
        }
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        isMove = direction.magnitude >= 0.1f;


        if (isMove)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        }
        if (!isGround)
        {
            moveDir.y += gravity * Time.deltaTime;

        }
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
        animator.SetBool("IsMove", isMove);
        animator.SetFloat("Speed", speed);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    public void RunModeOn(InputAction.CallbackContext context)
    {
        speed = runSpeed;

    }
    public void RunModeOff(InputAction.CallbackContext context)
    {
        speed = walkSpeed;
    }

    public void OnMouseButtonDown(InputAction.CallbackContext context)
    {
        if (canAct)
        {
            attackCombo++;
            isAttack = false;
            if (attackCombo == 1)
            {
                animator.SetBool("AttackCombo_1", true);
                isAttack = true;
            }
            else if (attackCombo >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo1"))
            {
                animator.SetBool("AttackCombo_1", false);
                animator.SetBool("AttackCombo_2", true);
                isAttack = true;
            }
            else if (attackCombo >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo2"))
            {
                animator.SetBool("AttackCombo_2", false);
                animator.SetBool("AttackCombo_3", true);
                isAttack = true;
            }
        }
    }
    public void SetDamage()
    {
        
        m_attackCollider.SetPlayerDamage(m_statsData._StatsData.AttackDamage);
        m_attackCollider.SetCritical(m_statsData._StatsData.Critical);
        m_attackCollider.SetWeaponDamage(m_statsData._StatsData.Critical);
    }
    public void GetDamaged(float damage)
    {
        float damageOffset = 0;
        if(m_statsData._StatsData.Defence > damage)
        {
            damageOffset = 0.5f;
        }
        else
        {
            damageOffset = 1.0f;
        }

        m_iCurPlayerHealth -= damage * damageOffset;
        Mathf.Clamp(m_iCurPlayerHealth, 0, m_statsData._StatsData.HP);
        m_playerDataUI.SetPlayerHPUI(m_iCurPlayerHealth / m_statsData._StatsData.HP);
    }
    public void Attack()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetBool("AttackCombo_1"))
        {
            animator.SetBool("AttackCombo_1", false); 
            attackCombo = 0;
            isAttack = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetBool("AttackCombo_2"))
        {
            animator.SetBool("AttackCombo_2", false); 
            attackCombo = 0;
            isAttack = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("AttackCombo_3"))
        {
            animator.SetBool("AttackCombo_3", false);
            attackCombo = 0;
            isAttack = false;
        }


    }

    void FixedUpdate()
    {
        Attack();
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
        if (other.CompareTag("InteractionObject"))
        {

            other.GetComponent<InteractionObject>().ShowOrHideInteractionObject(true);
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractionObject") && Input.GetKeyUp(KeyCode.Space)) 
        {
            other.GetComponent<InteractionObject>().PressInteractionKey();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
        if (other.CompareTag("InteractionObject"))
        {
            other.GetComponent<InteractionObject>().ShowOrHideInteractionObject(false);
        }
    }
   public void SetCanAct(bool canActing)
    {
        canAct = canActing;
    }
    public void AddExp(float exp)
    {
        m_PlayerData.AddExp(exp);

        m_playerDataUI.SetPlayerEXPUI(m_PlayerData._curEXP/m_PlayerData._EXP);
    }
    public void UsePortionItem(PortionType type,int value)
    {
        switch (type)
        {
            case PortionType.HP:
                m_iCurPlayerHealth += value;
                m_iCurPlayerHealth = Mathf.Clamp(m_iCurPlayerHealth, 0, m_statsData._StatsData.HP);
                break;
            case PortionType.MP:
                m_iCurPlayerMP += value;
                m_iCurPlayerHealth = Mathf.Clamp(m_iCurPlayerMP, 0, m_statsData._StatsData.MP);
                break;
        }
        //m_playerDataUI.UpdateUI(m_iCurPlayerHealth, m_iCurPlayerHealth)
    }
    public void LVUP()
    {
        m_statsData.StatsPointUp();
        m_iCurPlayerHealth = m_statsData._StatsData.HP;
        m_playerDataUI.SetPlayerHPUI(m_iCurPlayerHealth/ m_statsData._StatsData.HP);

        m_PlayerData.LVUP();
    }
    public void SavePlayerData()
    {
        SaveLoadManager.Instance.SavePlayerData(m_PlayerData);
    }
    public void SetPlayerDataByCustomizing(string playerName)
    {
        m_PlayerData.SetPlayerData(playerName, "무직", "초보자", 1, 0);
        m_statsData.UpdatePlayerData(m_PlayerData);
        m_playerDataUI.UpdateUI(m_iCurPlayerHealth/m_statsData._StatsData.HP,m_iCurPlayerMP/ m_statsData._StatsData.MP, m_PlayerData._curEXP / m_PlayerData._EXP,m_PlayerData._sUserName);
    }
    public void InputKey()
    {
        
    }
}
