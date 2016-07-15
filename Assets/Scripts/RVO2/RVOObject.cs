using UnityEngine;

namespace RVO
{
    public class RVOObject : MonoBehaviour
    {
        [SerializeField]
        private float _radius;
        public float radius
        {
            get { return _radius; }
        }
        

        private Vector2 _velocity;
        public Vector2 velocity
        {
            get { return _velocity; }
        }
        

        private Vector2 _position;
        public Vector2 position
        {
            get { return _position; }
        }

        protected Rigidbody rigidBody;

        public virtual void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            _position = new Vector2(transform.position.x, transform.position.z);
        }

        public virtual void FixedUpdate()
        {
            _position = new Vector2(transform.position.x, transform.position.z);
            _velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.z);
        }

        public void OnDrawGizmosSelected()
        {
            DebugExtension.DrawCircle(transform.position, Color.blue, radius);
            DebugExtension.DrawArrow(transform.position, transform.forward*radius, Color.blue);
        }

    }
}
