using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class AIController
    {
        private List<Objective> objectives;

        public void Start()
        {
            objectives = new List<Objective>();
        }


        public void AddObjective(Objective objective)
        {
            objectives.Add(objective);
        }


        public void FixedUpdate()
        {
            UpdateObjectivePriorities();
            UpdateTargetInfo();
            
            //After updating target info using basic attributes assigned to AIController, 
            //running the highest priority objective can cause the target info to be
            //altered; E.G., if an AttackObjective is higher priority than a DefenArea,
            //the AI will ignore other targets in favor of attacking the objective
            Objective highestPriority = objectives.Min();
        }


        private void UpdateTargetInfo()
        {

        }


        private void UpdateObjectivePriorities()
        {

        }
    }
}
