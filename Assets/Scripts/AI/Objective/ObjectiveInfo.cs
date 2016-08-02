using UnityEngine;
namespace AI.Objective
{
    public class ObjectiveInfo : MonoBehaviour
    {
        private Vector3 _objectiveLocation;
        public Vector3 objectiveLocation
        {
            get { return _objectiveLocation; }
        }

        private Vector3 _moveTarget;
        public Vector3 moveTarget
        {
            get { return _moveTarget; }
        }

        private bool _targetAcquired;
        public bool targetAcquired
        {
            get { return _targetAcquired; }
        }

        private Vector3 _attackTarget;
        public Vector3 attackTarget
        {
            get { return _attackTarget; }
        }

        private GameObject _targetedEnemy;
        public GameObject targetedEnemy
        {
            get { return _targetedEnemy; }
        }

        public void Update(IObjectiveInfoModifier infoModifier)
        {
            _objectiveLocation = infoModifier.GetUpdatedObjectiveLocation(_objectiveLocation);
            _moveTarget = infoModifier.GetUpdatedMoveTarget(_moveTarget);
            _targetAcquired = infoModifier.GetUpdatedTargetAcquired(_targetAcquired);
            _attackTarget = infoModifier.GetUpdatedAttackTarget(_attackTarget);
            _targetedEnemy = infoModifier.GetUpdatedTargetedEnemy(_targetedEnemy);
        }
    }
}
