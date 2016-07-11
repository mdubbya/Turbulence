using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public abstract class TargetModifierBase : MonoBehaviour
    {
        public abstract Vector3 GetNewTargetPosition(Vector3 currentTargetPosition, Vector3 currentTargetVelocity);
    }
}
