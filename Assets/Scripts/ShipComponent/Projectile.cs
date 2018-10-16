using UnityEngine;

namespace ShipComponent
{
    public enum ProjectileType
    {
        Rocket,
        Bullet
    }
    public class Projectile : MonoBehaviour
    {
        
        private float _projectileSpeed;
        public float projectileSpeed
        {
            get { return _projectileSpeed;}
        }

        private float _projectileDamage;
        public float projectileDamage
        {
            get { return _projectileDamage;}
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != 8)
            {
                HealthPointsController armorController = other.GetComponentInChildren<HealthPointsController>();
                if (armorController == null)
                {
                    armorController = other.GetComponentInParent<HealthPointsController>();
                }
                if (armorController != null)
                {
                    armorController.TakeDamage(projectileDamage);
                }
                // if (destructionAnimation != null)
                // {
                //     Instantiate(destructionAnimation, transform.position, transform.rotation);
                // }
                // if (destructionSound != null)
                // {
                //     Instantiate(destructionSound, transform.position, transform.rotation);
                // }
                Destroy(gameObject);
            }
        }
        
        
    }
}