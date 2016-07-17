using UnityEngine;

namespace AI
{
    public class AITargetInfo
    {
        public Vector3 position;
        public bool isTargetEnemy;
        public Rigidbody rigidBody;

        public AITargetInfo(Vector3 _position, bool _isTargetEnemy, Rigidbody _rigidBody)
        {
            position = _position;
            isTargetEnemy = _isTargetEnemy;
            rigidBody = _rigidBody;
        }
    }
}
