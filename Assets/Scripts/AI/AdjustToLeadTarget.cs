using System;
using UnityEngine;

namespace AI
{
    public class AdjustToLeadTarget : TargetModifierBase
    {
        private InterceptionCalculator interceptionCalculator;

        public void Awake()
        {
            interceptionCalculator = new InterceptionCalculator();
        }

        public override Vector3 GetNewTargetPosition(Vector3 previousTargetPosition, Vector3 previousTargetVelocity)
        {
            Vector3 currentPosition = GetComponent<WeaponController>().projectileSpawnLocation.position;
            float projectileSpeed = GetComponent<WeaponController>().projectilePrefab.GetComponent<ProjectileController>().projectileSpeed;

            Vector3? newPosition = interceptionCalculator.GetResult(currentPosition, projectileSpeed, previousTargetPosition, previousTargetVelocity);

            if (newPosition != null)
            {
                return newPosition.Value;
            }
            else
            {
                return previousTargetPosition;
            }
        }
    }
}
