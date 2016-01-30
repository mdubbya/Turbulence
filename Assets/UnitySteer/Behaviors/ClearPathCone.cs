using UnityEngine;

namespace UnitySteer.Behaviors
{
    class ClearPathCone
    {
        public Vector3 apex;
        public Vector3 leftVector;
        public Vector3 rightVector;

        public ClearPathCone(Vector3 apexOfCone, Vector3 leftSideOfCone, Vector3 rightSideOfCone)
        {
            apex = apexOfCone;
            leftVector = leftSideOfCone;
            rightVector = rightSideOfCone;
        }
    }
}
