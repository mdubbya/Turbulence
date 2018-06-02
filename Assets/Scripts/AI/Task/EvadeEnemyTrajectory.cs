using System.Collections.Generic;
using System.Linq;
using AI.PathCalculation;
using UnityEngine;
using Zenject;

namespace AI.Task
{
    //TODO: needs optimization
    public class EvadeEnemyTrajectory : MonoBehaviour, IAIMoveTargetTask
    {
        public int relativePriority;
        public float enemyTrajectoryLength;

        public float evadeDistance;
        private IRadar _radar;

        [Inject]
        public void Inject(IRadar radar)
        {
            _radar = radar;
        }

        public Vector3 GetNavigationTarget()
        {
            GameObject enemy = GetEnemyToAvoid();            
            if(enemy!=null)
            {
                Vector3 evadeTarget = PathCalculationUtilities.ClosestPointOnLine(
                    enemy.transform.position,enemy.transform.position +(enemy.transform.forward*enemyTrajectoryLength),transform.position);
                
                return (((transform.position - evadeTarget).normalized) + transform.position) * evadeDistance;
            }
            else
            {
                return transform.position;
            }
        }

        private GameObject GetEnemyToAvoid()
        {
             List<GameObject> detectedEnemies = _radar.GetDetectedEnemies();
            if(detectedEnemies != null)
            {
                return detectedEnemies.Aggregate( (current,next) =>
                
                    Vector3.Distance(current.transform.position,
                        PathCalculationUtilities.ClosestPointOnLine(
                        current.transform.position,current.transform.position + (current.transform.forward*enemyTrajectoryLength),transform.position)) <
                    Vector3.Distance(next.transform.position,
                        PathCalculationUtilities.ClosestPointOnLine(
                        next.transform.position,next.transform.position + (next.transform.forward*enemyTrajectoryLength),transform.position)) ?
                    current : next
                );
            }
            else
            {
                return null;
            }
        }

        public float GetPriority()
        {
            Vector3 target = GetNavigationTarget();
            if(target != transform.position)
            {
                return (relativePriority / Vector3.Distance(target,transform.position));
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Callback to draw gizmos only if the object is selected.
        /// </summary>
        // void OnDrawGizmosSelected()
        // {
        //     GameObject enemy = GetEnemyToAvoid();
        //     if(enemy!=null)
        //     {
        //         //Debug.DrawLine(GetNavigationTarget(),enemy.transform.position,Color.blue,0.1f);
        //         Debug.DrawLine(GetNavigationTarget(),transform.position,Color.blue,0.1f);
        //         DebugExtension.DrawPoint(GetEnemyToAvoid().transform.position,Color.green,3f);
        //     }
        // }
    }
}