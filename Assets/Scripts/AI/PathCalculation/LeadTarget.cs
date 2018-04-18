using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Process;

namespace AI.PathCalculation
{
    public class LeadTarget
    {

        private Rigidbody _rigidBody;
        private List<IWeaponController> _weapons;

        public void UpdateObjectiveInfo(Rigidbody enemyRigidBody)
        {
            //if (_commandInfo != null)
            //{
            //    Rigidbody enemyRigidBody = _commandInfo.targetedEnemy != null ? _commandInfo.targetedEnemy.GetComponent<Rigidbody>() : null;
            //    if (enemyRigidBody != null && _weapons.Count>0)
            //    {
            //        float projectileSpeed = (from p in _weapons select p.weaponOutputSpeed).Average();

            //        Vector3? newPosition = InterceptionCalculator.FirstOrderIntercept(transform.position,
            //                                                                          _rigidBody.velocity,
            //                                                                          projectileSpeed,
            //                                                                          enemyRigidBody.position,
            //                                                                          enemyRigidBody.velocity);

            //        if (newPosition != null)
            //        {
            //            _commandInfo.UpdateAttackTarget(newPosition.Value);
            //        }
            //    }
            //}
        }
    }
}
