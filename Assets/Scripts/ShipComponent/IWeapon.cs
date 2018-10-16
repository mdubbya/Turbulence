using UnityEngine;

namespace ShipComponent
{
    public interface IWeapon
    {
        void Fire();
        Vector3 GetTargetingVector(Vector3 enemyPosition, Vector3 enemyVelocity);
    }
}