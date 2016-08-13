using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Process;
using AI.PostProcess;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        private List<CommandBase> _commands;
        private List<IPostProcessor> _postProcessors;
        private CommandInfo _commandInfo;

        public void Start()
        {
            _commands = GetComponents<CommandBase>().ToList();
            _commandInfo = GetComponent<CommandInfo>();
            _postProcessors = GetComponents<IPostProcessor>().ToList();
            //the objective info is essentially an output of the ai controller,
            //generally it will be created/attached by the AI controller
            if(_commandInfo==null)
            {
                _commandInfo = gameObject.AddComponent<CommandInfo>();
            }
        }

        
        public void FixedUpdate()
        {
            _commands = _commands.Where(p => p.objectiveValid).ToList();

            if (_commands.Count > 0)
            {
                CommandBase highestPriority = _commands.OrderByDescending(p=> p.priority).First();
                highestPriority.UpdateObjectiveInfo(); 
            }

            _postProcessors.ForEach(p => p.UpdateObjectiveInfo());
        }
    }
}
