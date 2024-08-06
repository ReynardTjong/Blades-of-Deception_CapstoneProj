using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "PlayerStates/SprintState")]
    public class SprintState : PlayerState
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
            Debug.Log("Entered Sprint State");

            input = Vector2.zero;
            currentVelocity = Vector3.zero;
            gravityVelocity.y = 0;
            playerSpeed = character.sprintSpeed;
            gravityValue = character.gravityValue;

            character.animator.SetFloat("speed", 1.2f);
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

            if (!sprintAction.ReadValue<float>().Equals(1f) || input.sqrMagnitude == 0f)
            {
                character.ChangeState(character.standingState);
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
            character.animator.SetFloat("speed", Mathf.Max(speed, 1.2f));
        }

        public override void ExitState(Character character)
        {
            Debug.Log("Exited Sprint State");
            gravityVelocity.y = 0f;
            character.animator.SetFloat("speed", 0f);
        }
    }
}
