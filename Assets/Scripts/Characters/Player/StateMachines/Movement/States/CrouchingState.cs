using UnityEngine;

public class CrouchingState : State
{
    private float playerSpeed;
    private bool belowCeiling;
    private bool crouchToggled;
    private bool grounded;
    private float gravityValue;
    private Vector3 currentVelocity;

    public CrouchingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Crouching State");

        character.animator.SetTrigger("crouch");
        belowCeiling = false;
        crouchToggled = true;
        gravityVelocity.y = 0;

        playerSpeed = character.crouchSpeed;
        character.controller.height = character.crouchColliderHeight;
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f);
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Crouching State");

        // Reset collider and animation
        character.controller.height = character.normalColliderHeight;
        character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);
        character.animator.SetTrigger("move");
    }

    public override void HandleInput()
    {
        base.HandleInput();

        // Toggle crouch state on crouch action
        if (crouchAction.triggered)
        {
            crouchToggled = !crouchToggled;
            Debug.Log("Crouch Toggled: " + crouchToggled);
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Update animator speed
        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        // Change to standing state if crouch is toggled off and not below ceiling
        if (!crouchToggled && !belowCeiling)
        {
            Debug.Log("Changing to Standing State");
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Check if there's a ceiling above
        belowCeiling = CheckCollisionOverlap(character.transform.position + Vector3.up * character.normalColliderHeight);

        // Handle gravity
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        // Smooth velocity and move character
        currentVelocity = Vector3.Lerp(currentVelocity, velocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        // Rotate character towards movement direction
        if (velocity.magnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

    public bool CheckCollisionOverlap(Vector3 targetPosition)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;

        Vector3 direction = targetPosition - character.transform.position;
        if (Physics.Raycast(character.transform.position, direction, out hit, character.normalColliderHeight, layerMask))
        {
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * character.normalColliderHeight, Color.white);
            return false;
        }
    }
}