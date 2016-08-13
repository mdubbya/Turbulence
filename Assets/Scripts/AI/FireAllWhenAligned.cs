using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using AI.Process;

namespace AI
{
    public class FireAllWhenAligned : MonoBehaviour
    {
        public float targetZoneWidth;
        private CommandInfo _commandInfo;
        private List<IWeaponController> weapons;

        public void Start()
        {
            weapons = GetComponentsInChildren<IWeaponController>().ToList();
            _commandInfo = GetComponent<CommandInfo>();
        }


        private void FixedUpdate() 
        {
            if (_commandInfo != null)
            {
                if (_commandInfo.targetAcquired && _commandInfo.targetedEnemy != null)
                {
                    Vector3 checkVector = transform.position + ((_commandInfo.attackTarget - transform.position).magnitude * transform.parent.forward);
                    if (Vector3.Distance(checkVector, _commandInfo.attackTarget) < targetZoneWidth)
                    {
                        foreach (IWeaponController weapon in weapons)
                        {
                            weapon.Fire();
                        }
                    }
                }
            }
        }

    }
}