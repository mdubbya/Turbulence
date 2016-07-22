using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public class SelectClosestEnemy : MonoBehaviour, ITargetModifier
    {
        public float detectionRadius;
        
        [SerializeField]
        int _priority;
        public int priority
        {
            get{ return _priority; }

            set { _priority = value; }
        }

        private TeamInfoController teamInfoController;

        public void Start()
        {
            teamInfoController = GetComponent<TeamInfoController>();
        }


        public AITargetInfo GetNewTargetInfo(AITargetInfo targetInfo)
        {
            Vector3 currentPosition = transform.position;
            
            Collider[] nearbyColliders = Physics.OverlapSphere(currentPosition, detectionRadius);
            
            //find the closest collider in the deteciton radius
            Collider closest = new Collider();
            bool colliderFound = false;
            float distance = float.MaxValue;
            if (nearbyColliders.Length > 1)
            {
                foreach (Collider col in nearbyColliders)
                {
                    TeamInfoController otherTeamInfo = col.gameObject.transform.GetComponent<TeamInfoController>();
                    if (otherTeamInfo != null)
                    {
                        if (col != transform.GetComponentInChildren<Collider>() &&
                            teamInfoController.EnemyTeams.Contains(otherTeamInfo.OwningTeam))
                        {
                            float newDistance = (col.transform.position - currentPosition).magnitude;
                            if (newDistance < distance)
                            {
                                closest = col;
                                colliderFound = true;
                            }
                        }
                    }
                }
            }
            else
            {
                closest = nearbyColliders[0];
                colliderFound = true;
            }

            if (colliderFound)
            {
                DebugExtension.DebugPoint(closest.transform.position, Color.green, 2, Time.fixedDeltaTime * 4);
                return new AITargetInfo(targetInfo.moveTarget,true,closest.attachedRigidbody.position, closest.attachedRigidbody);
                
            }
            else
            {
                return targetInfo;
            }
        }

        public void Update()
        {
            

        }
    }

}