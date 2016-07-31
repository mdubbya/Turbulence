using UnityEngine;
using AI;
using System.Collections;

public class NavMeshAgentTest : MonoBehaviour
{
    public float turnSpeed;
    public float thrust;
    public float thrustThreshold;
    public float arrivalDistance;
    public float maxSpeed;
    public float minSpeed;

    private AITargetInfo _targetInfo;
    
    private NavMeshAgent _agent;
    private Rigidbody _rigidBody;
	void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
        _targetInfo = GetComponent<AITargetInfo>();
        _agent.updatePosition = false;
        _agent.updateRotation = false;
        NavMesh.avoidancePredictionTime = 4;
    }


    public void Update()
    {
        if (Vector3.Distance(transform.position, _targetInfo.moveTarget) > arrivalDistance)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                Quaternion.LookRotation(_agent.desiredVelocity), Time.deltaTime * turnSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                 Quaternion.LookRotation(_targetInfo.moveTarget - transform.position), Time.deltaTime* turnSpeed);
        }

    }


    void FixedUpdate ()
    {
        if (_agent.SetDestination(_targetInfo.moveTarget))
        {
            if ((Vector3.Dot(_rigidBody.velocity, _agent.desiredVelocity.normalized) < maxSpeed) &&
                (Vector3.Angle(transform.forward, _agent.desiredVelocity.normalized) < thrustThreshold) &&
                    Vector3.Distance(transform.position, _targetInfo.moveTarget) > arrivalDistance)
            {
                _rigidBody.AddForce(transform.forward * thrust);
            }

            //body.velocity = agent.desiredVelocity;
            _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, maxSpeed);

            //update navmeshagent position to synchronize path calculations
            _agent.nextPosition = transform.position;
        }
    }


}
