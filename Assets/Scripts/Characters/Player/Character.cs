using BladesOfDeceptionCapstoneProject;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Character : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;

    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;

    public PlayerState standingState;
    public PlayerState sprintState;

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

    public StateMachine stateMachine;

    private void Awake()
    {
        // Initialize components here to avoid redundant calls in Start
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        stateMachine = new StateMachine();

        // Ensure these states are assigned in the Inspector or initialized properly
        stateMachine.Initialize(standingState, this);
    }

    private void Update()
    {
        stateMachine.currentState.UpdateState(this);
    }

    public void ChangeState(PlayerState newState)
    {
        stateMachine.ChangeState(newState, this);
    }
}
