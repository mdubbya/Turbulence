using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(TargetSelectionController))]
    public abstract class TargetSelectorBase : MonoBehaviour
    {
        public abstract Vector3 GetNewTargetPosition(Vector3 previousTargetPosition);
    }
}
