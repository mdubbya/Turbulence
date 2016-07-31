using UnityEngine;

namespace AI.Objective
{
    public struct Waypoint
    {
        public Vector3 position;
        public bool sequenced;

        public Waypoint(Vector3 _position)
        {
            position = _position;
            sequenced = false;
        }
    }
}
