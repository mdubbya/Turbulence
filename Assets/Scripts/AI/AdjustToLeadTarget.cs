using System;
using UnityEngine;

namespace AI
{
    public class AdjustToLeadTarget : MonoBehaviour , ITargetModifier
    {
        [SerializeField]
        private int _priority;
        public int priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private Rigidbody rigidBody;


        public void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        
        public Vector3 GetNewTargetPosition(Rigidbody objectToIntercept)
        {
            float projectileSpeed = GetComponent<AIProjectileWeaponController>().projectilePrefab.GetComponent<ProjectileController>().projectileSpeed;

            Vector3? newPosition = InterceptionCalculator.FirstOrderIntercept(transform.position, 
                                                                              rigidBody.velocity, 
                                                                              projectileSpeed, 
                                                                              objectToIntercept.position, 
                                                                              objectToIntercept.velocity);
            
            
            if (newPosition != null)
            {
                return newPosition.Value;
            }
            else
            {
                return objectToIntercept.position;
            }
        }

        public AITargetInfo GetNewTargetInfo(AITargetInfo targetInfo)
        {
            if (targetInfo.targetAcquired && targetInfo.enemyRigidBody != null)
            {
                Vector3 updatedPosition = GetNewTargetPosition(targetInfo.enemyRigidBody);
                return new AITargetInfo(targetInfo.moveTarget,true,updatedPosition,targetInfo.enemyRigidBody);
            }
            else
            {
                return targetInfo;
            }
        }
    }
}
