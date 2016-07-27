using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class PlayerShipMovementController : MonoBehaviour 
{
    private ShipMovementProperties shipMovementProperties;

    public void Start()
    {
        shipMovementProperties = GetComponent<ShipMovementProperties>();
    }

	public void ApplyShipMovement(float thrustPercentage, float angularThrust)
	{
		Rigidbody body = GetComponent<Rigidbody> ();
		float acceleration = 0;

		
		if (thrustPercentage >= 0)
		{
			acceleration = shipMovementProperties.thrust;
		}

		body.AddForce(body.transform.forward*acceleration*thrustPercentage);
        body.velocity = Vector3.ClampMagnitude(body.velocity, shipMovementProperties.maxSpeed);

        float turnSpeed = 1 / shipMovementProperties.turnTime;
        body.angularVelocity = new Vector3 (0, turnSpeed * angularThrust, 0);
	}



}
