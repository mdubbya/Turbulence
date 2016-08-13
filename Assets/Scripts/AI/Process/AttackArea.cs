using System;
using UnityEngine;

namespace AI.Process
{
    public class AttackArea : CommandBase
    {
        public Vector3 attackAnchor;
        public float anchorDistance;

        private CommandInfo _commandInfo;

        private int _priority;
        public override int priority
        {
            get { return _priority; }
        }

        public void Start()
        {
            _commandInfo = GetComponent<CommandInfo>();
            FixedUpdate();
        }

        public void FixedUpdate()
        {
            _priority = basePriority * (int)Vector3.Distance(transform.position, attackAnchor) * weightDistance;
        }

        public override void UpdateObjectiveInfo()
        {
            if(_commandInfo!=null)
            {
                _commandInfo.UpdateObjectiveLocation(attackAnchor);
                if(Vector3.Distance(transform.position,attackAnchor)>anchorDistance)
                {
                    _commandInfo.UpdateMoveTarget(attackAnchor);
                }
            }
        }
    }
}
