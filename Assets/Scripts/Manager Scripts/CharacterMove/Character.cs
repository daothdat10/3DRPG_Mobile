using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    [Header("Control")]
    public float playerSpeed = 5;
    public float rotationSpeed = 5f;
    public float attack = 0.8f;

    public float gravityMultiplier = 5.0f;

    [Header("Animation Smoothing")]

    [Range(0f, 1f)]
    public float speedDampTime = 0.1f;

    [Range(0f, 1f)]
    public float velocityDampTime = 0.9f;

    [Range(0f, 1f)]
    public float rotationDampTime = 0.2f;


    public StateMachine movementSM;
    public StandingState standing;
    public attack combatAttack;

    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller ;
    [HideInInspector]
    public PlayerInput playerInput ;
    [HideInInspector]
    public Transform cameraTransform ;
    [HideInInspector]
    public Animator aniamtor;
    [HideInInspector]
    public Vector3 playerVelocity;

    private void Start()
    {
        controller =  GetComponent<CharacterController>();
        aniamtor = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        combatAttack = new attack(this, movementSM);

        movementSM.Intialize(standing); 

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }
    private void Update()
    {
        movementSM.currentState.HandleInput();
        movementSM.currentState.LogicUpdate();

    }
    private  void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }
    

}
