using UnityEngine;
using System.Collections;

public class EngineAnimator : MonoBehaviour 
{
	public float maxJetSize;


	void FixedUpdate () 
	{
		ShipMovementController shipMovementController = GetComponentInParent<ShipMovementController> ();
		GetComponentInChildren<ParticleSystem> ().startSpeed = maxJetSize * -shipMovementController.thrustPercentage;
	}
}
