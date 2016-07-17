using System;
using UnityEngine;

namespace AI
{
    public class AdjustToLeadTarget : MonoBehaviour
    {
        private InterceptionCalculator interceptionCalculator;

        public void Start()
        {
            interceptionCalculator = new InterceptionCalculator();
        }
        
        public Vector3 GetNewTargetPosition(Rigidbody objectToIntercept)
        {
            Vector3 currentPosition = GetComponent<ProjectileWeaponController>().spawnLocation.position;
            float projectileSpeed = GetComponent<ProjectileWeaponController>().projectilePrefab.GetComponent<ProjectileController>().projectileSpeed;

            Vector3? newPosition = interceptionCalculator.GetResult(currentPosition, projectileSpeed, objectToIntercept.position, objectToIntercept.velocity);

            if (newPosition != null)
            {
                return newPosition.Value;
            }
            else
            {
                return objectToIntercept.position;
            }
        }
    }
}
