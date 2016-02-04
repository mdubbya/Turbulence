using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnitySteer.Behaviors
{
    class SteerToAvoidLineOfSight : Steering
    {
        
        public float avoidRange;
        public float avoidForce;

        protected override Vector3 CalculateForce()
        {
            foreach(Vehicle neighbor in Vehicle.Radar.Vehicles)
            {
                RaycastHit info = new RaycastHit();
                if(Physics.Raycast(neighbor.transform.position,neighbor.transform.forward, out info, avoidRange ))
                {
                    if(info.rigidbody == Vehicle.Rigidbody)
                    {
                        return neighbor.transform.right * avoidForce;
                    }
                }

                if(Utilities.PhysicsUtilities.RayCastPath(neighbor.transform.position,neighbor.transform.forward,7,avoidRange,0.2f,out info))
                {
                    if(info.rigidbody==Vehicle.Rigidbody)
                    {
                        return neighbor.transform.right * avoidForce;
                    }
                }
            }
            return new Vector3();
        }

        

    }
}
