using System;
using UnityEngine;
using AI.Objective;
namespace AI
{
    public class AIShipMovementController : MonoBehaviour 
    {
        private ShipMovementProperties _shipMovementProperties;
        private ObjectiveInfo _objectiveInfo;
        private Rigidbody _rigidBody;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _shipMovementProperties = GetComponent<ShipMovementProperties>();
            _objectiveInfo = GetComponent<ObjectiveInfo>();
        }


        private void FixedUpdate()
        {
            if (_objectiveInfo != null)
            {
                Vector3 moveVector = (_objectiveInfo.moveTarget - transform.position).normalized;
                Vector3 attackVector = (_objectiveInfo.attackTarget - transform.position);

                Vector3 projectedVelocity = Vector3.Project(_rigidBody.velocity, moveVector);

                if (projectedVelocity.magnitude < _shipMovementProperties.maxSpeed - 1f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                                                        Quaternion.LookRotation(moveVector), Time.deltaTime * 1 / _shipMovementProperties.turnTime);

                    _rigidBody.AddForce(transform.forward * _shipMovementProperties.thrust);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                                                         Quaternion.LookRotation(attackVector), Time.deltaTime * 1 / _shipMovementProperties.turnTime);
                }
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _shipMovementProperties.maxSpeed);
            }
        }
    }
}
