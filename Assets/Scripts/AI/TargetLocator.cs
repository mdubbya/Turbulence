using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI
{
    public class TargetLocator : MonoBehaviour
    {
        public float detectionRadius;
        private TeamInfoController _teamInfoController;
        private AITargetInfo _targetInfo;

        public void Start()
        {
            _teamInfoController = GetComponent<TeamInfoController>();
            _targetInfo = GetComponent<AITargetInfo>();
        }

        public void FixedUpdate()
        {
            UpdatePotentialTargets(_targetInfo);
        }

        private void UpdatePotentialTargets(AITargetInfo targetInfo)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            List<Rigidbody> rigidBodies = (from p in nearbyColliders select p.GetComponent<Rigidbody>()).ToList();
            rigidBodies = (from p in rigidBodies where 
                           _teamInfoController.EnemyTeams.Contains(p.GetComponent<TeamInfoController>().OwningTeam)
                           select p).ToList();

            targetInfo.potentialTargets = rigidBodies;
        }
    }
}
