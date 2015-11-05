using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;


[ExecuteInEditMode]
public class ComponentBuilder : MonoBehaviour 
{
	public List<ComponentBlock> prefabComponents;

	public void Awake()
	{
		Build ();
	}
	

	public void Build()
	{
		try
		{
			while (true)
			{
				GameObject components = transform.Find ("Component").gameObject;
				DestroyImmediate(components);
			}
		}
		catch(System.NullReferenceException)
		{
			foreach(ComponentBlock component in prefabComponents)
			{
				AddComponent(component);
			}
		}
	}

	private void AddComponent(ComponentBlock component)
	{	
		GameObject components = new GameObject();
		components.name = "Component";
		components.transform.parent = transform;
		GameObject gObject = Instantiate(component.component,
		                                 component.transform.position,
		                                 component.transform.rotation) as GameObject;
		gObject.transform.SetParent(components.transform);
		//gObject.transform.localPosition = component.component.transform.position;
		gObject.transform.localRotation = component.component.transform.rotation;
	}



}
