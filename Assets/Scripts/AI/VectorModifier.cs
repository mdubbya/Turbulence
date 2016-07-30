using UnityEngine;

namespace AI
{
    public abstract class VectorModifier : MonoBehaviour
    {
        public abstract void ModifyAttackVector(AITargetInfo targetInfo);
    }
}
