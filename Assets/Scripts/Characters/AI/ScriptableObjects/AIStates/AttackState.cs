using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{

    [CreateAssetMenu(menuName = "AI/States/AttackState")]
    public class AttackState : AIState
    {
        private float attackCooldownTimer;

        public override void EnterState(AIController aiController)
        {
            aiController.agent.isStopped = true;
            attackCooldownTimer = 0f;
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
            Debug.Log("AttackState: Distance to player: " + distanceToPlayer);

            // Check if the player is within attack range
            float attackRange = aiController.enemyStats.attackRange;

            if (distanceToPlayer <= attackRange && IsPlayerInFront(aiController))
            {
                if (attackCooldownTimer <= 0f)
                {
                    PerformAttack(aiController);
                }
                else
                {
                    // Decrease the cooldown timer
                    attackCooldownTimer -= Time.deltaTime;
                }
            }
            else
            {
                aiController.TransitionToState(aiController.chaseState);
            }
        }

        private void PerformAttack(AIController aiController)
        {
            Debug.Log("AttackState: Attacking player, dealing " + aiController.enemyStats.damage + " damage");

            PlayerHealth playerHealth = aiController.playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(aiController.enemyStats.damage);
                Debug.Log("AttackState: Player took damage, remaining health: " + playerHealth.CurrentHealth);

                // Reset attack cooldown timer
                attackCooldownTimer = aiController.enemyStats.attackCooldown;
            }
            else
            {
                Debug.LogError("AttackState: PlayerHealth component not found on player");
            }
        }

        private bool IsPlayerInFront(AIController aiController)
        {
            Vector3 directionToPlayer = (aiController.playerTransform.position - aiController.transform.position).normalized;
            float dotProduct = Vector3.Dot(aiController.transform.forward, directionToPlayer);

            // Check if the player is within a 90-degree cone in front of the enemy
            return dotProduct > Mathf.Cos(90f * Mathf.Deg2Rad / 2f);
        }

        public override void ExitState(AIController aiController)
        {
            aiController.agent.isStopped = false;
            Debug.Log("AttackState: Exited");
        }
    }
}
