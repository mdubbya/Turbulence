using System;
using UnityEngine;

namespace AI
{
    public class AttackArea : Objective
    {
        public Vector3 attackAnchor;
        public float anchorDistance;

        public override AITargetInfo GetTargetInfo(AITargetInfo targetInfo)
        {
            return new AITargetInfo(attackAnchor, targetInfo.targetAcquired, targetInfo.attackTarget, targetInfo.enemyRigidBody);
        }
    }
}
