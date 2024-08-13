using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                
                LightAttackCombos(weapon);
                HeavyAttackCombos(weapon);
            }
        }

        public void LightAttackCombos(WeaponItem weapon)
        {
            if (lastAttack == weapon.Katana_Light_Attack_1)
            {
                animatorHandler.PlayTargetAnimation(weapon.Katana_Light_Attack_2, true);
                lastAttack = weapon.Katana_Light_Attack_2;
            }
            else if (lastAttack == weapon.Katana_Light_Attack_2)
            {
                animatorHandler.PlayTargetAnimation(weapon.Katana_Light_Attack_3, true);
                lastAttack = weapon.Katana_Light_Attack_3;

            }
            else if (lastAttack == weapon.Katana_Light_Attack_3)
            {
                animatorHandler.PlayTargetAnimation(weapon.Katana_Light_Attack_4, true);
                lastAttack = weapon.Katana_Light_Attack_4;
            }
        }

        public void HeavyAttackCombos(WeaponItem weapon)
        {
            if (lastAttack == weapon.Katana_Heavy_Attack_1)
            {
                animatorHandler.PlayTargetAnimation(weapon.Katana_Heavy_Attack_2, true);
                lastAttack = weapon.Katana_Heavy_Attack_2;
            }
            else if (lastAttack == weapon.Katana_Heavy_Attack_2)
            {
                animatorHandler.PlayTargetAnimation(weapon.Katana_Heavy_Attack_3, true);
                lastAttack = weapon.Katana_Heavy_Attack_3;

            }
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.Katana_Light_Attack_1, true);
            lastAttack = weapon.Katana_Light_Attack_1;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.Katana_Heavy_Attack_1, true);
            lastAttack = weapon.Katana_Heavy_Attack_1;
        }
    }
}
