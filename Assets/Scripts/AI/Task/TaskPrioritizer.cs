using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Profiling;

namespace AI.Task
{
    public class TaskPrioritizer : MonoBehaviour
    {

        private List<IAITask> _tasks;

        void Start()
        {
            _tasks = new List<IAITask>(gameObject.GetComponents<IAITask>());
        }
       
        public void AddTask(IAITask target)
        {
            _tasks.Add(target); 
        }

        public void RemoveTask(IAITask target)
        {
            _tasks.Remove(target);
        }

        public IAITask GetCurrentPriority()
        {
            //get highest priority task
            IAITask task = _tasks.Aggregate((current,next) => current.GetPriority() >= next.GetPriority() ? current : next);
            return task;
        }
     
    }
}
