using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public float CurrentHealth
        {
            get { return currentHealth; }
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
            Debug.Log("Player Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Handle player's death
            Debug.Log("Player has died");
            // Implement additional player death logic here
        }
    }
}
