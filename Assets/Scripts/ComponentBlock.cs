using UnityEngine;
using System;

[Serializable]
public class ComponentBlock
{
	public ComponentBlock()
	{
	}

	public ComponentBlock(Transform componentTransform,GameObject shipComponent, Tags componentType)
	{
		transform = componentTransform;
		component = shipComponent;
		type = componentType;
	}

	public Transform transform;
	public GameObject component;
	public Tags type;
}

