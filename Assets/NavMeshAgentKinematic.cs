﻿using UnityEngine;
using System.Collections;

public class NavMeshAgentKinematic : MonoBehaviour
{
    public Transform target;

    private UnityEngine.AI.NavMeshAgent agent;

	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	void Update ()
    {
        agent.SetDestination(target.position);
    }
}
