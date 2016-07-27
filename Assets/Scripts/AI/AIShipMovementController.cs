using System;
using UnityEngine;

namespace AI
{
    public class AIShipMovementController : MonoBehaviour , IShipMovementController
    {
        private ShipMovementProperties shipMovementProperties;

        private Rigidbody rigidBody;

        public void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            shipMovementProperties = GetComponent<ShipMovementProperties>();
        }


        public void ApplyMovementControl(AITargetInfo targetInfo)
        {
            Vector3 moveVector = (targetInfo.moveTarget - transform.position).normalized;
            Vector3 attackVector = (targetInfo.attackTarget - transform.position);

            Vector3 projectedVelocity = Vector3.Project(rigidBody.velocity, moveVector);
            
            if (projectedVelocity.magnitude < shipMovementProperties.maxSpeed - 1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                                                    Quaternion.LookRotation(moveVector), Time.deltaTime * 1 / shipMovementProperties.turnTime);

                rigidBody.AddForce(transform.forward * shipMovementProperties.thrust);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                                                     Quaternion.LookRotation(attackVector), Time.deltaTime * 1 / shipMovementProperties.turnTime); 
            }
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, shipMovementProperties.maxSpeed);
        }
    }
}
