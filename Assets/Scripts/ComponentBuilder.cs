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


	private void GetDescendants(Transform root, ref List<GameObject> descendants)
	{
		foreach (Transform child in root)
		{
			if(child.name == "Component")
			{
				descendants.Add(child.gameObject);
			}
			GetDescendants(child,ref descendants);
		}
	}


	public void Build()
	{
		List<GameObject> components = new List<GameObject> ();
		GetDescendants (transform,ref components);
		foreach (GameObject component in components)
		{
			DestroyImmediate(component);
		}
	
		foreach(ComponentBlock component in prefabComponents)
		{
			AddComponent(component);
		}
	}


	private void AddComponent(ComponentBlock componentBlock)
	{	
		GameObject component = new GameObject();
		component.name = "Component";
		component.transform.parent = componentBlock.transform;
		component.transform.localPosition = new Vector3 (0, 0, 0);
		component.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));

		GameObject gObject = Instantiate(componentBlock.component,
		                                 componentBlock.component.transform.position,
		                                 componentBlock.component.transform.rotation) as GameObject;
		gObject.transform.SetParent(component.transform);
		gObject.transform.localPosition = componentBlock.component.transform.position;
		gObject.transform.localRotation = componentBlock.component.transform.rotation;
	}



}
