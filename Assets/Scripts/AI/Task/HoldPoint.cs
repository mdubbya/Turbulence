using UnityEngine;

using AI.PathCalculation;

namespace AI.Task
{
    
    public class HoldPoint : MonoBehaviour, IAITask
    {
        public Vector3 holdPoint;
        public float relativePriority;

        private RVOAgent _rvoAgent;
        private ShipMovementProperties _shipMovementProperties;
        public AITaskType taskType { get { return AITaskType.Move; } }       
        public void Start()
        {
            _rvoAgent = GetComponent<RVOAgent>();
            _shipMovementProperties = GetComponent<ShipMovementProperties>();
        }

        public Vector3 GetTarget()
        {
            return _rvoAgent.GetAdjustedTargetPosition(holdPoint,_shipMovementProperties.maxSpeed);
        }

        public float GetPriority()
        {
            return relativePriority * Vector3.Distance(transform.position,holdPoint);
        }
    }
}