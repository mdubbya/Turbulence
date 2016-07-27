using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIController
    {

        public float fleeThreshold;
        public ArmorController armorController;
        private List<Objective> objectives;

        public void Start()
        {
            objectives = new List<Objective>();
        }


        public void AddPatrol(Patrol patrol, int priority)
        {

        }
        

        public void AddAttackArea(AttackArea attackArea, int priority)
        {

        }


        public void AddAttackObjective(AttackObjective objective, int priority)
        {

        }


        public void AddDefendArea(DefendArea defendArea, int priority)
        {

        }


        public void AddDefendObjective(DefendObjective defendObjective, int priority)
        {

        }


        public void FixedUpdate()
        {

        }

    }
}
