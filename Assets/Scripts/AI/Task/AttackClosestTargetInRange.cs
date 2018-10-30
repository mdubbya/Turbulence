using System;
using System.Collections.Generic;
using System.Linq;
using AI.PathCalculation;
using ShipComponent;
using UnityEngine;


namespace AI.Task
{
    //TODO: needs optimization
    public class AttackClosestTargetInRange : MonoBehaviour, IAITask
    {
        public float relativePriority;
        public float followDistance;
        public float targetDeadZone;

        public float attackRange;
        public float attackZoneWidth;

        private IRadar _radar;
        private RVOAgent _rvoAgent;
        private ShipMovementProperties _shipMovementProperties;
        
        private IWeapon _weapon;
 
       
        public void Start()
        {
            _radar = gameObject.GetComponent<IRadar>();
            _rvoAgent = GetComponent<RVOAgent>();
            _shipMovementProperties = GetComponent<ShipMovementProperties>();
        }


        public float GetPriority()
        {
            GameObject target = _radar.GetClosestDetectedEnemy();
            if(target != null && _weapon != null)
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
            if(enemy != null && _weapon != null)
            {
                target = _weapon.GetTargetingVector(enemy.transform.position,enemy.GetComponent<Rigidbody>().velocity);
                
                GameObject closest = _radar.GetClosestDetected();

                Vector3 closestVector = closest==null ? target : closest.transform.position;

                bool inDeadzone = Vector3.Distance(closestVector,transform.position) < targetDeadZone;
                target = inDeadzone ? 
                _rvoAgent.GetAdjustedTargetPosition(((transform.position-target).normalized * followDistance) + target,_shipMovementProperties.maxSpeed) : target;

                if(!inDeadzone)
                {
                    if(Vector3.Distance(
                        PathCalculationUtilities.ClosestPointOnLine(
                            transform.position,(transform.forward * attackRange)+transform.position,target),target) < attackZoneWidth)
                    {
                        _weapon.Fire();
                    }
                }
            }

            

            return target;
        }
    }
}