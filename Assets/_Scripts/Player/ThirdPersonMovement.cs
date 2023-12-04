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
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    public CharacterController controller;
    public Transform cam;
    private Vector2 input;
    [SerializeField] float speed = 0.8f;
    [SerializeField] float walkSpeed = 2f, runSpeed = 4f;
    private float gravity = -9.81f;
    private Vector3 moveDir;

     public float turnSmoothTime = 0.1f;
     float turnSmoothVelocity;
    public bool isGround;

    [SerializeField] private Animator animator;
    public Animator _playerAnim => animator;
    private bool isMove = false;
    private bool isAttack;
    public bool _isAttack { get; set; }
    public AttackCollider weapon;
    private int attackCombo = 0;

    void Update()
    {
        Move();
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
        Debug.Log("Attack");
        attackCombo++;
        if (attackCombo == 1)
        {
            animator.SetBool("AttackCombo_1", true);
            isAttack = true;
            weapon.AttackComboSet(1);
        }
        else if (attackCombo >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo1"))
        {
            animator.SetBool("AttackCombo_1", false);
            animator.SetBool("AttackCombo_2", true);
            isAttack = true;
            weapon.AttackComboSet(2);
        }
        else if (attackCombo >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo2"))
        {
            animator.SetBool("AttackCombo_2", false);
            animator.SetBool("AttackCombo_3", true);
            isAttack = true;
            weapon.AttackComboSet(3);
        }
        weapon.isAttack = isAttack;

    }


    public void Attack()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetBool("AttackCombo_1"))
        {
            animator.SetBool("AttackCombo_1", false); 
            attackCombo = 0;
            isAttack = false;
            weapon.isAttack = isAttack;

            weapon.AttackComboSet(0);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetBool("AttackCombo_2"))
        {
            animator.SetBool("AttackCombo_2", false); 
            attackCombo = 0;
            isAttack = false;
            weapon.isAttack = isAttack;
            weapon.AttackComboSet(0);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("AttackCombo_3"))
        {
            animator.SetBool("AttackCombo_3", false);
            attackCombo = 0;
            isAttack = false;
            weapon.isAttack = isAttack;
            weapon.AttackComboSet(0);
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
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
