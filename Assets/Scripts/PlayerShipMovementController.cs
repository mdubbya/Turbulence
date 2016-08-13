using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class PlayerShipMovementController : MonoBehaviour 
{
    public float maxSpeed;
    public float turnTime;
    public float thrust;

	public void ApplyShipMovement(float thrustPercentage, float angularThrust)
	{
		Rigidbody body = GetComponent<Rigidbody> ();
		float acceleration = 0;

		
		if (thrustPercentage >= 0)
		{
			acceleration = thrust;
		}

		body.AddForce(body.transform.forward*acceleration*thrustPercentage);
        body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);

        float turnSpeed = 1 / turnTime;
        body.angularVelocity = new Vector3 (0, turnSpeed * angularThrust, 0);
	}



}
