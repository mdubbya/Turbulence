using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Objective;

namespace AI
{
    public class LeadTarget : FlightManeuverBase
    {

        private Rigidbody _rigidBody;
        private List<IWeaponController> _weapons;
        private ObjectiveInfo _targetInfo;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _weapons = GetComponentsInChildren<IWeaponController>().ToList();
            _targetInfo = GetComponent<ObjectiveInfo>();
        }


        public override Vector3 UpdateForManeuver()
        {
            Rigidbody enemyRigidBody = _targetInfo.targetedEnemy != null ? _targetInfo.targetedEnemy.GetComponent<Rigidbody>() : null;
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
                    return newPosition.Value;
                }
                else
                {
                    return new Vector3();
                }
            }
            else
            {
                return new Vector3();
            }
        }
    }
}
