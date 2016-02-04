using UnityEngine;

class DrawCollisions:MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            DebugExtension.DebugPoint(contact.point,Color.red,2,1f);
        }
    }
}

