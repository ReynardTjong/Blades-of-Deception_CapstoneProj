using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class DamageDealer : MonoBehaviour
    {
        bool canDealDamage;
        List<GameObject> hasDealtDamage;

        [SerializeField] float weaponLength;
        [SerializeField] float weaponDamage;

        void Start()
        {
            canDealDamage = false;
            hasDealtDamage = new List<GameObject>();
        }

        void Update()
        {
            if (canDealDamage)
            {
                RaycastHit hit;
                int layerMask = 1 << 9; // Assuming layer 9 is the enemy layer

                if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
                {
                    if (hit.transform.TryGetComponent(out AIController aiController) && !hasDealtDamage.Contains(hit.transform.gameObject))
                    {
                        aiController.TakeDamage(weaponDamage); // Call the AIController's TakeDamage method
                        aiController.HitVFX(hit.point); // Call the AIController's HitVFX method
                        hasDealtDamage.Add(hit.transform.gameObject);
                    }
                }
            }
        }

        public void StartDealDamage()
        {
            canDealDamage = true;
            hasDealtDamage.Clear();
        }

        public void EndDealDamage()
        {
            canDealDamage = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
        }
    }
}
