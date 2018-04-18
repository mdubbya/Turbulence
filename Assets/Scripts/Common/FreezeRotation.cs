using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
