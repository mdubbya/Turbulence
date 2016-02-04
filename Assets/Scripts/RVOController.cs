using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;
using RVO;

namespace RVO
{
    class RVOController : Singleton<RVOController>
    {

        public KdTree KDTree = new RVO.KdTree();
        public List<SteerForRVO> RVOAgents = new List<SteerForRVO>();
        public List<RVO.Obstacle> RVOObstacles = new List<RVO.Obstacle>();

        public int defaultAgentMaxNeighbors;
        public float defaultAgentMaxSpeed;
        public float defaultAgentNeighborDist;
        public float defaultAgentRadius;
        public float defaultAgentTimeHorizon;
        public float defaultAgentTimeHorizonObst;
        public Vector2 defaultAgentVelocity;

        public SteerForRVO defaultAgent;

        public void Awake()
        {
            defaultAgent = new SteerForRVO();
            defaultAgent.maxNeighbors_ = defaultAgentMaxNeighbors;
            defaultAgent.maxSpeed_ = defaultAgentMaxSpeed;
            defaultAgent.neighborDist_ = defaultAgentNeighborDist;
            defaultAgent.radius_ = defaultAgentRadius;
            defaultAgent.timeHorizon_ = defaultAgentRadius;
            defaultAgent.timeHorizonObst_ = defaultAgentTimeHorizonObst;
            defaultAgent.velocity_ = defaultAgentVelocity;
        }

        public void AddRVOAgent(SteerForRVO agent)
        {
            if (!RVOAgents.Contains(agent))
            {
                RVOAgents.Add(agent);
                KDTree.buildAgentTree();
            }

        }

        public void AddRVOObstacle(RVO.Obstacle obstacle)
        {
            if (!RVOObstacles.Contains(obstacle))
            {
                RVOObstacles.Add(obstacle);
                KDTree.buildObstacleTree();
            }

        }

        public void RebuildKDTree()
        {
            KDTree.buildAgentTree();
        }
    }

}