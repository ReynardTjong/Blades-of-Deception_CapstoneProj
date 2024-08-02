using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "AI/Enemy Stats", order = 1)]
    public class EnemyStats : ScriptableObject
    {
        public float health;
        public float damage;
        public float attackRange;

        /*public float health; // The amount of damage the enemy can take before being defeated.
        public float damage; // The amount of damage the enemy deals to the player per attack.
        public float walkSpeed;
        public float runSpeed;
        public float attackSpeed; // How quickly the enemy can execute attacks.
        public float movementSpeed; // How fast the enemy moves when chasing or patrolling.
        public float attackRange; // The range within which the enemy can hit the player.
        public float armor; // If applicable, this could reduce the damage taken from player attacks.
        public float aggroRadius; // The radius within which the enemy will become aggressive towards the player.
        public float attackCooldown; // The time between consecutive attacks.
        public float defense; // This represents evasion or block chances.
        */
    }
}
