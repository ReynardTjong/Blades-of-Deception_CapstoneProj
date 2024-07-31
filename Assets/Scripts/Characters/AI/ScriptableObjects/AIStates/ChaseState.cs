using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/ChaseState")]
    public class ChaseState : AIState
    {
        [SerializeField] private float chaseSpeed = 4.0f; // Speed for chasing
        [SerializeField] private float chaseRange = 10.0f; // Maximum range for chasing

        public override void EnterState(AIController aiController)
        {
            // Setup code for entering Chase state
            aiController.agent.isStopped = false;
            aiController.agent.speed = chaseSpeed;
            Debug.Log("ChaseState: Entered with speed: " + chaseSpeed);
        }

        public override void UpdateState(AIController aiController)
        {
            if (aiController.playerTransform != null)
            {
                float distanceToPlayer = Vector3.Distance(aiController.transform.position, aiController.playerTransform.position);
                Debug.Log("ChaseState: Distance to player: " + distanceToPlayer);

                if (distanceToPlayer <= chaseRange)
                {
                    aiController.agent.SetDestination(aiController.playerTransform.position);
                    Debug.Log("ChaseState: Chasing player to " + aiController.playerTransform.position);
                }
                else
                {
                    Debug.Log("ChaseState: Player out of range, transitioning to PatrolState");
                    aiController.TransitionToState(aiController.patrolState);
                }
            }
        }

        public override void ExitState(AIController aiController)
        {
            // Cleanup code for exiting Chase state
            aiController.agent.isStopped = true;
            Debug.Log("ChaseState: Exited");
        }
    }
}
