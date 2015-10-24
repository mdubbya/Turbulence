using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
	public float projectileSpeed;
	public float projectileDamage;
	public GameObject destructionAnimation;
	public GameObject destructionSound;

	public void LaunchProjectile(Vector3 startVelocity)
	{
		Rigidbody rigidBody = GetComponent<Rigidbody>();

		if (Vector3.Dot (startVelocity, transform.forward)>0)
		{
			rigidBody.velocity = startVelocity;
		}
		rigidBody.AddForce (transform.forward * projectileSpeed);
	}

	public void OnTriggerEnter(Collider other)
	{
		ArmorController armorController = other.GetComponent<ArmorController> ();
		if(armorController != null)
		{
			armorController.TakeDamage(projectileDamage);
		}
		if (destructionAnimation != null)
		{
			Instantiate (destructionAnimation, transform.position, transform.rotation);
		}
		if (destructionSound != null)
		{
			Instantiate (destructionSound, transform.position, transform.rotation);
		}
		Destroy (gameObject);
	}

}

