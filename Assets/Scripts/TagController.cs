using UnityEngine;
using System;
using System.Collections.Generic;

public enum Tags{
					Player,
				 	Ship,
				 	AI,
					Projectile,
					ProjectileWeapon,
					EnvironmentHazard,
					ShipEngine,
					ShipFuselage,
					Model,
					MissileEngine
				}

public class TagController : MonoBehaviour
{
	[SerializeField]
	private List<Tags> tags = new List<Tags>();

	public bool HasTag(Tags tag)
	{
		return tags.Contains (tag);
	}
}

 