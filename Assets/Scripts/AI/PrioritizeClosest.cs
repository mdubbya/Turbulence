using UnityEngine;
using System.Linq;

namespace AI
{
    public class PrioritizeClosest : TargetPrioritizer
    {
        public float detectionRadius;

        public int priority;

        private TeamInfoController teamInfoController;

        public void Start()
        {
            teamInfoController = GetComponent<TeamInfoController>();
        }

        public override void UpdateTargetInfo(AITargetInfo targetInfo)
        {
            Vector3 currentPosition = transform.position;
                        
            //find the closest rigidBody in the detection radius
            if (targetInfo!=null && targetInfo.potentialTargets.Count > 0)
            {
                targetInfo.enemyRigidBody = targetInfo.potentialTargets.OrderBy(p => Vector3.Distance(transform.position, p.position)).First();
                targetInfo.targetAcquired = true;
            }
        }
    }

}