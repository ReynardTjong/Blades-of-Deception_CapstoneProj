using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Idle Animations")]
        public string right_hand_idle;
        public string left_hand_idle;
        public string th_idle;

        [Header("Katana Attack Animations")]
        public string Katana_Light_Attack_1;
        public string Katana_Light_Attack_2;
        public string Katana_Light_Attack_3;
        public string Katana_Light_Attack_4;
        public string Katana_Heavy_Attack_1;
        public string Katana_Heavy_Attack_2;
        public string Katana_Heavy_Attack_3;

        [Header("Stamin Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}
