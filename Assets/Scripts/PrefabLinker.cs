using UnityEngine;
using System.Collections.Generic;

public class PrefabLinker : MonoBehaviour 
{
	public List<GameObject> LinkedPrefabs;

	public void Awake()
	{
		foreach (GameObject obj in LinkedPrefabs)
		{
			//Instantiate the object at at it's position
			GameObject instantiatedObj = Instantiate(obj,obj.transform.position,obj.transform.rotation) as GameObject;
			//Set object's parent as gameobject attached to this script
			instantiatedObj.transform.parent=transform;
			//Zero out local rotation/position so the it's location is relative to the
			//gameobject attached to this script, instead of in world space
			instantiatedObj.transform.localPosition = obj.transform.position;
			instantiatedObj.transform.localRotation = obj.transform.rotation;
		}
	}
}
