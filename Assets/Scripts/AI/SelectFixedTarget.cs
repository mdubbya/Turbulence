using System;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public class SelectFixedTarget : MonoBehaviour, ITargetModifier
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
            return new AITargetInfo(target.position, targetInfo.isTargetEnemy, targetInfo.rigidBody);
        }
    }
}
