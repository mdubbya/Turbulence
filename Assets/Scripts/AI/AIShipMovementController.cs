using System;
using UnityEngine;
using AI.Objective;
namespace AI
{
    public class AIShipMovementController : MonoBehaviour 
    {
        private ShipMovementProperties _shipMovementProperties;
        private ObjectiveInfo _targetInfo;
        private Rigidbody _rigidBody;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _shipMovementProperties = GetComponent<ShipMovementProperties>();
            _targetInfo = GetComponent<ObjectiveInfo>();
        }

        public void FixedUpdate()
        {
            ApplyMovementControl(_targetInfo);
        }


        private void ApplyMovementControl(ObjectiveInfo targetInfo)
        {
            Vector3 moveVector = (targetInfo.moveTarget - transform.position).normalized;
            Vector3 attackVector = (targetInfo.attackTarget - transform.position);

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
