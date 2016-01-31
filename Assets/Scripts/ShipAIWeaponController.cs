using UnityEngine;

class ShipAIWeaponController : MonoBehaviour
{
    public GameObject target;
    public float targetingDistance;
    public float targetZoneWidth;
    private ProjectileWeaponController[] projectileControllers;
    

    public void Start()
    {
        projectileControllers = GetComponentsInChildren<ProjectileWeaponController>();
    }

    public void FixedUpdate()
    {
        RaycastHit info = new RaycastHit();
        Vector3 right = transform.position + (transform.right * (targetZoneWidth/2));
        Vector3 left = transform.position - (transform.right * (targetZoneWidth/2));


        if(Utilities.PhysicsUtilities.RayCastPath(transform.position,transform.forward,targetZoneWidth,targetingDistance,0.2f,out info))
        {
            if(info.transform==target.transform)
            {
                foreach(ProjectileWeaponController controller in projectileControllers)
                {
                    controller.Fire();
                }
            }
        }
        
    }
}

