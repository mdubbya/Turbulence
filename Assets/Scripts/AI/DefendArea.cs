using UnityEngine;

namespace AI
{
    public class DefendArea : Objective
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
    }
}