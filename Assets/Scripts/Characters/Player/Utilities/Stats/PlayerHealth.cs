using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class PlayerHealth : MonoBehaviour
    {
        public float health = 100f;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            // Handle player's death
            Debug.Log("Player has died");
        }
    }
}
