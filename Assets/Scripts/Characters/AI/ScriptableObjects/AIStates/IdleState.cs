using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/IdleState")]
    public class IdleState : AIState
    {
        [SerializeField] private float idleDuration = 3.0f; // Time to stay idle before checking for transition
        private float idleTimer;

        public override void EnterState(AIController aiController)
        {
            idleTimer = idleDuration;
            aiController.agent.isStopped = true; // Stop the AI movement
            Debug.Log("IdleState: Entered, starting timer with duration: " + idleDuration);
            // Optionally play idle animation
            // aiController.animator.Play("Idle");
        }

        public override void UpdateState(AIController aiController)
        {
            idleTimer -= Time.deltaTime;
            Debug.Log("IdleState: Idle timer: " + idleTimer);

            // Check for transitions (e.g., player in detection range and FOV)
            if (aiController.IsPlayerInFOV())
            {
                Debug.Log("IdleState: Player detected, transitioning to ChaseState");
                aiController.TransitionToState(aiController.chaseState);
                return; // Exit to avoid further processing
            }

            if (idleTimer <= 0)
            {
                // Transition back to PatrolState
                Debug.Log("IdleState: Idle duration elapsed, transitioning to PatrolState");
                aiController.TransitionToState(aiController.patrolState);
                return; // Exit to avoid further processing
            }
        }

        public override void ExitState(AIController aiController)
        {
            // Optionally stop idle animation
            // aiController.animator.Stop("Idle");
            aiController.agent.isStopped = false; // Resume movement
            Debug.Log("IdleState: Exited");
        }
    }
}
