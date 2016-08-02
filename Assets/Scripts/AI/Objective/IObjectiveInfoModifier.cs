using UnityEngine;

namespace AI.Objective
{
    public interface IObjectiveInfoModifier
    {
        Vector3 GetUpdatedObjectiveLocation(Vector3 currentObjectiveLocation);
        Vector3 GetUpdatedMoveTarget(Vector3 currentMoveTarget);
        bool GetUpdatedTargetAcquired(bool currentTargetAcquired);
        Vector3 GetUpdatedAttackTarget(Vector3 currentAttackTarget);
        GameObject GetUpdatedTargetedEnemy(GameObject currentTargetedEnemy);
    }
}