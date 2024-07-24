using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{

    [CreateAssetMenu(menuName = "AI/States/AttackState")]
    public class AttackState : AIState
    {
        public override void EnterState(AIController aiController)
        {
            // Setup code for entering Attack state
        }

        public override void UpdateState(AIController aiController)
        {
            // Implement attack logic here
            // Transition to other states if conditions are met
        }

        public override void ExitState(AIController aiController)
        {
            // Cleanup code for exiting Attack state
        }
    }
}
