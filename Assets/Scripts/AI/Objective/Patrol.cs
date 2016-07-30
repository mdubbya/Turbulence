using System;
using System.Collections.Generic;

namespace AI.Objective
{
    public enum PatrolPattern { Reverse, Loop, Random}
    public class Patrol : ObjectiveBase
    {
        public List<Waypoint> waypoints;
        public Waypoint currentTarget;
        public bool repeat;
        public PatrolPattern patrolPattern;
        public float closeEnoughDistance;
        public float anchorDistance;

        public override void UpdateTargetInfo(AITargetInfo targetInfo)
        {
            throw new NotImplementedException();
        }

        public override void UpdatePriority()
        {
            throw new NotImplementedException();
        }
    }
}
