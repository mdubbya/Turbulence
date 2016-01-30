using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;
using System;
using System.Collections.Generic;

namespace UnitySteer.Behaviors
{ 
    public class SteerForClearPathAvoid : Steering
    {
        protected override Vector3 CalculateForce()
        {
            if (Vehicle.Radar.Detected != null)
            {
                List<ClearPathCone> cones = new List<ClearPathCone>();
                //find a cone for each neighbor that describes all possible velocities that could collide with that neighbor
                foreach (Collider neighbor in Vehicle.Radar.Detected)
                {
                    if (neighbor != null)
                    {
                        Vector3 beginVector = (neighbor.transform.position - Vehicle.Position).normalized;

                        if (Physics.Raycast(Vehicle.Position, beginVector, Vehicle.Velocity.magnitude))
                        {
                            float neighborBounds = neighbor.bounds.extents.magnitude + Vehicle.GetComponentInChildren<Collider>().bounds.extents.magnitude;
                            Vector3 leftVector = (((neighbor.transform.position+(neighbor.transform.right * neighborBounds)) - Vehicle.Position).normalized) * Vehicle.Velocity.magnitude;
                            Vector3 rightVector = (((neighbor.transform.position+ -neighbor.transform.right * neighborBounds)) - Vehicle.Position).normalized * Vehicle.Velocity.magnitude;
                            Vector3 coneApex = Vehicle.Position + ((neighbor.attachedRigidbody.velocity + Vehicle.Velocity) / 2);

                            cones.Add(new ClearPathCone(coneApex, leftVector, rightVector));

                            //Debug.DrawRay(coneApex, leftVector, Color.red, 0.2f);
                            //Debug.DrawRay(coneApex, rightVector, Color.red, 0.2f);
                        }
                    }
                }
                if (cones.Count > 0)
                {
                    Vector3 shortestVector = cones[0].leftVector;
                    foreach (ClearPathCone cone in cones)
                    {
                        Vector3 projectedVector = Vector3.Project(Vehicle.Velocity, cone.leftVector);
                        //Debug.DrawRay(Vehicle.Position, projectedVector, Color.yellow, 0.2f);
                        if (projectedVector.magnitude < shortestVector.magnitude)
                        {
                            shortestVector = projectedVector;
                        }
                        projectedVector = Vector3.Project(Vehicle.Velocity, cone.rightVector);
                        //Debug.DrawRay(Vehicle.Position, projectedVector, Color.yellow, 0.2f);
                        if (projectedVector.magnitude < shortestVector.magnitude)
                        {
                            shortestVector = projectedVector;
                        }
                    }
                    return shortestVector;
                }

            }
            return new Vector3();
        }
    }
}