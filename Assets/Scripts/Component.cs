using UnityEngine;
using System;

[Serializable]
public class Component
{
	public Component()
	{
	}

	public Component(Transform componentTransform,GameObject shipComponent, Tags componentType)
	{
		transform = componentTransform;
		component = shipComponent;
		type = componentType;
	}

	public Transform transform;
	public GameObject component;
	public Tags type;
}

