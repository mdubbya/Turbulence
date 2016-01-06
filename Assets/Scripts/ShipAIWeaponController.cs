using UnityEngine;

class ShipAIWeaponController : MonoBehaviour
{
    public GameObject target;
    public float targetingDistance;
    private ProjectileWeaponController[] projectileControllers;
    

    public void Start()
    {
        projectileControllers = GetComponentsInChildren<ProjectileWeaponController>();
    }

    public void FixedUpdate()
    {
        RaycastHit info = new RaycastHit();
        if(Physics.Raycast(transform.position,transform.forward,out info,targetingDistance))
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

