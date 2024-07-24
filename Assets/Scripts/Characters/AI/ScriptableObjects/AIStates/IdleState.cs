using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/IdleState")]
    public class IdleState : AIState
    {
        public float idleDuration = 3.0f; // Time to stay idle before checking for transition
        private float idleTimer;

        public override void EnterState(AIController aiController)
        {
            idleTimer = idleDuration;
            aiController.agent.isStopped = true; // Stop the AI movement
                                                 // Optionally play idle animation
                                                 // aiController.animator.Play("Idle");
        }

        public override void UpdateState(AIController aiController)
        {
            idleTimer -= Time.deltaTime;

            // Check for transitions (e.g., player in detection range and FOV)
            if (aiController.IsPlayerInFOV())
            {
                aiController.TransitionToState(aiController.chaseState);
            }

            if (idleTimer <= 0)
            {
                // Optionally transition to a patrol state or back to Idle
                idleTimer = idleDuration; // Reset timer if needed
            }
        }

        public override void ExitState(AIController aiController)
        {
            // Optionally stop idle animation
            // aiController.animator.Stop("Idle");
            aiController.agent.isStopped = false; // Resume movement
        }
    }
}
