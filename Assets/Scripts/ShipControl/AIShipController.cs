using System;
using UnityEngine;
using AI.Process;
using Zenject;
using AI.Task;
using AI.PathCalculation;
using Common;

namespace AI
{
   [RequireComponent(typeof(ShipMovementProperties))]
   public class AIShipController : MonoBehaviour 
   {

       private Rigidbody _rigidBody;

       private ShipMovementProperties _shipMovementProperties;

       TaskPrioritizer _taskPrioritizer;

       private RVOAgent _rvoAgent;

       private IRadar _radar;

       private PIDController _angularThrustPidController;

       private float _integralTerm=0;
       private float _previousError=0;

       [Inject]
       public void Inject(
            Rigidbody rigidBody, 
            ShipMovementProperties shipMovementProperties, 
            TaskPrioritizer taskPrioritizer,
            RVOAgent rvoAgent,
            IRadar radar,
            PIDController angularThrustPidController,
            PIDController forwardThrustPidController)
       {
           _rigidBody = rigidBody;
           _shipMovementProperties = shipMovementProperties;
           _taskPrioritizer = taskPrioritizer;
           _rvoAgent = rvoAgent;
           _radar = radar;
           _angularThrustPidController = angularThrustPidController;
           _angularThrustPidController.proportionalGain = _shipMovementProperties.angularThrustProportionalGain;
           _angularThrustPidController.integralGain = _shipMovementProperties.angularThrustIntegralGain;
           _angularThrustPidController.derivativeGain = _shipMovementProperties.angularThrustDerivativeGain;
       }


       private void FixedUpdate()
       {
            Vector3 movePostion = _taskPrioritizer.GetCurrentMovementPriority();
            Vector3 moveVector = _rvoAgent.GetAdjustedTargetPosition(movePostion,_shipMovementProperties.maxSpeed);
            moveVector = (moveVector - transform.position).normalized;
            Vector3 attackPosition =_taskPrioritizer.GetCurrentAttackPriority(); 
            Vector3 attackVector = (attackPosition - transform.position).normalized;

            Vector3 projectedVelocity = Vector3.Project(_rigidBody.velocity, moveVector);

            GameObject closest = _radar.GetClosestDetected();
            Vector3 closestVector = closest==null ? movePostion : closest.transform.position;

            Vector3 targetVector = Vector3.Distance(closestVector,transform.position) < 10 ? moveVector : attackVector;
            Vector3 targetPosition = Vector3.Distance(closestVector,transform.position) < 10 ? movePostion : attackPosition;

            float error = Vector3.Angle(targetVector,transform.forward);
            int sign = Vector3.Cross(targetVector,transform.forward).y>=0?-1:1;
            error *= sign;

            float angularThrust = _angularThrustPidController.GetUpdatedOutput(error,Time.fixedDeltaTime);
            angularThrust = Mathf.Clamp(angularThrust,-_shipMovementProperties.maxAngularThrust,_shipMovementProperties.maxAngularThrust);
            _rigidBody.AddTorque(transform.up * angularThrust);
            
            //forwardThrust = error > 10 ? 0 : forwardThrust;
            _rigidBody.AddForce(transform.forward * _shipMovementProperties.thrust);


            _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _shipMovementProperties.maxSpeed);
       }
   }
}
