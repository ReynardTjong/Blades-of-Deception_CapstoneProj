using UnityEngine;
using UnityEngine.AI;

namespace BladesOfDeceptionCapstoneProject
{
    public class AIController : MonoBehaviour
    {
        public AIState currentState;
        public Transform playerTransform;
        public NavMeshAgent agent;
        public float detectionRange = 10.0f; // Detection range
        public float fieldOfViewAngle = 120.0f; // Field of view angle

        public AIState idleState; // Reference to IdleState
        public AIState chaseState; // Reference to ChaseState
        public AIState patrolState; // Reference to PatrolState

        private WaypointsManager waypointsManager;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            waypointsManager = FindObjectOfType<WaypointsManager>();

            // Assign waypoints to the patrol state if it's not already done
            PatrolState patrolStateScript = patrolState as PatrolState;
            if (patrolStateScript != null && patrolStateScript.patrolPoints.Count == 0)
            {
                patrolStateScript.patrolPoints = waypointsManager.waypoints;
            }

            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }

        void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState(this);
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
    }
}
