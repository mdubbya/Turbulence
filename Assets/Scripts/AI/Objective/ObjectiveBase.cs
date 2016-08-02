using UnityEngine;
namespace AI.Objective
{
    public abstract class ObjectiveBase : MonoBehaviour, IObjectiveInfoModifier
    {
        public bool objectiveValid=true;
        public int priority=0;

        public abstract Vector3 GetUpdatedAttackTarget(Vector3 currentAttackTarget);
        public abstract Vector3 GetUpdatedMoveTarget(Vector3 currentMoveTarget);
        public abstract Vector3 GetUpdatedObjectiveLocation(Vector3 currentObjectiveLocation);
        public abstract bool GetUpdatedTargetAcquired(bool currentTargetAcquired);
        public abstract GameObject GetUpdatedTargetedEnemy(GameObject currentTargetedEnemy);

        public abstract void UpdatePriority();
    }
}
