using System;
using UnityEngine;
using UnitySteer.Behaviors;

namespace Assets.UnitySteer.Behaviors
{
    class SteerForObstacleAvoid : Steering
    {
        public float maxVelocity;
        public float maxSeeAheadDistance;
        public float maxAvoidForce;

        private Vector3 collisionAvoidance()
        {
            float dynamicLength = Vehicle.Velocity.magnitude / maxVelocity;
            Vector3 ahead = Vehicle.Position + Vehicle.Velocity.normalized * dynamicLength;
            ahead = Vehicle.Position + Vehicle.Velocity.normalized * maxSeeAheadDistance;
            Vector3 ahead2 = Vehicle.Position + Vehicle.Velocity.normalized * maxSeeAheadDistance * 0.5f;

            GameObject mostThreatening = findMostThreateningObstacle();
            Vector3 avoidance = new Vector3(0, 0, 0);

            if (mostThreatening != null)
            {
                avoidance = new Vector3(ahead.x - mostThreatening.transform.position.x,
                                        ahead.y - mostThreatening.transform.position.y,
                                        ahead.z);

                avoidance.Normalize();
                avoidance = avoidance * maxAvoidForce;
            }
            else
            {
                avoidance = avoidance * 0f; // nullify the avoidance force
            }

            return avoidance;
        }



        private GameObject findMostThreateningObstacle()
        {
            RaycastHit info = new RaycastHit();
            if (Physics.Raycast(Vehicle.Position, Vehicle.transform.forward, out info, maxSeeAheadDistance))
            {
                return info.rigidbody.gameObject;
            }
            else
            {
                return null;
            }
        }

        protected override Vector3 CalculateForce()
        {
            return collisionAvoidance();
        }
    }
}
