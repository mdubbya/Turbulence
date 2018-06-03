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
                IAIMoveTargetTask task = _moveTargetTasks.Aggregate((current,next) => current.GetPriority() >= next.GetPriority() ? current : next);
                return task.GetNavigationTarget();
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
                IAIAttackTargetTask task = _attackTargetTasks.Aggregate((current,next) => current.GetPriority() >= next.GetPriority() ? current : next);
                return task.GetAttackTarget();
            }
            else
            {
                return gameObject.transform.position;
            }
        }



     
    }
}
