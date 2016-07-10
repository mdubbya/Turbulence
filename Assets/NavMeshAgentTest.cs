using UnityEngine;
using System.Collections;

public class NavMeshAgentTest : MonoBehaviour
{
    public Transform target;

    //private 

    private NavMeshAgent agent;
    private Rigidbody body;
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        agent.updatePosition = false;
        agent.updateRotation = false;
    }


    void FixedUpdate ()
    {
        float angleToDesiredVector = Vector3.Angle(transform.forward, agent.desiredVelocity);
        agent.SetDestination(target.position);
        body.AddTorque(transform.up * 10);
        body.AddForce(agent.desiredVelocity);
        agent.nextPosition = transform.position;
    }


}
