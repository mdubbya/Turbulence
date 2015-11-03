using UnityEngine;
using System.Linq;
using System.Collections;

public class FixedCameraController : MonoBehaviour 
{
	private GameObject objectToFollow;
	private Vector3 _startPosition;
	public delegate void targetObjectDestroyedType();
	public targetObjectDestroyedType targetObjectDestroyed;


	public void Start()
	{
		_startPosition = new Vector3 (transform.position.x,
		                              transform.position.y,
		                              transform.position.z);
	}

	public void FixCameraOnObject (GameObject obj)
	{
		objectToFollow = obj;
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
