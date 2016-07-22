using System;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public class moveToFixedTarget : MonoBehaviour, ITargetModifier
    {
        public Transform target;

        [SerializeField]
        private int _priority;
        public int priority
        {
            get { return _priority; }

            set { _priority = value; }
        }

        public AITargetInfo GetNewTargetInfo(AITargetInfo targetInfo)
        {
            return new AITargetInfo(target.position, targetInfo.targetAcquired,targetInfo.attackTarget, targetInfo.enemyRigidBody);
        }
    }
}
