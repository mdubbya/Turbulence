using UnityEngine;
using AI;
class ShipAIWeaponController : MonoBehaviour
{
    public float targetingDistance;
    public float targetZoneWidth;
    public SingleUnityLayer targetLayer;
    private ProjectileWeaponController[] projectileControllers;

    private Vector3 target;

    public void Start()
    {
        projectileControllers = GetComponentsInChildren<ProjectileWeaponController>();
        target = GetComponent<TargetSelectionController>().targetPosition;
    }

    public void FixedUpdate()
    {
        if (target != null)
        {
            RaycastHit info = new RaycastHit();

            Collider[] colliders = Physics.OverlapSphere(transform.position, targetingDistance, targetLayer.Mask);
            foreach (Collider col in colliders)
            {
                Vector3 checkVector = transform.position + ((col.transform.position - transform.position).magnitude*transform.forward);
                if (Vector3.Distance(checkVector, col.transform.position) < targetZoneWidth)
                {
                    foreach (ProjectileWeaponController controller in projectileControllers)
                    {
                        controller.Fire();
                    }
                }
            }
            //Vector3 right = transform.position + (transform.right * (targetZoneWidth / 2));
            //Vector3 left = transform.position - (transform.right * (targetZoneWidth / 2));


            //if (Utilities.PhysicsUtilities.RayCastPath(transform.position, transform.forward, targetZoneWidth, targetingDistance, 0.2f, targetLayer, out info))
            //{
            //    if (info.transform == target.transform)
            //    {
            //        foreach (ProjectileWeaponController controller in projectileControllers)
            //        {
            //            controller.Fire();
            //        }
            //    }
            //}
        }
    }
}

