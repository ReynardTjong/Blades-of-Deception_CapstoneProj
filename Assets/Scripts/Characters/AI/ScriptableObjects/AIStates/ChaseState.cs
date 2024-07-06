using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "AI/States/ChaseState")]
    public class ChaseState : AIState
    {
        public override void Enter(AIController ai)
        {
            // Enter chase logic
        }

        public override void Exit(AIController ai)
        {
            // Exit chase logic
            ai.agent.ResetPath();
        }

        public override void UpdateState(AIController ai)
        {
            // Chase update logic
            ai.agent.SetDestination(ai.player.position);

            if (Vector3.Distance(ai.transform.position, ai.player.position) < ai.enemyStats.attackRange)
            {
                ai.ChangeState(ai.attackState);
            }
            else if (!ai.CanSeePlayer())
            {
                ai.ChangeState(ai.idleState);
            }
        }
    }
}
