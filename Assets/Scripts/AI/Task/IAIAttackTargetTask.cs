using UnityEngine;

namespace AI.Task
{
    public interface IAIAttackTargetTask : IAITask
    {
        Vector3 GetAttackTarget();
    }
}
