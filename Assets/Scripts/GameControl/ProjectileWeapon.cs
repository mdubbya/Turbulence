using UnityEngine;
using AI.PathCalculation;


namespace Component
{
    public class ProjectileWeapon : MonoBehaviour
    {
        public Transform mountPoint;
        public Vector3 projectileSpawnPosition;

        public float launchSpeed;
        public float shotsPerSecond;
        private ProjectileType projectileType;

        private Rigidbody _rigidBody;
        
        private float _timeSinceLastShot;

        private GameObject _projectilePrefab;
       
        public void Update()
        {
            _timeSinceLastShot += Time.deltaTime;
        }
         
        public void Fire()
        {
            if(_timeSinceLastShot >= 1/shotsPerSecond)
            {
                Projectile projectile = Instantiate(_projectilePrefab).GetComponent<Projectile>();
                projectile.transform.position = projectileSpawnPosition;
                Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();

                if (Vector3.Dot (_rigidBody.velocity, transform.forward)>0)
                {
                    projectileRigidBody.velocity = _rigidBody.velocity;
                }
                projectileRigidBody.velocity += transform.forward * launchSpeed;
            }
        }

        public Vector3 GetTargetingVector(Vector3 enemyPosition, Vector3 enemyVelocity)
        {
            return PathCalculationUtilities.GetFirstOrderIntercept(transform.position,_rigidBody.velocity,launchSpeed,enemyPosition, enemyVelocity);
        }
    }
}
  