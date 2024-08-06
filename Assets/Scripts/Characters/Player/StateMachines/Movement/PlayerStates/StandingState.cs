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

        public override void EnterState(Character character)
        {
            Debug.Log("Entered Standing State");

            // Initialize variables
            input = Vector2.zero;
            currentVelocity = Vector3.zero;
            gravityVelocity.y = 0;
            playerSpeed = character.playerSpeed;
            gravityValue = character.gravityValue;

            // Reset animation parameters for standing
            character.animator.SetFloat("speed", 0f);

            // Initialize input actions only once here
            InitializeInputActions(character.playerInput);
        }

        public override void UpdateState(Character character)
        {
            HandleInput(character);
            HandleMovement(character);
            UpdateAnimation(character);
        }

        private void HandleInput(Character character)
        {
            input = moveAction.ReadValue<Vector2>();
            velocity = new Vector3(input.x, 0, input.y);
            velocity = Vector3.ProjectOnPlane(character.cameraTransform.right * velocity.x + character.cameraTransform.forward * velocity.z, Vector3.up);

            if (sprintAction.triggered)
            {
                character.ChangeState(character.sprintState);
            }
        }

        private void HandleMovement(Character character)
        {
            gravityVelocity.y += character.gravityValue * Time.deltaTime;
            if (character.controller.isGrounded && gravityVelocity.y < 0)
            {
                gravityVelocity.y = 0f;
            }

            currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
            character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

            if (velocity.sqrMagnitude > 0)
            {
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
            }
        }

        private void UpdateAnimation(Character character)
        {
            float speed = input.magnitude;
            character.animator.SetFloat("speed", speed);
        }

        public override void ExitState(Character character)
        {
            Debug.Log("Exited Standing State");
            gravityVelocity.y = 0f;
        }
    }
}
