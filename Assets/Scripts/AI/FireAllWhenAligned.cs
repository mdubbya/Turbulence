using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using AI.Objective;

namespace AI
{
    public class FireAllWhenAligned : MonoBehaviour
    {
        public float targetZoneWidth;
        private ObjectiveInfo _targetInfo;
        private List<IWeaponController> weapons;

        public void Start()
        {
            weapons = GetComponentsInChildren<IWeaponController>().ToList();
            _targetInfo = GetComponent<ObjectiveInfo>();
        }

        public void FixedUpdate()
        {
            AttackIfTargetValid(_targetInfo);
        }


        private void AttackIfTargetValid(ObjectiveInfo targetInfo) 
        {
            if (targetInfo.targetAcquired && targetInfo.targetedEnemy != null)
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