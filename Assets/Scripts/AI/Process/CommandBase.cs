using UnityEngine;
namespace AI.Process
{
    public abstract class CommandBase : MonoBehaviour
    {
        public bool objectiveValid=true;
        abstract public int priority { get; }
        public int basePriority;
        public int weightDistance;

        public abstract void UpdateObjectiveInfo();
    }
}
