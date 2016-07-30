using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class VectorModifierLeadTarget : VectorModifier
    {

        private Rigidbody _rigidBody;
        private List<IWeaponController> _weapons;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _weapons = GetComponentsInChildren<IWeaponController>().ToList();
        }


        public override void ModifyAttackVector(AITargetInfo targetInfo)
        {           
            float projectileSpeed = (from p in _weapons select p.weaponOutputSpeed).Average();

            Vector3? newPosition = InterceptionCalculator.FirstOrderIntercept(transform.position,
                                                                              _rigidBody.velocity,
                                                                              projectileSpeed,
                                                                              targetInfo.enemyRigidBody.position,
                                                                              targetInfo.enemyRigidBody.velocity);

            if (newPosition != null)
            {
                targetInfo.attackTarget = newPosition.Value;
            }
        }
    }
}
