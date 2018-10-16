using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShipController : MonoBehaviour
{
	public PlayerShipMovementController shipMovementController;


	void FixedUpdate()
	{
		//GetAxis returns a float between -1 and 1
		float rotate = Input.GetAxis ("Horizontal");
		float thrust = Input.GetAxis ("Vertical");
        
        bool fire = Input.GetButton("Fire1");
        if (fire)
        {
        }
        
        shipMovementController.ApplyShipMovement(thrust,rotate);
	}
}
