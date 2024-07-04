using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/IdleState")]
    public class IdleState : AIState
    {
        public override void Enter(AIController ai)
        {
            // Enter idle logic
        }

        public override void Exit(AIController ai)
        {
            // Exit idle logic
        }

        public override void Update(AIController ai)
        {
            // Idle update logic
            if (ai.CanSeePlayer())
            {
                ai.ChangeState(ai.chaseState);
            }
        }
    }
}
