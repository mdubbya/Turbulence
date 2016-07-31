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
            float projectileSpeed = (from p in _weapons select p.weaponOutputSpeed).Average();

            Vector3? newPosition = InterceptionCalculator.FirstOrderIntercept(transform.position,
                                                                              _rigidBody.velocity,
                                                                              projectileSpeed,
                                                                              _targetInfo.enemyRigidBody.position,
                                                                              _targetInfo.enemyRigidBody.velocity);

            if (newPosition != null)
            {
                _targetInfo.attackTarget = newPosition.Value;
            }
        }
    }
}
