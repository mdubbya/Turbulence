using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public class ShipMovementController : MonoBehaviour
    {
        public float turnTime;
        public float maxSpeed;
        public float thrust;

        private Rigidbody rigidBody;
        private TargetSelectionController targetSelectionController;

        public void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            targetSelectionController = GetComponent<TargetSelectionController>();
        }

        public void FixedUpdate()
        {
            var targetVector = targetSelectionController.targetPosition - transform.position;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                Quaternion.LookRotation(targetVector), Time.deltaTime * 1 / turnTime);

            rigidBody.AddForce(transform.forward * thrust);
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }
    }
}
