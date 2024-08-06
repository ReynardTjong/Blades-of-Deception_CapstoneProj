using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "PlayerStates/StandingState")]
    public class StandingState : PlayerState
    {
        private Vector3 gravityVelocity;
        private Vector3 currentVelocity;
        private Vector3 cVelocity;
        private Vector3 velocity;
        private Vector2 input;
        private float playerSpeed;
        private float gravityValue;
        private bool grounded;

        public override void EnterState(Character character)
        {
            Debug.Log("Entered Standing State");

            // Initialize variables
            input = Vector2.zero;
            currentVelocity = Vector3.zero;
            gravityVelocity.y = 0;
            playerSpeed = character.playerSpeed;
            grounded = character.controller.isGrounded;
            gravityValue = character.gravityValue;

            // Initialize input actions
            InitializeInputActions(character.playerInput);
        }

        public override void UpdateState(Character character)
        {
            HandleInput(character);
            HandleMovement(character);
        }

        private void HandleInput(Character character)
        {
            // Read movement input
            input = moveAction.ReadValue<Vector2>();
            velocity = new Vector3(input.x, 0, input.y);

            // Convert input direction to world direction
            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;
        }

        private void HandleMovement(Character character)
        {
            // Apply gravity
            gravityVelocity.y += gravityValue * Time.deltaTime;
            grounded = character.controller.isGrounded;

            if (grounded && gravityVelocity.y < 0)
            {
                gravityVelocity.y = 0f;
            }

            // Smooth movement
            currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
            character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

            // Rotate character towards movement direction
            if (velocity.sqrMagnitude > 0)
            {
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
            }
        }

        public override void ExitState(Character character)
        {
            Debug.Log("Exited Standing State");

            // Reset gravity velocity
            gravityVelocity.y = 0f;
        }
    }
}
