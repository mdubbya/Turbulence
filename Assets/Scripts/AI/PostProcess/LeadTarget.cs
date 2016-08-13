using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Process;

namespace AI.PostProcess
{
    public class LeadTarget : MonoBehaviour, IPostProcessor
    {

        private Rigidbody _rigidBody;
        private List<IWeaponController> _weapons;
        private CommandInfo _commandInfo;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _weapons = GetComponentsInChildren<IWeaponController>().ToList();
            _commandInfo = GetComponent<CommandInfo>();
        }


        public void UpdateObjectiveInfo()
        {
            if (_commandInfo != null)
            {
                Rigidbody enemyRigidBody = _commandInfo.targetedEnemy != null ? _commandInfo.targetedEnemy.GetComponent<Rigidbody>() : null;
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
                        _commandInfo.UpdateAttackTarget(newPosition.Value);
                    }
                }
            }
        }
    }
}
