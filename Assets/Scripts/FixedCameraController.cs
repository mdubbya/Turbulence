using UnityEngine;
using System.Linq;
using System.Collections;

public class FixedCameraController : MonoBehaviour 
{
	public GameObject objectToFollow;
	private Vector3 _startPosition;
	public delegate void targetObjectDestroyedType();
	public targetObjectDestroyedType targetObjectDestroyed;


	public void Start()
	{
		_startPosition = new Vector3 (transform.position.x,
		                              transform.position.y,
		                              transform.position.z);
	}

	void Update () 
	{
		if (objectToFollow != null)
		{
			transform.position = new Vector3 (objectToFollow.transform.position.x + _startPosition.x,
			                                  transform.position.y,
			                                  objectToFollow.transform.position.z+_startPosition.z);
		}
		else
		{
			targetObjectDestroyed();
		}
	}
}
