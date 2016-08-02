using UnityEngine;
using System.Linq;
using AI.Objective;


namespace AI
{
    public class PrioritizeTargets : MonoBehaviour
    {
        public float detectionRadius;
        public int priority;

        private ObjectiveInfo _objectiveInfo;
        private Radar _radar;

        public void Start()
        {
            _objectiveInfo = GetComponent<ObjectiveInfo>();
            _radar = GetComponent<Radar>();
        }

        public void Calculate()
        {
            //find the closest rigidBody in the detection radius
            if (_objectiveInfo != null && _radar.enemiesDetected.Count > 0)
            {
                //_targetInfo.enemy = _radar.enemiesDetected.OrderBy(p => Vector3.Distance(transform.position, p.position)).First().gameObject;
                //_targetInfo.targetAcquired = true;
            }
        }
    }
}
