using UnityEngine;
using System.Collections.Generic;
namespace AI
{
    public class AITargetInfo : MonoBehaviour
    {
        public Vector3 objectiveLocation;
        public Vector3 moveTarget;
        public bool targetAcquired;
        public Vector3 attackTarget;
        public Rigidbody enemyRigidBody;
    }
}
