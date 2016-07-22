using System;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public class AIShipMovementController : MonoBehaviour , IShipMovementController
    {
        public float turnTime;
        public float maxSpeed;
        public float thrust;

        private Rigidbody rigidBody;

        public void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }


        public void ApplyMovementControl(AITargetInfo targetInfo)
        {
            Vector3 moveVector = (targetInfo.moveTarget - transform.position).normalized;
            Vector3 attackVector = (targetInfo.attackTarget - transform.position);

            Vector3 projectedVelocity = Vector3.Project(rigidBody.velocity, moveVector);
            
            if (projectedVelocity.magnitude < maxSpeed-1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                                                    Quaternion.LookRotation(moveVector), Time.deltaTime * 1 / turnTime);

                rigidBody.AddForce(transform.forward * thrust);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                                                     Quaternion.LookRotation(attackVector), Time.deltaTime * 1 / turnTime); 
            }
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }
    }
}
