using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Objective;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        private List<ObjectiveBase> _objectives;
        private List<FlightManeuverBase> _flightManeuvers;
        private ObjectiveInfo _targetInfo;

        public void Start()
        {
            _objectives = GetComponents<ObjectiveBase>().ToList();
            _targetInfo = GetComponent<ObjectiveInfo>();
            _flightManeuvers = GetComponents<FlightManeuverBase>().ToList();
            if(_targetInfo==null)
            {
                _targetInfo = gameObject.AddComponent<ObjectiveInfo>();
            }
        }

        
        public void FixedUpdate()
        {


            _objectives = _objectives.Where(p => p.objectiveValid).ToList();

            if (_objectives.Count > 0)
            {
                _objectives.ForEach(p => p.UpdatePriority());
                ObjectiveBase highestPriority = _objectives.OrderByDescending(p=> p.priority).First();

                _targetInfo.Update( highestPriority);
            }

            
            
            _flightManeuvers.ForEach(p => p.UpdateForManeuver());

        }
    }
}
