using System;
using System.Collections.Generic;
using System.Linq;
using AI.PathCalculation;
using UnityEngine;
using Zenject;

namespace AI.Task
{
    //TODO: needs optimization
    public class AttackClosestTargetInRange : MonoBehaviour, IAITask
    {
        public float relativePriority;
        public float followDistance;
        public float targetDeadZone;

        private IRadar _radar;
        private RVOAgent _rvoAgent;
        private ShipMovementProperties _shipMovementProperties;

        [Inject]
        public void Inject(IRadar radar, RVOAgent rvoAgent, ShipMovementProperties shipMovementProperties)
        {
            _radar = radar;
            _rvoAgent = rvoAgent;
            _shipMovementProperties = shipMovementProperties;
        }

        public float GetPriority()
        {
            GameObject target = _radar.GetClosestDetectedEnemy();
            if(target != null)
            {
                return relativePriority / Vector3.Distance(target.transform.position,transform.position);
            }
            else
            {
                return 0;
            }
        }

        public Vector3 GetTarget()
        {
            GameObject enemy = _radar.GetClosestDetectedEnemy();
            Vector3 target = Vector3.zero;
            if(enemy != null)
            {
                target = enemy.transform.position;
                GameObject closest = _radar.GetClosestDetected();

                Vector3 closestVector = closest==null ? target : closest.transform.position;

                target = Vector3.Distance(closestVector,transform.position) < targetDeadZone ? 
                _rvoAgent.GetAdjustedTargetPosition(((transform.position-target).normalized * followDistance) + target,_shipMovementProperties.maxSpeed) : target;
            }
            return target;
        }
    }
}