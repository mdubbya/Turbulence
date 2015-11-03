using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class ShipMovementController : MonoBehaviour 
{
	[HideInInspector]
	public float thrustPercentage;
	public float maxForwardSpeed;
	public float forwardAcceleration;
	public float maxRearSpeed;
	public float rearAcceleration;
	public float maxAngularSpeed;
	public float angularAcceleration;
	public float angularThrustPercentage;
	public float brakeFactor;
	[HideInInspector]
	public bool brakesOn = false;

	public void FixedUpdate()
	{
		Rigidbody body = GetComponent<Rigidbody> ();
		float acceleration = 0;

		if (brakesOn)
		{
			body.AddForce (-brakeFactor * body.velocity);
		}
		else
		{

			if (thrustPercentage >= 0)
			{
				acceleration = forwardAcceleration;
			}
			else
			{
				acceleration = rearAcceleration;
			}
		}

		body.velocity=Vector3.ClampMagnitude (body.velocity,maxForwardSpeed);
		body.AddForce(body.transform.forward*acceleration*thrustPercentage);

		body.angularVelocity = new Vector3 (0, angularThrustPercentage * angularAcceleration, 0);

		body.position = new Vector3 
		(
			body.position.x,
			0,
			body.position.z			
		);
		

	}



}
