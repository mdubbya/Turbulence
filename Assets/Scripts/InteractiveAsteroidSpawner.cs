using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System.Collections;


public class InteractiveAsteroidSpawner : Singleton<InteractiveAsteroidSpawner>
{
	[Range(1,10)]
	public float minAsteroidSpacing;
	public int numberOfAsteroids;
	public List<Box> excludedAreas;
	public GameObject asteroidPrefab;
	public Box asteroidSpawnArea;

	[HideInInspector]
	public List<GameObject> instantiatedObjects = new List<GameObject>(); 

	void Start ()
	{
		//possibly in future create simple struct with x,y instead of using Vector2, decrease overhead
		List<Vector2> possibleCoordinates = new List<Vector2>();
		float i = asteroidSpawnArea.xMin;
		float j = asteroidSpawnArea.zMin;
		while (i < asteroidSpawnArea.xMax)
		{
			while(j < asteroidSpawnArea.zMax)
			{
				Vector2 nextPossibleCoordinate = new Vector2(i,j);
				bool isPossible = true;
				foreach(Box excludedArea in excludedAreas)
				{
					if( (i > excludedArea.xMin && i < excludedArea.xMax)||
					     j > excludedArea.zMin && j < excludedArea.zMax)
					{
						isPossible = false;
						break;
					}
				}
				if(isPossible)
				{
					possibleCoordinates.Add(nextPossibleCoordinate);
				}
				j = j + minAsteroidSpacing;
			}
			j = asteroidSpawnArea.zMin;
			i = i + minAsteroidSpacing;
		}

		if (possibleCoordinates.Count < numberOfAsteroids)
		{
			numberOfAsteroids = Mathf.Clamp(numberOfAsteroids,0,possibleCoordinates.Count);
		}
		for (int q=0; q < numberOfAsteroids; q++)
		{
			Vector2 nextPosCoordinates = possibleCoordinates [Random.Range (0, possibleCoordinates.Count)];
			possibleCoordinates.Remove (nextPosCoordinates);
			Vector3 nextPos = new Vector3 (nextPosCoordinates.x,
		                              0,
		                              nextPosCoordinates.y);

			GameObject asteroid = 
			Instantiate (asteroidPrefab,
			            nextPos,
			            Random.rotation) as GameObject;
			instantiatedObjects.Add (asteroid);
		}
		


	}
}
