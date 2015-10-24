using UnityEngine;
using System.Collections;

public class ArmorController : MonoBehaviour 
{
	public float maxHitPoints;
	public float startHitPoints;
	public GameObject destructionAnimationPrefab;

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
		if(destructionAnimationPrefab!=null)
		{
			print(transform.position);
			Instantiate(destructionAnimationPrefab,transform.position,transform.rotation);
		}
		Destroy(gameObject);
	}
}
