using UnityEngine;
using System.Collections;

public class ProjectileWeaponController : MonoBehaviour 
{
	public GameObject projectilePrefab;
	public Transform projectileSpawnLocation;
	public float maxProjectilesPerSecond;
	private float lastProjectileSpawned=0;

	public void Fire()
	{
		float projectileSpawnTimeSpacing = 1 / maxProjectilesPerSecond;
		if(Time.time-lastProjectileSpawned > projectileSpawnTimeSpacing)
		{
			lastProjectileSpawned = Time.time;
			CreateAndLaunchProjectile();
		}
	}

	public void CreateAndLaunchProjectile()
	{
		GameObject projectile = Instantiate (projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation) as GameObject;
		projectile.GetComponent<ProjectileController> ().LaunchProjectile (GetComponentInParent<Rigidbody> ().velocity);
	}
	
}
