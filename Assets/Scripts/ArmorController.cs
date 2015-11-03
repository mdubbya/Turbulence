using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ArmorController : MonoBehaviour 
{
	public float maxHitPoints;
	public float startHitPoints;
	public List<GameObject> destructionPrefabs;
	public float explosionForce;
	public float explosionRadius;

	private float currentHitPoints;


	public void Start()
	{
		currentHitPoints = startHitPoints;
	}


	public void TakeDamage(float damage)
	{
		currentHitPoints -= damage;
		if (currentHitPoints <= 0)
		{
			DestroyObject();
		} 
	}

	public void DestroyObject()
	{
		if(destructionPrefabs.Count>0)
		{
			foreach(GameObject destructionPrefab in destructionPrefabs)
			{
				Instantiate(destructionPrefab,transform.position,transform.rotation);
			}
		}
		if (gameObject != null)
		{
			Destroy (gameObject);
		}
	}
}
