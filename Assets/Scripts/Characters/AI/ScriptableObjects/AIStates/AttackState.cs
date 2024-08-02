using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{

    [CreateAssetMenu(menuName = "AI/States/AttackState")]
    public class AttackState : AIState
    {
        public override void EnterState(AIController aiController)
        {
            aiController.agent.isStopped = true;
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

            if (distanceToPlayer <= attackRange)
            {
                PerformAttack(aiController);
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
                Debug.Log("AttackState: Player took damage, remaining health: " + playerHealth.health);
            }
            else
            {
                Debug.LogError("AttackState: PlayerHealth component not found on player");
            }
        }

        public override void ExitState(AIController aiController)
        {
            aiController.agent.isStopped = false;
            Debug.Log("AttackState: Exited");
        }
    }
}
