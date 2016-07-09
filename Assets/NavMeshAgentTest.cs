using UnityEngine;
using System.Collections;

public class NavMeshAgentTest : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Rigidbody body;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        agent.autoRepath = true;
        agent.updatePosition = false;
        agent.updateRotation = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
        agent.SetDestination(target.position);
        //body.AddForce(agent.desiredVelocity);
    }

    
}
