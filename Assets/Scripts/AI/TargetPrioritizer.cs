using UnityEngine;

namespace AI
{
    public abstract class TargetPrioritizer : MonoBehaviour
    {
        public abstract void UpdateTargetInfo(AITargetInfo targetInfo);
    }
}
