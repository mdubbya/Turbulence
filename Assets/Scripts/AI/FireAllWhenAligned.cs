using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using AI.Objective;

namespace AI
{
    public class FireAllWhenAligned : MonoBehaviour
    {
        public float targetZoneWidth;
        private ObjectiveInfo _objectiveInfo;
        private List<IWeaponController> weapons;

        public void Start()
        {
            weapons = GetComponentsInChildren<IWeaponController>().ToList();
            _objectiveInfo = GetComponent<ObjectiveInfo>();
        }

        public void FixedUpdate()
        {
            AttackIfTargetValid(_objectiveInfo);
        }


        private void AttackIfTargetValid(ObjectiveInfo objectiveInfo) 
        {
            if (objectiveInfo.targetAcquired && objectiveInfo.targetedEnemy != null)
            {
                Vector3 checkVector = transform.position + ((objectiveInfo.attackTarget - transform.position).magnitude * transform.parent.forward);
                if (Vector3.Distance(checkVector, objectiveInfo.attackTarget) < targetZoneWidth)
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