using System;
using UnityEngine;
using AI.Process;
using Zenject;
using AI.Task;
using AI.PathCalculation;
using Common;
using UnityEngine.Profiling;

namespace AI
{
   [RequireComponent(typeof(ShipMovementProperties))]
   public class AIShipController : MonoBehaviour 
   {
        public float targetDeadZone;
        public float targetUpdateRate;

        private Rigidbody _rigidBody;

        private ShipMovementProperties _shipMovementProperties;

        TaskPrioritizer _taskPrioritizer;

        private RVOAgent _rvoAgent;

        private IRadar _radar;

        private PIDController _angularThrustPidController;

        private float _integralTerm=0;
        private float _previousError=0;

        private Vector3 _target;
        
        private float _elapsedTime=0;

        [Inject]
        public void Inject(
        Rigidbody rigidBody, 
        ShipMovementProperties shipMovementProperties, 
        TaskPrioritizer taskPrioritizer,
        RVOAgent rvoAgent,
        IRadar radar,
        PIDController angularThrustPidController)
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

        private void Update()
        {
            _angularThrustPidController.proportionalGain = _shipMovementProperties.angularThrustProportionalGain;
            _angularThrustPidController.integralGain = _shipMovementProperties.angularThrustIntegralGain;
            _angularThrustPidController.derivativeGain = _shipMovementProperties.angularThrustDerivativeGain;

            _elapsedTime += Time.deltaTime;
            if(_elapsedTime >= targetUpdateRate)
            {
                _elapsedTime=0;
                Vector3 movePostion = _taskPrioritizer.GetCurrentMovementPriority();
                movePostion = _rvoAgent.GetAdjustedTargetPosition(movePostion,_shipMovementProperties.maxSpeed);
                Vector3 attackPosition =_taskPrioritizer.GetCurrentAttackPriority(); 
                GameObject closest = _radar.GetClosestDetected();
                Vector3 closestVector = closest==null ? movePostion : closest.transform.position;
                _target = Vector3.Distance(closestVector,transform.position) < targetDeadZone ? movePostion : attackPosition;
            }
        }

        private void FixedUpdate()
        {
            float error = Vector3.Angle(_target - transform.position,transform.forward);
            int sign = Vector3.Cross(_target - transform.position,transform.forward).y>=0?-1:1;
            error *= sign;
            float angularThrust = _angularThrustPidController.GetUpdatedOutput(error,Time.deltaTime);
            angularThrust = Mathf.Clamp(angularThrust,-_shipMovementProperties.maxAngularThrust,_shipMovementProperties.maxAngularThrust);
            _rigidBody.AddTorque(transform.up * angularThrust);
            
            //forwardThrust = error > 10 ? 0 : forwardThrust;
            _rigidBody.AddForce(transform.forward * _shipMovementProperties.thrust);

            _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _shipMovementProperties.maxSpeed);
        }

        void OnDrawGizmosSelected()
        {
            DebugExtension.DrawPoint(_target,Color.red,3);
        }
    }
}
