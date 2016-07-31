using UnityEngine;
using System.Linq;

namespace AI
{
    public class PrioritizeClosest : TargetPrioritizer
    {
        public float detectionRadius;
        public int priority;

        private AITargetInfo _targetInfo;
        private Radar _radar;

        public void Start()
        {
            _targetInfo = GetComponent<AITargetInfo>();
            _radar = GetComponent<Radar>();
        }

        public override void UpdateTargetInfo()
        {
            //find the closest rigidBody in the detection radius
            if (_targetInfo!=null && _radar.enemiesDetected.Count > 0)
            {
                _targetInfo.enemyRigidBody = _radar.enemiesDetected.OrderBy(p => Vector3.Distance(transform.position, p.position)).First();
                _targetInfo.targetAcquired = true;
            }
        }
    }

}