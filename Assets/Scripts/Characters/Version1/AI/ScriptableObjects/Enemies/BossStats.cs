using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class BossStats : EnemyStats
    {
        public float parryStunDuration;   // Additional parry stun duration
        public float healingOverTime;     // Heal over time
        public float staminaRecoveryRate; // Stamina recovery rate
        public float reducedStaminaUsage; // Reduced stamina usage
        public float damageMultiplier;    // Multiplier for boss's damage
        public float healthMultiplier;    // Multiplier for boss's health
    }
}
