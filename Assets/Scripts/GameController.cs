using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class GameController : Singleton<GameController>
{
	public List<GameObject> constructedOnStart;

	//todo: make readonly from inspector
	public List<GameObject> instantiatedObjects = new List<GameObject>(); 

	void Start () 
	{
		foreach (GameObject obj in constructedOnStart)
		{
			GameObject instantiatedObj = Instantiate(obj);
			instantiatedObjects.Add(instantiatedObj);
		}
	}
}
