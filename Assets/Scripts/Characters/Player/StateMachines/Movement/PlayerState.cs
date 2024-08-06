using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BladesOfDeceptionCapstoneProject
{
    public abstract class PlayerState : ScriptableObject
    {
        protected InputAction moveAction;
        protected InputAction lookAction;
        protected InputAction crouchAction;
        protected InputAction sprintAction;
        protected InputAction drawWeaponAction;
        protected InputAction attackAction;

        // Initialize input actions for the state
        public virtual void InitializeInputActions(PlayerInput playerInput)
        {
            moveAction = playerInput.actions["Move"];
            lookAction = playerInput.actions["Look"];
            crouchAction = playerInput.actions["Crouch"];
            sprintAction = playerInput.actions["Sprint"];
            drawWeaponAction = playerInput.actions["DrawWeapon"];
            attackAction = playerInput.actions["Attack"];
        }

        // Methods to be implemented by specific states
        public abstract void EnterState(Character character);
        public abstract void UpdateState(Character character);
        public abstract void ExitState(Character character);
    }
}
