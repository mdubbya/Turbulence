using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Process
{
    public enum PatrolPattern { Reverse, Loop, Random}
    public class Patrol
    {
        public List<Waypoint> waypoints;
        public Waypoint currentTarget;
        public bool repeat;
        public PatrolPattern patrolPattern;
        public float closeEnoughDistance;
        public float anchorDistance;

        public int priority
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void UpdateObjectiveInfo()
        {
            throw new NotImplementedException();
        }
    }
}
