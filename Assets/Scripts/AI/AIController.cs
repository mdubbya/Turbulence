using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AI.Objective;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        private List<ObjectiveBase> objectives;
        private AITargetInfo targetInfo;

        public void Start()
        {
            objectives = GetComponents<ObjectiveBase>().ToList();
            targetInfo = GetComponent<AITargetInfo>();
            if(targetInfo==null)
            {
                targetInfo = gameObject.AddComponent<AITargetInfo>();
            }
        } 

        
        public void FixedUpdate()
        {
            objectives = (from p in objectives where p.objectiveValid select p).ToList();
            if (objectives.Count > 0)
            {
                UpdateObjectivePriorities();

                //After updating target info using basic attributes assigned to AIController, 
                //running the highest priority objective can cause the target info to be
                //altered; E.G., if an AttackObjective is higher priority than a DefendArea,
                //the AI will ignore other targets in favor of attacking the objective
                ObjectiveBase highestPriority = objectives.OrderBy(p=> p.priority).First();

                highestPriority.UpdateTargetInfo(targetInfo);
            }
        }


        private void UpdateObjectivePriorities()
        {

        }
    }
}
