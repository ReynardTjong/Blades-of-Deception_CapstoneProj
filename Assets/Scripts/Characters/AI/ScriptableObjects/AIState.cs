using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public abstract class AIState : ScriptableObject
    {
        public abstract void Enter(AIController ai);
        public abstract void Exit(AIController ai);
        public abstract void UpdateState(AIController ai);
    }
}
