using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/ChaseState")]
    public class ChaseState : AIState
    {
        public override void EnterState(AIController aiController)
        {
            // Setup code for entering Chase state
        }

        public override void UpdateState(AIController aiController)
        {
            if (aiController.playerTransform != null)
            {
                aiController.agent.SetDestination(aiController.playerTransform.position);
            }
            // Transition to other states if conditions are met
        }

        public override void ExitState(AIController aiController)
        {
            // Cleanup code for exiting Chase state
        }
    }
}
