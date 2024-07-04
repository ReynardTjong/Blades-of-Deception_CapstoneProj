using UnityEngine;
using UnityEngine.AI;

namespace BladesOfDeceptionCapstoneProject
{
    public class AIController : MonoBehaviour
    {
        public AIState currentState;
        public AIState idleState;
        public AIState chaseState;
        public AIState attackState;

        public Transform player;
        public EnemyStats enemyStats;

        [HideInInspector]
        public NavMeshAgent agent;

        private float currentHealth;
        private float lastAttackTime;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            currentHealth = enemyStats.health;
            agent.speed = enemyStats.movementSpeed;
            ChangeState(idleState);
        }

        private void Update()
        {
            currentState?.Update(this);
        }

        public void ChangeState(AIState newState)
        {
            currentState?.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }

        public bool CanSeePlayer()
        {
            return Vector3.Distance(transform.position, player.position) < enemyStats.detectionRange;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= Mathf.Max(0, damage - enemyStats.armor);
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Handle AI death
        }

        public void DealDamage()
        {
            if (Time.time >= lastAttackTime + enemyStats.attackCooldown)
            {
                // Logic to deal damage to the player
                lastAttackTime = Time.time;
            }
        }
    }
}
