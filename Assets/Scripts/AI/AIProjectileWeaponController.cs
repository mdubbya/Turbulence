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
            if (targetInfo.targetAcquired && targetInfo.enemyRigidBody != null)
            {
                Vector3 checkVector = transform.position + ((targetInfo.attackTarget - transform.position).magnitude * transform.forward);
                DebugExtension.DebugPoint(checkVector, Color.white, 1, Time.fixedDeltaTime);
                Debug.DrawLine(transform.position, checkVector, Color.green, Time.fixedDeltaTime);
                //Debug.DrawLine(checkVector,targetInfo.enemyRigidBody.position,Color)
                if (Vector3.Distance(checkVector, targetInfo.attackTarget) < targetZoneWidth)
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