using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public enum ShipComponentType{Engine,Weapon,Camera,Fuselage};

public class ShipBuilder : MonoBehaviour 
{
	public List<ShipComponent> components;

	private List<ShipComponent> existingComponents = new List<ShipComponent>();

	public void Awake()
	{
		Build ();
	}


	public void Build()
	{
		Destroy ();

		foreach(ShipComponent component in components)
		{
			AddShipComponent(component);
		}
	}
			   

	public void Destroy()
	{
		foreach(ShipComponent component in existingComponents)
		{
			Destroy(component.component);
			existingComponents.Remove(component);
		}
	}


	private void AddShipComponent(ShipComponent component)
	{	
		GameObject gObject = Instantiate(component.component,component.transform.position,component.transform.rotation) as GameObject;
		gObject.transform.SetParent(transform);
		existingComponents.Add (new ShipComponent (gObject.transform,gObject,component.type));
	}

}
