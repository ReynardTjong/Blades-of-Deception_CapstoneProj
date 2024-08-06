using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{

    [CreateAssetMenu(menuName = "AI/States/AttackState")]
    public class AttackState : AIState
    {
        private float attackCooldownTimer;
        [SerializeField] private string[] attackAnimations; // Array to hold multiple attack animation names
        [SerializeField] private float crossfadeDuration = 0.1f; // Duration of the crossfade

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

            float distanceToPlayer = Vector3.Distance(aiController.transform.position, aiController.playerTransform.position);
            Debug.Log("AttackState: Distance to player: " + distanceToPlayer);

            float attackRange = aiController.bossStats != null ? aiController.bossStats.attackRange : aiController.enemyStats.attackRange;
            float attackCooldown = aiController.bossStats != null ? aiController.bossStats.attackCooldown : aiController.enemyStats.attackCooldown;

            if (distanceToPlayer <= attackRange && IsPlayerInFront(aiController))
            {
                if (attackCooldownTimer <= 0f)
                {
                    PerformAttack(aiController);
                }
                else
                {
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
            Debug.Log("AttackState: Attacking player");

            PlayerHealth playerHealth = aiController.playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                float damage = aiController.bossStats != null ? aiController.bossStats.damage : aiController.enemyStats.damage;
                playerHealth.TakeDamage(damage);
                Debug.Log("AttackState: Player took damage, remaining health: " + playerHealth.CurrentHealth);

                attackCooldownTimer = aiController.bossStats != null ? aiController.bossStats.attackCooldown : aiController.enemyStats.attackCooldown;

                // Randomly select an attack animation and crossfade to it
                if (attackAnimations.Length > 0)
                {
                    string attackAnimation = attackAnimations[Random.Range(0, attackAnimations.Length)];
                    aiController.animator.CrossFade(attackAnimation, crossfadeDuration);
                }
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
