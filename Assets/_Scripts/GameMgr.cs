using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }
    [SerializeField] PlayerInput playerInput;
    private ThirdPersonMovement player;
    public ThirdPersonMovement _player => player;
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
   
    void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        playerInput.actions["RunMode"].performed += ThirdPersonMovement.Instance.RunModeOn;                         //�޸��� ���(shift)
        playerInput.actions["RunMode"].canceled += ThirdPersonMovement.Instance.RunModeOff;
        playerInput.actions["Move"].performed += ThirdPersonMovement.Instance.OnMove;                               //wasd �����̱�
        playerInput.actions["Move"].canceled += ThirdPersonMovement.Instance.OnMove;
        playerInput.actions["MouseLeftClick"].performed += ThirdPersonMovement.Instance.OnMouseButtonDown;          //����(���콺����)

        //playerInput.actions["InteractionButtonClick"].performed += ThirdPersonMovement.Instance.OnMouseButtonDown;          //��ȣ�ۿ�(space)
        player = GameObject.Find("Player").GetComponent<ThirdPersonMovement>();

        PlayerDataManager.Instance.Initialize();
        ThirdPersonMovement.Instance.Initialize();

    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        playerInput.actions["RunMode"].performed -= ThirdPersonMovement.Instance.RunModeOn;
        playerInput.actions["RunMode"].canceled -= ThirdPersonMovement.Instance.RunModeOff;
        playerInput.actions["Move"].performed -= ThirdPersonMovement.Instance.OnMove;
        playerInput.actions["Move"].canceled -= ThirdPersonMovement.Instance.OnMove;
        playerInput.actions["MouseLeftClick"].performed -= ThirdPersonMovement.Instance.OnMouseButtonDown;


    }

}
