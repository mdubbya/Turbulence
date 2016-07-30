﻿using UnityEngine;
namespace AI.Objective
{
    public abstract class ObjectiveBase : MonoBehaviour
    {
        public bool objectiveValid=true;
        public int priority=0;

        public abstract void UpdateTargetInfo(AITargetInfo targetInfo);

        public abstract void UpdatePriority();
    }
}
