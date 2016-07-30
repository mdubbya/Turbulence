using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AI
{
    public class FireAllAttackHarness : MonoBehaviour
    {
        public float targetZoneWidth;
        private AITargetInfo _targetInfo;
        private List<IWeaponController> weapons;

        public void Start()
        {
            weapons = GetComponentsInChildren<IWeaponController>().ToList();
        }

        public void FixedUpdate()
        {
            AttackIfTargetValid(_targetInfo);
        }


        private void AttackIfTargetValid(AITargetInfo targetInfo) 
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

    }
}