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
        private ObjectiveInfo _objectiveInfo;

        public void Start()
        {
            _objectives = GetComponents<ObjectiveBase>().ToList();
            _objectiveInfo = GetComponent<ObjectiveInfo>();
            _flightManeuvers = GetComponents<FlightManeuverBase>().ToList();
            if(_objectiveInfo==null)
            {
                _objectiveInfo = gameObject.AddComponent<ObjectiveInfo>();
            }
        }

        
        public void FixedUpdate()
        {


            _objectives = _objectives.Where(p => p.objectiveValid).ToList();

            if (_objectives.Count > 0)
            {
                _objectives.ForEach(p => p.UpdatePriority());
                ObjectiveBase highestPriority = _objectives.OrderByDescending(p=> p.priority).First();

                _objectiveInfo.Update( highestPriority);
            }

            
            
            _flightManeuvers.ForEach(p => p.UpdateForManeuver());

        }
    }
}
