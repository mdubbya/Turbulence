using UnityEngine;

namespace Utilities
{ 
    public static class PhysicsUtilities
    {
        public static bool RayCastPath(Vector3 start, Vector3 direction, float width, float length, float rayCastIncrement, out RaycastHit rayCastHitInfo )
        {
            rayCastHitInfo = new RaycastHit();
            Vector3 left = (Quaternion.Euler(0, 90, 0) * direction).normalized;
            Vector3 right = (Quaternion.Euler(0, -90, 0) * direction).normalized;

            float currentDistanceFromStart = 0;
            Vector3 currentLeftVector = start + (left * (currentDistanceFromStart));
            Vector3 currentRightVector = start + (right * (currentDistanceFromStart));

            bool hitDetected = false;
            //we will shoot vectors in [direction] until we detect a hit, or we've run out of vectors to shoot.
            while((currentDistanceFromStart < width/2) && !hitDetected)
            {
                //Debug.DrawRay(currentLeftVector, direction * length, Color.blue, 0.1f);
                //Debug.DrawRay(currentRightVector, direction * length, Color.blue, 0.1f);
                if (Physics.Raycast(currentLeftVector, direction, out rayCastHitInfo, length))
                {
                    hitDetected = true;
                    break;
                }
                else if (Physics.Raycast(currentRightVector, direction, out rayCastHitInfo, length))
                {
                    hitDetected = true;
                    break;
                }

                currentDistanceFromStart += rayCastIncrement;
                currentLeftVector = start + (left * currentDistanceFromStart);
                currentRightVector = start +(right * currentDistanceFromStart);
            }

            return hitDetected;
        }
    }
}