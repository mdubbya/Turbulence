using UnityEngine;

namespace AI
{
    public abstract class FlightManeuverBase : MonoBehaviour
    {
        public abstract Vector3 UpdateForManeuver();
    }
}
