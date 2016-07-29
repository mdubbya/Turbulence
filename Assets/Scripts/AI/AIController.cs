using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        private List<Objective> objectives;
        private AITargetInfo targetInfo;

        public void Start()
        {
            objectives = GetComponents<Objective>().ToList();
            targetInfo = new AITargetInfo();
        }

        
        public void FixedUpdate()
        {
            if (objectives.Count > 0)
            {
                UpdateObjectivePriorities();
                UpdateTargetInfo();

                //After updating target info using basic attributes assigned to AIController, 
                //running the highest priority objective can cause the target info to be
                //altered; E.G., if an AttackObjective is higher priority than a DefendArea,
                //the AI will ignore other targets in favor of attacking the objective
                Objective highestPriority = objectives.Min();

                highestPriority.UpdateTargetInfo(targetInfo);
            }

        }


        private void UpdateTargetInfo()
        {

        }


        private void UpdateObjectivePriorities()
        {

        }
    }
}
