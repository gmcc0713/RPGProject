using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
     private float m_fMoveSpeed;
    [SerializeField] private float m_fWalkSpeed = 3;
    [SerializeField] private float m_fRunSpeed = 10;
    private Vector3 m_MoveDir;
    private Animator m_animatior;
    private float m_fXInput, m_fYInput;
    private CharacterController m_CharacterController;
    private Transform m_CameraObject;
    private bool m_bIsMove;
    [SerializeField] float m_fGravityOffset;
    [SerializeField] LayerMask m_groundMask;

    private Vector3 m_spherePos;
    [SerializeField] float m_fGravity = -9.81f;
    private Vector3 m_Velocity;
    private bool m_bRunMode = false;
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_animatior = GetComponent<Animator>();
    }

    void Update()
    {
        GetDirectionAndMove();
        RunCheck();
        Gravity();
    }
    void GetDirectionAndMove()
    {
        m_fMoveSpeed = m_fWalkSpeed;
        if (m_bRunMode)
            m_fMoveSpeed = m_fRunSpeed;

        m_fXInput = Input.GetAxis("Horizontal");
        m_fYInput = Input.GetAxis("Vertical");

        m_MoveDir = transform.forward * m_fYInput + transform.right * m_fXInput;

        m_CharacterController.Move(m_MoveDir * m_fMoveSpeed * Time.deltaTime);
        m_bIsMove = m_MoveDir.sqrMagnitude >= 0.1f;
    }
    private void RunCheck()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_fMoveSpeed = m_fRunSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_fMoveSpeed = m_fWalkSpeed;
        }
    }
    bool IsGounded()
    {
        m_spherePos = new Vector3(transform.position.x, transform.position.y - m_fGravityOffset, transform.position.z);
        if (Physics.CheckSphere(m_spherePos, m_CharacterController.radius - 0.05f, m_groundMask))
            return true;
        return false;
    }
    void Gravity()
    {
        Debug.Log(IsGounded());
        if(!IsGounded())
        {
           m_Velocity.y += m_fGravity * Time.deltaTime;
        }
        else if(m_Velocity.y < 0)
        {
            m_Velocity.y = -2f;
        }

        m_CharacterController.Move(m_Velocity * Time.deltaTime);

        m_animatior.SetBool("IsMove", m_bIsMove);
        m_animatior.SetFloat("Speed", m_fMoveSpeed);
    }
    void Attack()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
        
        }
    }
}
