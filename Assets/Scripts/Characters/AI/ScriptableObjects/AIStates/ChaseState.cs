using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/ChaseState")]
    public class ChaseState : AIState
    {
        [SerializeField] private float chaseRange = 10.0f; // Maximum range for chasing

        public override void EnterState(AIController aiController)
        {
            // Setup code for entering Chase state
            aiController.agent.isStopped = false;
        }

        public override void UpdateState(AIController aiController)
        {
            if (aiController.playerTransform != null)
            {
                float distanceToPlayer = Vector3.Distance(aiController.transform.position, aiController.playerTransform.position);

                if (distanceToPlayer <= chaseRange)
                {
                    aiController.agent.SetDestination(aiController.playerTransform.position);
                }
                else
                {
                    aiController.TransitionToState(aiController.idleState);
                }
            }
        }

        public override void ExitState(AIController aiController)
        {
            // Cleanup code for exiting Chase state
            aiController.agent.isStopped = true;
        }
    }
}
