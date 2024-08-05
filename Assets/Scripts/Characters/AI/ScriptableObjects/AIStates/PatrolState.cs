using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/PatrolState")]
    public class PatrolState : AIState
    {
        [SerializeField] private float patrolSpeed = 2.0f;

        private WaypointsManager waypointsManager;
        private Transform currentWaypoint;

        public override void EnterState(AIController aiController)
        {
            waypointsManager = aiController.waypointsManager;

            if (waypointsManager == null || waypointsManager.waypoints.Count == 0)
            {
                Debug.LogError("No waypoints assigned in WaypointsManager!");
                return;
            }

            aiController.agent.speed = patrolSpeed;
            aiController.agent.isStopped = false;
            currentWaypoint = waypointsManager.GetRandomWaypoint();
            aiController.agent.SetDestination(currentWaypoint.position);
            Debug.Log("PatrolState: Entered state and set destination to " + currentWaypoint.position);

            aiController.animator.CrossFade("Walk", 0.1f);
        }

        public override void UpdateState(AIController aiController)
        {
            if (currentWaypoint == null)
            {
                Debug.LogError("No current waypoint set!");
                return;
            }

            if (!aiController.agent.pathPending && aiController.agent.remainingDistance <= aiController.agent.stoppingDistance)
            {
                // Ensure the agent has stopped moving
                aiController.agent.isStopped = true;
                aiController.agent.ResetPath();
                Debug.Log("PatrolState: Reached waypoint, transitioning to IdleState");
                aiController.TransitionToState(aiController.idleState);
            }

            // Check for player detection to transition to ChaseState
            if (aiController.IsPlayerInFOV())
            {
                Debug.Log("PatrolState: Player detected, transitioning to ChaseState");
                aiController.TransitionToState(aiController.chaseState);
            }
        }

        public override void ExitState(AIController aiController)
        {
            aiController.agent.isStopped = true;
            Debug.Log("PatrolState: Exiting state");
        }
    }
}
