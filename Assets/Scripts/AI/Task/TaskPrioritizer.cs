using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AI.Task
{
    public class TaskPrioritizer : MonoBehaviour
    {

        private List<IAIMoveTargetTask> _moveTargetTasks;
        private List<IAIAttackTargetTask> _attackTargetTasks;

        [Inject]
        public void Inject(IAIMoveTargetTask[] moveTargets, IAIAttackTargetTask[] attackTargets)
        {
            _moveTargetTasks = new List<IAIMoveTargetTask>(moveTargets);
            _attackTargetTasks = new List<IAIAttackTargetTask>(attackTargets);
        }

        public void AddMoveTargetTask(IAIMoveTargetTask target)
        {
            _moveTargetTasks.Add(target); 
        }

           public void RemoveMoveTargetTask(IAIMoveTargetTask target)
        {
            _moveTargetTasks.Remove(target);
        }

        public void AddAttackTargetTask(IAIMoveTargetTask target)
        {
            _moveTargetTasks.Add(target);
        }

           public void RemoveAttackTargetTask(IAIAttackTargetTask target)
        {
            _attackTargetTasks.Remove(target);
        }

        public Vector3 GetCurrentMovementPriority()
        {
            if(_moveTargetTasks != null && _moveTargetTasks.Count>0)
            {
                return ((IAIMoveTargetTask)GetHighestPriority(_moveTargetTasks.Select(p => (IAITask)p).ToList())).GetNavigationTarget();
            }
            else
            {
                return gameObject.transform.position;
            }
        }

        public Vector3 GetCurrentAttackPriority()
        {
            if(_attackTargetTasks != null && _attackTargetTasks.Count>0)
            {
                return ((IAIAttackTargetTask)GetHighestPriority(_attackTargetTasks.Select(p => (IAITask)p).ToList())).GetAttackTarget();
            }
            else
            {
                return gameObject.transform.position;
            }
        }

        private IAITask GetHighestPriority(List<IAITask> tasks)
        {
            IAITask priority = null;
            var sortedPriorities = tasks.OrderByDescending(p => p.GetPriority()).ToList();
            sortedPriorities = sortedPriorities.Where(p => p.GetPriority() == sortedPriorities.First().GetPriority()).ToList();

            if (sortedPriorities.Count() > 1)
            {
                priority = sortedPriorities.OrderByDescending(p => tasks.IndexOf(p)).Last();
            }
            else
            {
                priority = sortedPriorities.First();
            }
            
            return priority;
        }


     
    }
}
