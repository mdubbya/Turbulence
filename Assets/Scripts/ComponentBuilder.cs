using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class ComponentBuilder : MonoBehaviour 
{
	public List<ComponentBlock> components;

	private List<ComponentBlock> existingComponents = new List<ComponentBlock>();

	public void Awake()
	{
		Build ();
	}


	public void Build()
	{
		Destroy ();

		foreach(ComponentBlock component in components)
		{
			AddShipComponent(component);
		}
	}
			   

	public void Destroy()
	{
		foreach(ComponentBlock component in existingComponents)
		{
			Destroy(component.component);
			existingComponents.Remove(component);
		}
	}


	private void AddShipComponent(ComponentBlock component)
	{	
		GameObject gObject = Instantiate(component.component,component.transform.position,component.transform.rotation) as GameObject;
		gObject.transform.SetParent(transform);
		existingComponents.Add (new ComponentBlock (gObject.transform,gObject,component.type));
	}

}
