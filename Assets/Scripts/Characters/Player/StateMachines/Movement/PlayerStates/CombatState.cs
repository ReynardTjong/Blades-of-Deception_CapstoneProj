using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "PlayerStates/CombatState")]
    public class CombatState : PlayerState
    {
        private Vector3 gravityVelocity;
        private Vector3 currentVelocity;
        private Vector3 cVelocity;
        private Vector3 velocity;
        private Vector2 input;
        private bool grounded;
        private bool isWeaponDrawn;
        private bool attackRequested;

        public override void EnterState(Character character)
        {
            Debug.Log("Entered Combat State");

            // Initialize variables
            input = Vector2.zero;
            currentVelocity = Vector3.zero;
            gravityVelocity.y = 0;
            grounded = character.controller.isGrounded;
            isWeaponDrawn = false;
            attackRequested = false;

            // Ensure weapon is drawn if not already
            if (!isWeaponDrawn)
            {
                character.GetComponent<EquipmentSystem>().DrawWeapon();
                isWeaponDrawn = true;
            }

            // Set the animator to combat idle animation
            character.animator.SetFloat("speed", 0f); // Assuming 0 is the combat idle speed
        }

        public override void UpdateState(Character character)
        {
            HandleInput(character);
            HandleMovement(character);
            UpdateAnimation(character);
        }

        private void HandleInput(Character character)
        {
            // Read movement input
            input = moveAction.ReadValue<Vector2>();
            velocity = new Vector3(input.x, 0, input.y);

            // Convert input direction to world direction
            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;

            // Check if the draw/sheath weapon key is pressed
            if (drawWeaponAction.triggered)
            {
                if (isWeaponDrawn)
                {
                    character.animator.SetTrigger("sheathWeapon");
                    isWeaponDrawn = false;
                }
                else
                {
                    character.animator.SetTrigger("drawWeapon");
                    isWeaponDrawn = true;
                }
            }

            // Check if the player wants to attack
            if (attackAction.triggered)
            {
                attackRequested = true;
            }

            // Check if the player wants to exit combat state
            if (input.sqrMagnitude == 0f && !attackRequested) // Example condition to exit combat state
            {
                character.ChangeState(character.standingState);
            }
        }

        private void HandleMovement(Character character)
        {
            // Apply gravity
            gravityVelocity.y += character.gravityValue * Time.deltaTime;
            grounded = character.controller.isGrounded;

            if (grounded && gravityVelocity.y < 0)
            {
                gravityVelocity.y = 0f;
            }

            // Smooth movement
            currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
            character.controller.Move(currentVelocity * Time.deltaTime * character.playerSpeed + gravityVelocity * Time.deltaTime);

            // Rotate character towards movement direction
            if (velocity.sqrMagnitude > 0)
            {
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
            }
        }

        private void UpdateAnimation(Character character)
        {
            // Update animation for combat state if attack is requested
            if (attackRequested)
            {
                character.animator.SetTrigger("attack");
                attackRequested = false; // Reset attack request after triggering
                character.ChangeState(character.attackingState); // Transition to attacking state
            }
        }

        public override void ExitState(Character character)
        {
            Debug.Log("Exited Combat State");

            // Reset gravity velocity
            gravityVelocity.y = 0f;

            // Sheath weapon if exiting combat state
            if (isWeaponDrawn)
            {
                character.GetComponent<EquipmentSystem>().SheathWeapon();
                isWeaponDrawn = false;
            }

            // Set animator back to standing idle
            character.animator.SetFloat("speed", 0f);
        }
    }
}
