using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Objective;

namespace AI.PostProcess
{
    public class LeadTarget : MonoBehaviour, IPostProcessor
    {

        private Rigidbody _rigidBody;
        private List<IWeaponController> _weapons;
        private ObjectiveInfo _objectiveInfo;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _weapons = GetComponentsInChildren<IWeaponController>().ToList();
            _objectiveInfo = GetComponent<ObjectiveInfo>();
        }


        public void UpdateObjectiveInfo()
        {
            if (_objectiveInfo != null)
            {
                Rigidbody enemyRigidBody = _objectiveInfo.targetedEnemy != null ? _objectiveInfo.targetedEnemy.GetComponent<Rigidbody>() : null;
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
                        _objectiveInfo.UpdateAttackTarget(newPosition.Value);
                    }
                }
            }
        }
    }
}
