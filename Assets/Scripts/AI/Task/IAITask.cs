using UnityEngine;

namespace AI.Task
{
    public interface IAITask
    {
         float GetPriority();
         Vector3 GetTarget();
    }
}