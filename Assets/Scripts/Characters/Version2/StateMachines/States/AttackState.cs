using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class AttackState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStatistics enemyStatistics, EnemyAnimatorManager enemyAnimatorManager)
        {
            //Select one of player many attacks based on attack scores
            //If the selected attack is not able to be used because of bad angle or 
            //distance, select a new attack
            //If the attack is viable, stop movement and attack target
            //Set recovery timer to the attacks recovery time
            //Return to combat stance state

            return this;
        }
    }
}
