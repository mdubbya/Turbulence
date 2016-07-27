using System;
using UnityEngine;

public class ProjectileWeaponController : MonoBehaviour, IWeaponController
{
    public ProjectileController projectilePrefab;
    public float maxProjectilesPerSecond;
    private float lastProjectileSpawned = 0;

    [SerializeField]
    private Transform _spawnLocation;
    public Transform spawnLocation
    {
        get { return _spawnLocation; }
        set { _spawnLocation = value; }
    }

    private float _weaponOutputSpeed;
    public float weaponOutputSpeed
    {
        get{ return _weaponOutputSpeed; }
    }


    public void Start()
    {
        _weaponOutputSpeed = projectilePrefab.projectileSpeed;
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
