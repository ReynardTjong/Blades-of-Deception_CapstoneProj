using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/PatrolState")]
    public class PatrolState : AIState
    {
        public List<Transform> patrolPoints;
        private int currentPatrolIndex;

        public float patrolSpeed = 2.0f;

        public override void EnterState(AIController aiController)
        {
            if (patrolPoints.Count == 0)
            {
                Debug.LogError("No patrol points assigned!");
                return;
            }

            aiController.agent.speed = patrolSpeed;
            aiController.agent.isStopped = false;
            currentPatrolIndex = 0;
            aiController.agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        public override void UpdateState(AIController aiController)
        {
            if (aiController.agent.remainingDistance <= aiController.agent.stoppingDistance)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                aiController.agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }

            // Check for player detection to transition to ChaseState
            if (aiController.IsPlayerInFOV())
            {
                aiController.TransitionToState(aiController.chaseState);
            }
        }

        public override void ExitState(AIController aiController)
        {
            aiController.agent.isStopped = true;
        }
    }
}
