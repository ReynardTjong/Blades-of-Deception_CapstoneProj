using UnityEngine;
using UnityEngine.AI;

namespace BladesOfDeceptionCapstoneProject
{
    public class AIController : MonoBehaviour
    {
        public EnemyStats enemyStats;

        public Transform playerTransform;

        [HideInInspector]
        public NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            
        }

        void Update()
        {
            agent.destination = playerTransform.position;
            
        }
    }
}
