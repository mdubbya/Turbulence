using UnityEngine;
using Zenject;
using AI.PathCalculation;

namespace AI.Task
{
    
    public class HoldPoint : MonoBehaviour, IAITask
    {
        public Vector3 holdPoint;
        public float relativePriority;

        private RVOAgent _rvoAgent;
        private ShipMovementProperties _shipMovementProperties;

        [Inject]
        public void Inject(RVOAgent rvoAgent, ShipMovementProperties shipMovementProperties)
        {
            _rvoAgent = rvoAgent;
            _shipMovementProperties = shipMovementProperties;
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