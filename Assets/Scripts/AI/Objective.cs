using UnityEngine;
namespace AI
{
    public abstract class Objective : MonoBehaviour
    {
        public bool objectiveComplete;
        public int priority;

        public abstract void UpdateTargetInfo(AITargetInfo targetInfo);
    }
}
