using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class SelectClosestTarget : TargetSelectorBase
    {
        public float detectionRadius;
        public List<SingleUnityLayer> layers;

        int _priority;
        public override int priority
        {
            get{ return _priority; }

            set { _priority = value; }
        }

        public override Vector3 GetNewTargetPosition(Vector3 previousTargetPosition)
        {
            Vector3 currentPosition = transform.position;
            
            int layerMask = 0;
            foreach(SingleUnityLayer layer in layers)
            {
                layerMask = layerMask | layer.Mask;
            }
            Collider[] nearbyColliders = Physics.OverlapSphere(currentPosition, detectionRadius, layerMask);
            
            //find the closest collider in the deteciton radius
            Collider closest = new Collider();
            bool colliderFound = false;
            float distance = float.MaxValue;
            if (nearbyColliders.Length > 1)
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
                DebugExtension.DebugPoint(closest.transform.position, Color.green, 2, Time.fixedDeltaTime * 4);
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