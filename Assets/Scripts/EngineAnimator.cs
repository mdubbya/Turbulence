using UnityEngine;
using System.Collections;

public class EngineAnimator : MonoBehaviour 
{
	public float maxJetSize;


	void FixedUpdate () 
	{
		ShipMovementController shipMovementController = GetComponentInParent<ShipMovementController> ();
        foreach (ParticleSystem system in GetComponentsInChildren<ParticleSystem>())
        {
            system.startSpeed = maxJetSize * -shipMovementController.thrustPercentage;
        }
        foreach (AudioSource source in GetComponentsInChildren<AudioSource>())
        {
            source.volume = shipMovementController.thrustPercentage;
        }
	}
}
