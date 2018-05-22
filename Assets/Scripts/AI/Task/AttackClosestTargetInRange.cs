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
                return minDistance * relativePriority;
            }
            else
            {
                return 0;
            }
        }

        public Vector3 GetAttackTarget()
        {
            List<GameObject> enemies = _radar.GetDetectedEnemies();
            if(enemies != null && enemies.Count>0)
            {
                return _radar.GetDetectedEnemies().Aggregate(
                    (minItem,NextItem) => 
                    (Vector3.Distance(minItem.transform.position,transform.position) < 
                    Vector3.Distance(NextItem.transform.position,transform.position)) ? minItem : NextItem).transform.position;
            }
            else
            {
                return gameObject.transform.position;
            }
        }

        public Vector3 GetNavigationTarget()
        {
            return GetAttackTarget();
        }
    }
}