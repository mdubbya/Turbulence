using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;


public class GameController : Singleton<GameController>
{
	public float numberOfAsteroids;
	public GameObject asteroidPrefab;

	[HideInInspector]
	public List<GameObject> instantiatedObjects = new List<GameObject>(); 

	void Start ()
	{

		for (int i=0; i < numberOfAsteroids; i++)
		{

			Vector3 nextPos = new Vector3(Random.Range(-100,100),0,Random.Range(-100,100));


			GameObject asteroid = 
				Instantiate(asteroidPrefab,
				            nextPos,
				            Random.rotation) as GameObject;
			instantiatedObjects.Add(asteroid);
		}

	}
}
