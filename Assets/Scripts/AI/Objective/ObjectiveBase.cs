using UnityEngine;
namespace AI.Objective
{
    public abstract class ObjectiveBase : MonoBehaviour
    {
        public bool objectiveValid=true;

        public abstract void SetBasePriority(int basePriority);

        public abstract int GetPriority();

        public abstract void UpdateObjectiveInfo();
    }
}
