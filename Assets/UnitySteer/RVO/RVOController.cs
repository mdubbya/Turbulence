using System.Collections.Generic;
using UnitySteer.Behaviors;

namespace UnitySteer.RVO
{
    class RVOController : Singleton<RVOController>
    {

        public KdTree KDTree = new RVO.KdTree();
        public List<SteerForRVO> RVOAgents = new List<SteerForRVO>();
        public List<RVO.Obstacle> obstacles_ = new List<RVO.Obstacle>();

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
        

        public void RebuildKDTree()
        {
            KDTree.buildAgentTree();
        }

        public int AddRVOObstacle(List<Vector2> vertices)
        {
            if (vertices.Count < 2)
            {
                return -1;
            }

            int obstacleNo = obstacles_.Count;

            for (int i = 0; i < vertices.Count; ++i)
            {
                Obstacle obstacle = new Obstacle();
                obstacle.point_ = vertices[i];

                if (i != 0)
                {
                    obstacle.previous_ = obstacles_[obstacles_.Count - 1];
                    obstacle.previous_.next_ = obstacle;
                }

                if (i == vertices.Count - 1)
                {
                    obstacle.next_ = obstacles_[obstacleNo];
                    obstacle.next_.previous_ = obstacle;
                }

                obstacle.direction_ = RVOMath.normalize(vertices[(i == vertices.Count - 1 ? 0 : i + 1)] - vertices[i]);

                if (vertices.Count == 2)
                {
                    obstacle.convex_ = true;
                }
                else
                {
                    obstacle.convex_ = (RVOMath.leftOf(vertices[(i == 0 ? vertices.Count - 1 : i - 1)], vertices[i], vertices[(i == vertices.Count - 1 ? 0 : i + 1)]) >= 0.0f);
                }

                obstacle.id_ = obstacles_.Count;
                obstacles_.Add(obstacle);
            }
            KDTree.buildObstacleTree();
            return obstacleNo;

            
        }
    }

}