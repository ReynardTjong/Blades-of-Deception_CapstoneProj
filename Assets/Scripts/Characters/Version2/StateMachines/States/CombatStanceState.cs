using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class CombatStanceState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStatistics enemyStatistics, EnemyAnimatorManager enemyAnimatorManager)
        {
            //Check for attack range
            //potentially circle player or walk around them
            //if in attack range return attack State
            //If in a cool down after attacking, return this state and continue circling player
            //Return the combat stance state
            return this;
        }
    }
}
