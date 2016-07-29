using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AI
{
    public class FireAllAttackHarness : MonoBehaviour
    {
        public float targetZoneWidth;
        public float targetingDistance;
        private List<IWeaponController> weapons;
        private Rigidbody rigidBody;


        public void Start()
        {
            weapons = GetComponentsInChildren<IWeaponController>().ToList();
            rigidBody = GetComponent<Rigidbody>();
        }


        public void AttackIfTargetValid(AITargetInfo targetInfo) 
        {
            if (targetInfo.targetAcquired && targetInfo.enemyRigidBody != null)
            {
                Vector3 checkVector = transform.position + ((targetInfo.attackTarget - transform.position).magnitude * transform.parent.forward);
                if (Vector3.Distance(checkVector, targetInfo.attackTarget) < targetZoneWidth)
                {
                    foreach(IWeaponController weapon in weapons)
                    {
                        weapon.Fire();
                    }
                }
            }
        }


        private Vector3 GetNewTargetPosition(Rigidbody objectToIntercept)
        {
            List<IWeaponController> weapons = GetComponentsInChildren<IWeaponController>().ToList();

            float projectileSpeed = (from p in weapons select p.weaponOutputSpeed).Average();

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

        private AITargetInfo GetNewTargetInfo(AITargetInfo targetInfo)
        {
            if (targetInfo.targetAcquired && targetInfo.enemyRigidBody != null)
            {
                Vector3 updatedPosition = GetNewTargetPosition(targetInfo.enemyRigidBody);
                //return new AITargetInfo(targetInfo.moveTarget, true, updatedPosition, targetInfo.enemyRigidBody);
                return null;
            }
            else
            {
                return targetInfo;
            }
        }

    }
}