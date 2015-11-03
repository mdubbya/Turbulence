using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class ComponentBuilder : MonoBehaviour 
{
	public List<Component> components;

	private List<Component> existingComponents = new List<Component>();

	public void Awake()
	{
		Build ();
	}


	public void Build()
	{
		Destroy ();

		foreach(Component component in components)
		{
			AddShipComponent(component);
		}
	}
			   

	public void Destroy()
	{
		foreach(Component component in existingComponents)
		{
			Destroy(component.component);
			existingComponents.Remove(component);
		}
	}


	private void AddShipComponent(Component component)
	{	
		GameObject gObject = Instantiate(component.component,component.transform.position,component.transform.rotation) as GameObject;
		gObject.transform.SetParent(transform);
		existingComponents.Add (new Component (gObject.transform,gObject,component.type));
	}

}
