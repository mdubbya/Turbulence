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
        
        public override void UpdatePriority()
        {
            throw new NotImplementedException();
        }

        public override Vector3 GetUpdatedAttackTarget(Vector3 currentAttackTarget)
        {
            throw new NotImplementedException();
        }

        public override Vector3 GetUpdatedMoveTarget(Vector3 currentMoveTarget)
        {
            throw new NotImplementedException();
        }

        public override Vector3 GetUpdatedObjectiveLocation(Vector3 currentObjectiveLocation)
        {
            throw new NotImplementedException();
        }

        public override bool GetUpdatedTargetAcquired(bool currentTargetAcquired)
        {
            throw new NotImplementedException();
        }

        public override GameObject GetUpdatedTargetedEnemy(GameObject currentTargetedEnemy)
        {
            throw new NotImplementedException();
        }
    }
}
