using System;
using UnityEngine;

namespace AI
{
    public class AttackArea : Objective
    {
        public Vector3 attackAnchor;
        public float anchorDistance;



        public override void UpdateTargetInfo(AITargetInfo targetInfo)
        {
            targetInfo.moveTarget = attackAnchor;
        }
    }
}
