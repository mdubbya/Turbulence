﻿using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public class SelectClosestTarget : MonoBehaviour, ITargetModifier
    {
        public float detectionRadius;
        public List<SingleUnityLayer> layers;
        
        [SerializeField]
        int _priority;
        public int priority
        {
            get{ return _priority; }

            set { _priority = value; }
        }
        

        public AITargetInfo GetNewTargetInfo(AITargetInfo targetInfo)
        {
            Vector3 currentPosition = transform.position;
            
            int layerMask = 0;
            foreach(SingleUnityLayer layer in layers)
            {
                layerMask = layerMask | layer.Mask;
            }
            Collider[] nearbyColliders = Physics.OverlapSphere(currentPosition, detectionRadius);
            
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
                return new AITargetInfo(closest.transform.position,targetInfo.isTargetEnemy,targetInfo.rigidBody);
                
            }
            else
            {
                return targetInfo;
            }
        }

        public void Update()
        {
            

        }
    }

}