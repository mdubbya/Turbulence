﻿using UnityEngine;
using System.Collections;

public class DestroyAfterTimeLimit : MonoBehaviour {

	public float objectLifeTime;
	private float objectSpawnTime;


	void Start () 
	{
		objectSpawnTime = Time.time;
	}
	

	void Update () 
	{

		if (Time.time - objectSpawnTime >= objectLifeTime)
		{
			Destroy(gameObject);
		}
	}
}
