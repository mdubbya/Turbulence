using UnityEngine;
using System.Linq;
using System.Collections;

public class FixedCameraController : MonoBehaviour 
{
	private GameObject objectToFollow;

	public delegate void targetObjectDestroyedType();
	public targetObjectDestroyedType targetObjectDestroyed;

	public void FixCameraOnObject (GameObject obj)
	{
		objectToFollow = obj;
	}

	void Update () 
	{
		if (objectToFollow != null)
		{
			transform.position = new Vector3 (objectToFollow.transform.position.x, transform.position.y, objectToFollow.transform.position.z);
		}
		else
		{
			targetObjectDestroyed();
		}
	}
}
