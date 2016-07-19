using UnityEngine;
using AI;

namespace AI
{
    public class AIProjectileWeaponController : MonoBehaviour, IAIWeaponController
    {
        public GameObject projectilePrefab;
        public float maxProjectilesPerSecond;
        public float targetZoneWidth;
        public float targetingDistance;
        private float lastProjectileSpawned = 0;

        [SerializeField]
        private Transform _spawnLocation;
        public Transform spawnLocation
        {
            get { return _spawnLocation; }
            set { _spawnLocation = value; }
        }


        public void AttackIfTargetValid(AITargetInfo targetInfo)
        {
            if (targetInfo.isTargetEnemy && targetInfo.rigidBody != null)
            {
                Vector3 checkVector = transform.position + ((targetInfo.position - transform.position).magnitude * transform.forward);
                if (Vector3.Distance(checkVector, targetInfo.position) < targetZoneWidth)
                {
                    float projectileSpawnTimeSpacing = 1 / maxProjectilesPerSecond;
                    if (Time.time - lastProjectileSpawned > projectileSpawnTimeSpacing)
                    {
                        lastProjectileSpawned = Time.time;
                        CreateAndLaunchProjectile();
                    }
                }
            }
        }


        private void CreateAndLaunchProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnLocation.position, spawnLocation.rotation) as GameObject;
            projectile.GetComponent<ProjectileController>().LaunchProjectile(GetComponentInParent<Rigidbody>().velocity);
        }

    }
}