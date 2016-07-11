using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class SelectClosestTarget : TargetSelectorBase
    {
        public float detectionRadius;
        public List<SingleUnityLayer> layers;

        public override Vector3 GetNewTargetPosition(Vector3 previousTargetPosition)
        {
            Vector3 currentPosition = transform.position;

            List<Collider> nearbyColliders = new List<Collider>();
            foreach (SingleUnityLayer layer in layers)
            {
                nearbyColliders.AddRange(Physics.OverlapSphere(currentPosition, detectionRadius, layer.Mask));
            }
            //find the closest collider in the deteciton radius
            Collider closest = new Collider();
            bool colliderFound = false;
            float distance = float.MaxValue;
            if (nearbyColliders.Count > 1)
            {
                foreach (Collider col in nearbyColliders)
                {
                    if (col != transform.GetComponentInChildren<Collider>())
                    {
                        float newDistance = (col.transform.position - currentPosition).magnitude;
                        if (newDistance < distance)
                        {
                            closest = col;
                            colliderFound = true;
                        }
                    }
                }
            }
            else
            {
                closest = nearbyColliders[0];
                colliderFound = true;
            }

            if (colliderFound)
            {
                return closest.transform.position;
            }
            else
            {
                return previousTargetPosition;
            }
        }

        public void Update()
        {
            

        }
    }

}