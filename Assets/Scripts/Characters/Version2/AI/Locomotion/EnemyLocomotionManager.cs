using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        public Rigidbody enemyRigidbody;

        public CharacterStats currentTarget;
        public LayerMask detectionLayer;

        

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            enemyRigidbody.isKinematic = false;
        }
    }
}
