using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.Profiling;

namespace AI.Task
{
    public class TaskPrioritizer : MonoBehaviour
    {

        private List<IAITask> _tasks;

        [Inject]
        public void Inject(IAITask[] tasks)
        {
            _tasks = new List<IAITask>(tasks);
        }

        public void AddTask(IAITask target)
        {
            _tasks.Add(target); 
        }

        public void RemoveTask(IAITask target)
        {
            _tasks.Remove(target);
        }

        public Vector3 GetCurrentPriority()
        {
            if(_tasks != null && _tasks.Count>0)
            {
                IAITask task = _tasks.Aggregate((current,next) => current.GetPriority() >= next.GetPriority() ? current : next);
                return task.GetTarget();
            }
            else
            {
                return gameObject.transform.position;
            }
        }



     
    }
}
