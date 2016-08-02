using System;
using UnityEngine;

namespace AI.Objective
{
    public class AttackArea : ObjectiveBase
    {
        public Vector3 attackAnchor;
        public float anchorDistance;
        
        public override void UpdatePriority()
        {
            throw new NotImplementedException();
        }

        public override void UpdateTargetInfo()
        {
            throw new NotImplementedException();
        }
    }
}
