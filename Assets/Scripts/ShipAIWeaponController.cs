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

        Debug.DrawRay(right,transform.forward*targetingDistance,Color.blue,0.1f);
        Debug.DrawRay(left, transform.forward*targetingDistance, Color.blue, 0.1f);
        Debug.DrawRay(transform.position, transform.forward*targetingDistance, Color.blue, 0.1f);

        if (Physics.Raycast(transform.position,transform.forward,out info,targetingDistance))
        {
            if(info.transform==target.transform)
            {
                foreach(ProjectileWeaponController controller in projectileControllers)
                {
                    controller.Fire();
                }
            }
        }
        else if (Physics.Raycast(right, transform.forward, out info, targetingDistance))
        {
            if (info.transform == target.transform)
            {
                foreach (ProjectileWeaponController controller in projectileControllers)
                {
                    controller.Fire();
                }
            }
        }
        else if (Physics.Raycast(left, transform.forward, out info, targetingDistance))
        {
            if (info.transform == target.transform)
            {
                foreach (ProjectileWeaponController controller in projectileControllers)
                {
                    controller.Fire();
                }
            }
        }
    }
}

