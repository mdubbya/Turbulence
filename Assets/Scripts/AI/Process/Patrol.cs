using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Process
{
    public enum PatrolPattern { Reverse, Loop, Random}
    public class Patrol : CommandBase
    {
        public List<Waypoint> waypoints;
        public Waypoint currentTarget;
        public bool repeat;
        public PatrolPattern patrolPattern;
        public float closeEnoughDistance;
        public float anchorDistance;

        public override int priority
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void UpdateObjectiveInfo()
        {
            throw new NotImplementedException();
        }
    }
}
