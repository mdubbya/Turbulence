using UnityEngine;

public class PlayerProjectileWeaponController : MonoBehaviour, IWeaponController
{
    public GameObject projectilePrefab;
    public float maxProjectilesPerSecond;
    private float lastProjectileSpawned = 0;

    private Transform _spawnLocation;
    public Transform spawnLocation
    {
        get { return _spawnLocation; }
        set { _spawnLocation = value; }
    }


    public void Fire()
    {
        float projectileSpawnTimeSpacing = 1 / maxProjectilesPerSecond;
        if (Time.time - lastProjectileSpawned > projectileSpawnTimeSpacing)
        {
            lastProjectileSpawned = Time.time;
            CreateAndLaunchProjectile();
        }   
    }


    private void CreateAndLaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnLocation.position, spawnLocation.rotation) as GameObject;
        projectile.GetComponent<ProjectileController>().LaunchProjectile(GetComponentInParent<Rigidbody>().velocity);
    }

}
