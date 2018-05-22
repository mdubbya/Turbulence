using UnityEngine;

namespace AI.Task
{
    public interface IAIMoveTargetTask : IAITask
    {
        Vector3 GetNavigationTarget();
    }
}
