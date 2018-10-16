using UnityEngine;
using AI.PathCalculation;
using Zenject;

namespace ShipComponent
{
    public class ProjectileWeapon : MonoBehaviour, IWeapon
    {
        public Transform mountPoint;
        public Vector3 projectileSpawnPosition;

        public float launchSpeed;
        public float shotsPerSecond;
        private ProjectileType projectileType;

        private Factory<Projectile> _projectileFactory;

        private Rigidbody _rigidBody;
        
        private float _timeSinceLastShot;

        [Inject]
        public void Inject(Rigidbody rigidbody, Factory<Projectile>  projectileFactory)
        {
            _rigidBody = rigidbody;
            _projectileFactory = projectileFactory;
        }

        public void Update()
        {
            _timeSinceLastShot += Time.deltaTime;
        }
         
        public void Fire()
        {
            if(_timeSinceLastShot >= 1/shotsPerSecond)
            {
                Projectile projectile = _projectileFactory.Create();
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
  