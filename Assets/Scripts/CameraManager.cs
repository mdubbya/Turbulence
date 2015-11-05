using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraManager : Singleton<CameraManager>
{
	private Dictionary<string,Camera> registeredFixedCameras = new Dictionary<string, Camera> ();
	public Camera playerCameraPrefab;

	public void Start()
	{
		foreach (GameObject obj in GameController.Instance.instantiatedObjects)
		{
			if (obj.GetComponent<TagController>() != null)
			{
				if (obj.GetComponent<TagController>().HasTag(Tags.Player))
				{
					RegisterNewFixedCamera(playerCameraPrefab,obj,"Player"+obj.GetInstanceID().ToString());
				}
			}
		}
	}


	public void RegisterNewFixedCamera(Camera cameraPrefab, GameObject targetObjectInstance, string cameraName)
	{
		Camera camera = Instantiate (cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation) as Camera;
		FixedCameraController cameraController = camera.gameObject.AddComponent<FixedCameraController> ();
		cameraController.FixCameraOnObject (targetObjectInstance);
		registeredFixedCameras.Add (cameraName, camera);
	}

	public void UnregisterFixedCamera(string cameraName)
	{
		Camera camera = registeredFixedCameras [cameraName];
		Destroy (camera);
		registeredFixedCameras.Remove (cameraName);
	}
}
