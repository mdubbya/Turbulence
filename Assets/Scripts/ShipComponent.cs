using UnityEngine;
using System;

[Serializable]
public class ShipComponent
{
	public ShipComponent()
	{
	}

	public ShipComponent(Transform componentTransform,GameObject shipComponent, ShipComponentType componentType)
	{
		transform = componentTransform;
		component = shipComponent;
		type = componentType;
	}

	public Transform transform;
	public GameObject component;
	public ShipComponentType type;
}

