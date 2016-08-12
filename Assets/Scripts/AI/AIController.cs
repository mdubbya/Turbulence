using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Objective;
using AI.PostProcess;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        private List<ObjectiveBase> _objectives;
        private List<IPostProcessor> _postProcessors;
        private ObjectiveInfo _objectiveInfo;

        public void Start()
        {
            _objectives = GetComponents<ObjectiveBase>().ToList();
            _objectiveInfo = GetComponent<ObjectiveInfo>();
            _postProcessors = GetComponents<IPostProcessor>().ToList();
            //the objective info is essentially an output of the ai controller,
            //generally it will be created/attached by the AI controller
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
                _objectives.ForEach(p => p.GetPriority());
                ObjectiveBase highestPriority = _objectives.OrderByDescending(p=> p.GetPriority()).First();
                highestPriority.UpdateObjectiveInfo(); 
            }

            _postProcessors.ForEach(p => p.UpdateObjectiveInfo());
        }
    }
}
