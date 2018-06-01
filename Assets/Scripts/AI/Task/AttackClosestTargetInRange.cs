using System;
using System.Collections.Generic;
using System.Linq;
using AI.PathCalculation;
using UnityEngine;
using Zenject;

namespace AI.Task
{
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
            List<GameObject> enemies = _radar.GetDetectedEnemies();
            if(enemies != null && enemies.Count>0)
            {
                float minDistance = _radar.GetDetectedEnemies().Min(p=> Vector3.Distance(p.transform.position,transform.position));
                return relativePriority/minDistance;
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