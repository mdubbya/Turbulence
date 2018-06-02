using System;
using System.Collections.Generic;
using System.Linq;
using AI.PathCalculation;
using UnityEngine;
using Zenject;

namespace AI.Task
{
    //TODO: needs optimization
    public class AttackClosestTargetInRange : MonoBehaviour, IAIAttackTargetTask, IAIMoveTargetTask
    {
        public int relativePriority;
        public float followDistance;

        private IRadar _radar;

        [Inject]
        public void Inject(IRadar radar)
        {
            _radar = radar;
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

        public Vector3 GetAttackTarget()
        {
            GameObject closestEnemy = _radar.GetClosestDetectedEnemy();
            return closestEnemy == null ? transform.position : closestEnemy.transform.position;
        }

        public Vector3 GetNavigationTarget()
        {
            Vector3 navTarget = GetAttackTarget();
            navTarget = ((transform.position-navTarget).normalized * followDistance) + navTarget;
            
            return navTarget;
        }
    }
}