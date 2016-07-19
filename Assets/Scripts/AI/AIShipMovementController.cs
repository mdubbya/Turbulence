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
            var targetVector = targetInfo.position - transform.position;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                Quaternion.LookRotation(targetVector), Time.deltaTime * 1 / turnTime);

            rigidBody.AddForce(transform.forward * thrust);
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }
    }
}
