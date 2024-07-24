using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public abstract class AIState : ScriptableObject
    {
        public abstract void EnterState(AIController aiController);
        public abstract void UpdateState(AIController aiController);
        public abstract void ExitState(AIController aiController);
    }
}
