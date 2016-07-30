using System;
using UnityEngine;

namespace AI.Objective
{
    public class DefendArea : ObjectiveBase
    {
        public Vector3 position;
        public float anchorDistance;

        public override void UpdateTargetInfo(AITargetInfo targetInfo)
        {
            if (Vector3.Distance(transform.position, position) > anchorDistance)
            {
                targetInfo.moveTarget = position;
            } 
        }

        public override void UpdatePriority()
        {
            throw new NotImplementedException();
        }
    }
}