using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class AIManager : MonoBehaviour
    {
        public List<AIController> aiControllers;

        void Update()
        {
            foreach (var aiController in aiControllers)
            {
                // Global AI management logic if necessary
            }
        }
    }
}
