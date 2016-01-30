using System;
using UnityEngine;

namespace UnitySteer.Behaviors
{
    class SteerToAlignTarget : Steering
    {
        public GameObject target;

        protected override Vector3 CalculateForce()
        {
            Vector3 returnVal = new Vector3();
            RaycastHit info = new RaycastHit();
            if (target != null)
            {
                if (Physics.Linecast(transform.position, target.transform.position, out info))
                {
                    if (info.transform == target.transform)
                    {
                        Vector3 vectorToTarget = (target.transform.position - transform.position).normalized;
                        if (Vector3.Angle(transform.forward, vectorToTarget) > 5)
                        {
                            int sign = Vector3.Cross(transform.forward, (vectorToTarget)).y < 0 ? -1 : 1;
                            if (sign > 0)
                            {
                                returnVal = transform.right / 10;
                            }
                            else if (sign < 0)
                            {
                                returnVal = -transform.right / 10;
                            }
                        }
                    }
                }
            }
            
            return returnVal;
        }
    }
}
