using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class WaypointsManager : MonoBehaviour
    {
        public List<Transform> waypoints;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (i + 1 < waypoints.Count)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}
