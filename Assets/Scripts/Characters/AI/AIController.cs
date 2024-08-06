using UnityEngine;
using UnityEngine.AI;

namespace BladesOfDeceptionCapstoneProject
{
    public class AIController : MonoBehaviour
    {
        public AIState currentState;
        public Transform playerTransform;
        public NavMeshAgent agent;
        public Animator animator; // Add Animator reference
        [SerializeField] private float detectionRange = 10.0f; // Detection range
        [SerializeField] private float fieldOfViewAngle = 120.0f; // Field of view angle

        public AIState idleState; // Reference to IdleState
        public AIState chaseState; // Reference to ChaseState
        public AIState patrolState; // Reference to PatrolState
        public AIState searchState; // Reference to SearchState
        public AIState attackState; // Reference to AttackState

        public EnemyStats enemyStats;
        [HideInInspector] public BossStats bossStats;
        public WaypointsManager waypointsManager;

        private float currentHealth;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            currentHealth = enemyStats != null ? enemyStats.health : bossStats.health;

            if (currentState != null)
            {
                currentState.EnterState(this);
            }

            ApplyStats();
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState(this);
            }
        }

        void ApplyStats()
        {
            if (bossStats != null)
            {
                agent.speed = bossStats.movementSpeed;
                // Apply other boss stats as needed
            }
            else
            {
                agent.speed = enemyStats.movementSpeed;
                // Apply other enemy stats as needed
            }
        }

        public void TransitionToState(AIState newState)
        {
            if (currentState != null)
            {
                currentState.ExitState(this);
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player detected within attack range.");
                TransitionToState(attackState);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player exited attack range.");
                TransitionToState(chaseState);
            }
        }

        public bool IsPlayerInFOV()
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer <= detectionRange)
            {
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
                if (angleToPlayer <= fieldOfViewAngle / 2)
                {
                    return true;
                }
            }
            return false;
        }

        // New methods for handling damage and hit effects
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void HitVFX(Vector3 hitPoint)
        {
            // Implement visual effects for being hit
            Debug.Log("Hit VFX at: " + hitPoint);
        }

        private void Die()
        {
            // Handle death logic
            Debug.Log("Enemy died.");
            // Optionally, transition to a death state, play death animation, etc.
            Destroy(gameObject);
        }
    }
}
