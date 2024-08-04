using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "AI/Enemy Stats", order = 1)]
    public class EnemyStats : ScriptableObject
    {
        public float health;
        public float damage;
        public float attackRange;
        public float attackCooldown;
        public float movementSpeed;   
        public float attackSpeed;
    }
}
