using UnityEngine;
using System.Collections.Generic;

public class PrefabLinker : MonoBehaviour 
{
	public List<GameObject> LinkedPrefabs;

	public void Awake()
	{
		foreach (GameObject obj in LinkedPrefabs)
		{
			GameObject instantiatedObj = Instantiate(obj,obj.transform.position,obj.transform.rotation) as GameObject;
			instantiatedObj.transform.SetParent(transform);
		}
	}
}
