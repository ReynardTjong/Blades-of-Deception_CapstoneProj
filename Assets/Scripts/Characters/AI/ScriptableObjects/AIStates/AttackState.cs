using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{

    [CreateAssetMenu(menuName = "AI/States/AttackState")]
    public class AttackState : AIState
    {
        private float attackCooldownTimer;

        public override void EnterState(AIController aiController)
        {
            // Setup code for entering Attack state
            aiController.agent.isStopped = true; // Stop movement to attack
            attackCooldownTimer = 0f; // Reset attack cooldown timer
            Debug.Log("AttackState: Entered");
        }

        public override void UpdateState(AIController aiController)
        {
            if (aiController.playerTransform == null)
            {
                Debug.LogError("AttackState: PlayerTransform is null");
                return;
            }

            // Calculate distance to the player
            float distanceToPlayer = Vector3.Distance(aiController.transform.position, aiController.playerTransform.position);

            // Check if the player is within attack range
            if (distanceToPlayer <= aiController.enemyStats.attackRange)
            {
                // Check if the attack is on cooldown
                if (attackCooldownTimer <= 0f)
                {
                    // Attack the player
                    PerformAttack(aiController);
                    // Reset the cooldown timer
                    attackCooldownTimer = aiController.enemyStats.attackCooldown;
                }
                else
                {
                    // Decrease the cooldown timer
                    attackCooldownTimer -= Time.deltaTime;
                }
            }
            else
            {
                // If the player is out of attack range, transition back to ChaseState
                Debug.Log("AttackState: Player out of range, transitioning to ChaseState");
                aiController.TransitionToState(aiController.chaseState);
            }

            // Optionally, you can add logic to handle transitions to other states if conditions are met
            if (aiController.enemyStats.health <= 0)
            {
                Debug.Log("AttackState: Health is 0 or less, transitioning to DeadState (if implemented)");
                // aiController.TransitionToState(aiController.deadState);
            }
        }

        private void PerformAttack(AIController aiController)
        {
            // Implement the logic to attack the player
            Debug.Log("AttackState: Attacking player, dealing " + aiController.enemyStats.damage + " damage");

            // You can add code here to trigger attack animations, sound effects, etc.
            // For example:
            // aiController.animator.SetTrigger("Attack");

            // Deal damage to the player
            // Assuming you have a PlayerHealth script to manage player's health
            PlayerHealth playerHealth = aiController.playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(aiController.enemyStats.damage);
            }
            else
            {
                Debug.LogError("AttackState: PlayerHealth component not found on player");
            }
        }

        public override void ExitState(AIController aiController)
        {
            // Cleanup code for exiting Attack state
            aiController.agent.isStopped = false; // Resume movement
            Debug.Log("AttackState: Exited");
        }
    }
}
