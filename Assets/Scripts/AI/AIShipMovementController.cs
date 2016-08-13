using System;
using UnityEngine;
using AI.Process;
namespace AI
{
    [RequireComponent(typeof(ShipMovementProperties))]
    public class AIShipMovementController : MonoBehaviour 
    {
        private CommandInfo _commandInfo;
        private Rigidbody _rigidBody;
        private ShipMovementProperties _shipMovementProperties;

        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _commandInfo = GetComponent<CommandInfo>();
            _shipMovementProperties = GetComponent<ShipMovementProperties>();
        }


        private void FixedUpdate()
        {
            if (_commandInfo != null)
            {
                Vector3 moveVector = (_commandInfo.moveTarget - transform.position).normalized;
                Vector3 attackVector = (_commandInfo.attackTarget - transform.position);

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
