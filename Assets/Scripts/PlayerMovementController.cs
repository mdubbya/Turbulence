using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementController : MonoBehaviour
{
	public ShipMovementController shipMovementController; 

	void Update()
	{
		//GetAxis returns a float between -1 and 1
		float rotate = Input.GetAxis ("Horizontal");
		float thrust = Input.GetAxis ("Vertical");

		shipMovementController.thrustPercentage = thrust;
		shipMovementController.angularThrustPercentage= rotate;
	}
}
