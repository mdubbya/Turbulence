using System;
using UnityEngine;
using AI.Process;
using Zenject;
using AI.Task;
using AI.PathCalculation;

namespace AI
{
   [RequireComponent(typeof(ShipMovementProperties))]
   public class AIShipController : MonoBehaviour 
   {

       private Rigidbody _rigidBody;

       private ShipMovementProperties _shipMovementProperties;

       TaskPrioritizer _taskPrioritizer;

       private RVOAgent _rvoAgent;

       [Inject]
       public void Inject(
            Rigidbody rigidBody, 
            ShipMovementProperties shipMovementProperties, 
            TaskPrioritizer taskPrioritizer,
            RVOAgent rvoAgent)
       {
           _rigidBody = rigidBody;
           _shipMovementProperties = shipMovementProperties;
           _taskPrioritizer = taskPrioritizer;
           _rvoAgent = rvoAgent;
       }


       private void FixedUpdate()
       {
            Vector3 moveVector = _rvoAgent.GetAdjustedTargetPosition(_taskPrioritizer.GetCurrentMovementPriority(),_shipMovementProperties.maxSpeed);
            moveVector = (moveVector - transform.position).normalized;

            Vector3 attackVector = (_taskPrioritizer.GetCurrentAttackPriority()- transform.position);

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
