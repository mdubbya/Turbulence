using System.Collections.Generic;
using UnitySteer.Behaviors;
using UnityEngine;

namespace UnitySteer.RVO
{
    class RVOController : Singleton<RVOController>
    {
        public List<RVOVehicle> RVOAgents = new List<RVOVehicle>();
        public List<RVO.Obstacle> obstacles_ = new List<RVO.Obstacle>();

        public int defaultAgentMaxNeighbors;
        public float defaultAgentMaxSpeed;
        public float defaultAgentNeighborDist;
        public float defaultAgentRadius;
        public float defaultAgentTimeHorizon;
        public float defaultAgentTimeHorizonObst;
        public Vector2 defaultAgentVelocity;

        public RVOVehicle defaultAgent;

        public void Awake()
        {
            defaultAgent = new RVOVehicle();
            defaultAgent.maxNeighbors_ = defaultAgentMaxNeighbors;
            defaultAgent.maxSpeed_ = defaultAgentMaxSpeed;
            defaultAgent.neighborDist_ = defaultAgentNeighborDist;
            defaultAgent.radius_ = defaultAgentRadius;
            defaultAgent.timeHorizon_ = defaultAgentRadius;
            defaultAgent.timeHorizonObst_ = defaultAgentTimeHorizonObst;
            defaultAgent.velocity_ = defaultAgentVelocity;

        }
    }

}