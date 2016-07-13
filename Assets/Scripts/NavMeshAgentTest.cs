using UnityEngine;
using AI;
using System.Collections;

[RequireComponent(typeof(TargetSelectionController))]
public class NavMeshAgentTest : MonoBehaviour
{
    public float turnSpeed;
    public float thrust;
    public float thrustThreshold;
    public float arrivalDistance;
    public float maxSpeed;
    public float minSpeed;

    private Vector3 target;
    
    private NavMeshAgent agent;
    private Rigidbody body;
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        NavMesh.avoidancePredictionTime = 4;
        TargetSelectionController controller = GetComponent<TargetSelectionController>();
    }


    public void Update()
    {
        if (Vector3.Distance(transform.position, target) > arrivalDistance)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                Quaternion.LookRotation(agent.desiredVelocity), Time.deltaTime * turnSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                 Quaternion.LookRotation(target - transform.position), Time.deltaTime* turnSpeed);
        }

    }


    void FixedUpdate ()
    {
        if (target != null)
        {
            if (agent.SetDestination(target))
            {
                if ((Vector3.Dot(body.velocity, agent.desiredVelocity.normalized) < maxSpeed) &&
                    (Vector3.Angle(transform.forward, agent.desiredVelocity.normalized) < thrustThreshold) &&
                     Vector3.Distance(transform.position, target) > arrivalDistance)
                {
                    body.AddForce(transform.forward * thrust);
                }

                //body.velocity = agent.desiredVelocity;
                body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);

                //update navmeshagent position to synchronize path calculations
                agent.nextPosition = transform.position;
            }
        }
    }


}
