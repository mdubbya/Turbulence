using System;
using System.Collections.Generic;
using UnityEngine;

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
        
        public override int GetPriority()
        {
            throw new NotImplementedException();
        }

        public override void UpdateObjectiveInfo()
        {
            throw new NotImplementedException();
        }

        public override void SetBasePriority(int basePriority)
        {
            throw new NotImplementedException();
        }
    }
}
