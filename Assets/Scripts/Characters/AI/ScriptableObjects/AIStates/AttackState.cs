using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{

    [CreateAssetMenu(menuName = "AI/States/AttackState")]
    public class AttackState : AIState
    {
        public override void Enter(AIController ai)
        {
            // Enter attack logic
        }

        public override void Exit(AIController ai)
        {
            // Exit attack logic
        }

        public override void UpdateState(AIController ai)
        {
            // Attack update logic
            if (Vector3.Distance(ai.transform.position, ai.player.position) > ai.enemyStats.attackRange)
            {
                ai.ChangeState(ai.chaseState);
            }
            else
            {
                ai.DealDamage();
            }
        }
    }
}
