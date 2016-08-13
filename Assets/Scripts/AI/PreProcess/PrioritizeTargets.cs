using UnityEngine;
using System.Linq;
using AI.Process;


namespace AI.PreProcess
{
    public class PrioritizeTargets : MonoBehaviour
    {
        public float detectionRadius;
        public int priority;

        private CommandInfo _commandInfo;
        private Radar _radar;

        public void Start()
        {
            _commandInfo = GetComponent<CommandInfo>();
            _radar = GetComponent<Radar>();
        }

        public void Calculate()
        {
            //find the closest enemy in the detection radius
            if (_commandInfo != null && _radar.enemiesDetected.Count > 0)
            {
                _commandInfo.UpdateTargetedEnemy(_radar.enemiesDetected.OrderBy(p => Vector3.Distance(transform.position, p.position)).First().gameObject);
                _commandInfo.UpdateTargetAcquiredFlag(true);
            }
        }
    }
}
