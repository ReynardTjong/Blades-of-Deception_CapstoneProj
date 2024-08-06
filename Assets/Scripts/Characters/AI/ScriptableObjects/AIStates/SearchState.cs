using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/SearchState")]
    public class SearchState : AIState
    {
        [SerializeField] private float searchRadius = 5.0f; // Radius to search within
        [SerializeField] private float searchDuration = 10.0f; // Time to spend searching
        [SerializeField] private float idleDuration = 2.0f; // Time to stay idle during search
        [SerializeField] private float walkSpeed = 1.5f; // Speed while searching

        private float searchTimer;
        private float idleTimer;
        private bool isIdle;

        public override void EnterState(AIController aiController)
        {
            searchTimer = searchDuration;
            idleTimer = idleDuration;
            isIdle = true;
            aiController.agent.speed = walkSpeed;
            aiController.agent.isStopped = true;
            Debug.Log("SearchState: Entered, starting search timer with duration: " + searchDuration);
            aiController.animator.CrossFade("Search", 0.1f);
        }

        public override void UpdateState(AIController aiController)
        {
            searchTimer -= Time.deltaTime;

            if (searchTimer <= 0)
            {
                Debug.Log("SearchState: Search duration elapsed, transitioning to PatrolState");
                aiController.TransitionToState(aiController.patrolState);
                return;
            }

            if (isIdle)
            {
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    // Transition to walking around within the search radius
                    isIdle = false;
                    idleTimer = idleDuration;
                    Vector3 randomPoint = aiController.transform.position + Random.insideUnitSphere * searchRadius;
                    randomPoint.y = aiController.transform.position.y; // Ensure the point is on the same plane
                    aiController.agent.SetDestination(randomPoint);
                    aiController.agent.isStopped = false;
                    Debug.Log("SearchState: Walking to a new random point within radius: " + randomPoint);
                }
            }
            else
            {
                if (aiController.agent.remainingDistance <= aiController.agent.stoppingDistance)
                {
                    // Reached the random point, transition back to idle
                    isIdle = true;
                    aiController.agent.isStopped = true;
                    Debug.Log("SearchState: Reached random point, idling");
                }
            }

            // Check for player detection to transition to ChaseState
            if (aiController.IsPlayerInFOV())
            {
                Debug.Log("SearchState: Player detected, transitioning to ChaseState");
                aiController.TransitionToState(aiController.chaseState);
            }
        }

        public override void ExitState(AIController aiController)
        {
            aiController.agent.isStopped = true;
            Debug.Log("SearchState: Exited");
        }
    }
}
