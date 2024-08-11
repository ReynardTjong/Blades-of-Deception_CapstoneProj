using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BladesOfDeceptionCapstoneProject
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image rightWeaponIcon;

        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if (isLeft == false)
            {
                if (weapon.itemIcon != null)
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
                else
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }
            }
        }
    }
}
