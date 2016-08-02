using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class LeadTarget : FlightManeuverBase
    {

        private Rigidbody _rigidBody;
        private List<IWeaponController> _weapons;
        private AITargetInfo _targetInfo;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _weapons = GetComponentsInChildren<IWeaponController>().ToList();
            _targetInfo = GetComponent<AITargetInfo>();
        }


        public override void UpdateForManeuver()
        {
            Rigidbody enemyRigidBody = _targetInfo.enemy != null ? _targetInfo.enemy.GetComponent<Rigidbody>() : null;
            if (enemyRigidBody != null)
            {
                float projectileSpeed = (from p in _weapons select p.weaponOutputSpeed).Average();

                Vector3? newPosition = InterceptionCalculator.FirstOrderIntercept(transform.position,
                                                                                  _rigidBody.velocity,
                                                                                  projectileSpeed,
                                                                                  enemyRigidBody.position,
                                                                                  enemyRigidBody.velocity);

                if (newPosition != null)
                {
                    _targetInfo.attackTarget = newPosition.Value;
                }
            }
        }
    }
}
