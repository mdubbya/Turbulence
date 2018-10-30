using System;
using UnityEngine;
using AI.Process;

using AI.Task;
using AI.PathCalculation;
using Common;
using UnityEngine.Profiling;

namespace AI
{
   [RequireComponent(typeof(ShipMovementProperties))]
   [RequireComponent(typeof(TaskPrioritizer))]
   public class AIShipController : MonoBehaviour 
   {
        
        public float targetUpdateRate;

        private Rigidbody _rigidBody;

        private ShipMovementProperties _shipMovementProperties;

        TaskPrioritizer _taskPrioritizer;

        

        private IRadar _radar;

        private PIDController _angularThrustPidController;

        private float _integralTerm=0;
        private float _previousError=0;

        private Vector3 _target;
        
        private float _elapsedTime=0;

       
        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _shipMovementProperties = GetComponent<ShipMovementProperties>();
            _taskPrioritizer = GetComponent<TaskPrioritizer>();
            
            _radar = GetComponent<Radar>();
            _angularThrustPidController = new PIDController(_shipMovementProperties.angularThrustProportionalGain,
                                                            _shipMovementProperties.angularThrustIntegralGain,
                                                            _shipMovementProperties.angularThrustDerivativeGain);
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime >= targetUpdateRate)
            {
                _elapsedTime=0;
                _target = _taskPrioritizer.GetCurrentPriority();
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
            Debug.DrawLine(transform.position,_target,Color.red,0f);
        }
    }
}
