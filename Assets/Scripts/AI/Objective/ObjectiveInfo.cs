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
        
        public void UpdateObjectiveLocation(Vector3 newObjectiveLocation)
        {
            _objectiveLocation = newObjectiveLocation;
        }

        public void UpdateMoveTarget(Vector3 newMoveTarget)
        {
            _moveTarget = newMoveTarget;
        }

        public void UpdateTargetAcquiredFlag(bool newTargetAcquiredFlag)
        {
            _targetAcquired = newTargetAcquiredFlag;
        }

        public void UpdateAttackTarget(Vector3 newAttackTarget)
        {
            _attackTarget = newAttackTarget;
        }

        public void UpdateTargetedEnemy(GameObject newTargetedEnemy)
        {
            _targetedEnemy = newTargetedEnemy;
        }
    }
}
