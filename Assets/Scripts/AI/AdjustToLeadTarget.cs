using System;
using UnityEngine;

namespace AI
{
    public class AdjustToLeadTarget : MonoBehaviour , ITargetModifier
    {
        private InterceptionCalculator interceptionCalculator;

        [SerializeField]
        private int _priority;
        public int priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public void Start()
        {
            interceptionCalculator = new InterceptionCalculator();
        }
        
        public Vector3 GetNewTargetPosition(Rigidbody objectToIntercept)
        {
            Vector3 currentPosition = GetComponent<AIProjectileWeaponController>().spawnLocation.position;
            float projectileSpeed = GetComponent<AIProjectileWeaponController>().projectilePrefab.GetComponent<ProjectileController>().projectileSpeed;

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

        public AITargetInfo GetNewTargetInfo(AITargetInfo targetInfo)
        {
            throw new NotImplementedException();
        }
    }
}
