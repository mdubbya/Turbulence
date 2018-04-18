using UnityEngine;
using System.Collections;


public class BoundingBox : MonoBehaviour
{
	public Box boundary;
	// Update is called once per frame
	void FixedUpdate ()
	{
		Rigidbody rigidbody = GetComponentInChildren<Rigidbody> ();
		if (rigidbody.position.x > boundary.xMax || rigidbody.position.x < boundary.xMin ||
		    rigidbody.position.z > boundary.zMax || rigidbody.position.x < boundary.xMin )
		{
			rigidbody.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
		}
		rigidbody.position = new Vector3 
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin,boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidbody.position.z, boundary.zMin,boundary.zMax)		
		);

	}
}

