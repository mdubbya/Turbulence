using UnityEngine;

namespace AI
{
    public class AITargetInfo
    {
        public Vector3 moveTarget;
        public bool targetAcquired;
        public Vector3 attackTarget;
        public Rigidbody enemyRigidBody;

        public AITargetInfo(Vector3 _moveTarget, bool _targetAcquired, Vector3 _attackTarget, Rigidbody _enemyRigidBody)
        {
            moveTarget = _moveTarget;
            targetAcquired = _targetAcquired;
            attackTarget = _attackTarget;
            enemyRigidBody = _enemyRigidBody;
        }
    }
}
