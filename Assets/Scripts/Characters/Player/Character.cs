using BladesOfDeceptionCapstoneProject;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;

    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;

    public PlayerState standingState;

    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public float gravityValue = -9.81f;

    private StateMachine stateMachine;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        stateMachine = new StateMachine();

        // Initialize and set the starting state to StandingState
        standingState = ScriptableObject.CreateInstance<StandingState>();
        stateMachine.Initialize(standingState, this);
    }

    private void Update()
    {
        stateMachine.currentState.UpdateState(this);
    }

    // Additional states can be initialized and used as needed
    // public PlayerState jumpingState;
    // public PlayerState crouchingState;
    // public PlayerState sprintingState;
    // public PlayerState combattingState;

    public void ChangeState(PlayerState newState)
    {
        // Change to a new state
        stateMachine.ChangeState(newState, this);
    }
}
