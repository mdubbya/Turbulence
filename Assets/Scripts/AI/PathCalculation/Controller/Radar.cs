using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI.PathCalculation
{
    public class Radar : IRadar  
    {
        public float detectionRadius;
        private List<Rigidbody> _enemiesDetected = new List<Rigidbody>();

        private void GetNearby()
        {
            //Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            //List<Rigidbody> rigidBodies = (from p in nearbyColliders select p.GetComponent<Rigidbody>()).ToList();
            //rigidBodies = (from p in rigidBodies
            //               where
            //_teamInfoController.EnemyTeams.Contains(p.GetComponent<TeamInfoController>().OwningTeam)
            //               select p).ToList();

            //_enemiesDetected = rigidBodies;
        }
    }
}