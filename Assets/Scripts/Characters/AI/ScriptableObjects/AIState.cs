using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace BladesOfDeceptionCapstoneProject
{
    public abstract class AIState : ScriptableObject
    {
        public abstract void Enter(AIController ai);
        public abstract void Exit(AIController ai);
        public abstract void Update(AIController ai);
    }
}
