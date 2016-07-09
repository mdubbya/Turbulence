/*
 * 
 * RVO2 Library C#
 *
 * Copyright 2008 University of North Carolina at Chapel Hill
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Please send all bug reports to <geom@cs.unc.edu>.
 *
 * The authors may be contacted via:
 *
 * Jur van den Berg, Stephen J. Guy, Jamie Snape, Ming C. Lin, Dinesh Manocha
 * Dept. of Computer Science
 * 201 S. Columbia St.
 * Frederick P. Brooks, Jr. Computer Science Bldg.
 * Chapel Hill, N.C. 27599-3175
 * United States of America
 *
 * <http://gamma.cs.unc.edu/RVO2/>
 */

using System;
using System.Collections.Generic;
using UnitySteer.RVO;
using UnityEngine;

namespace UnitySteer.Behaviors
{

    public class RVOVehicle : AutonomousVehicle
    {
        internal List<RVOVehicle> agentNeighbors_ = new List<RVOVehicle>();
        internal List<Obstacle> obstacleNeighbors_ = new List<Obstacle>();
        internal IList<Line> orcaLines_ = new List<Line>();
        internal Vector2 position_;
        internal Vector2 prefVelocity_;
        internal Vector2 velocity_;

        public int maxNeighbors = 0;
        public float neighborDist = 0.0f;
        public float timeHorizon = 0.0f;
        public float timeHorizonObst = 0.0f;
        

        private Vector2 newVelocity_;


        /**
         * <summary>Computes the neighbors of this agent.</summary>
         */
        internal void computeNeighbors()
        {
            obstacleNeighbors_.Clear();
            float rangeSq = Mathf.Pow((timeHorizonObst * MaxSpeed + base.Radius),2);


            Vector2 obs1point_ = new Vector2(10, 10);
            Vector2 obs2point_ = new Vector2(-10, 10);
            Vector2 obs3point_ = new Vector2(-10, 0);
            Vector2 obs4point_ = new Vector2(10, 0);

            AddRVOObstacle(new List<Vector2>() { obs1point_, obs2point_, obs3point_, obs4point_ });

            agentNeighbors_.Clear();
            foreach(Vehicle vehicle in Radar.Vehicles)
            {
                RVOVehicle vehicleAsRVOVehicle = vehicle as RVOVehicle;
                if (vehicleAsRVOVehicle != null)
                {
                    agentNeighbors_.Add(vehicleAsRVOVehicle);
                }
            }
        }

        public int AddRVOObstacle(List<Vector2> vertices)
        {
            List<RVO.Obstacle> obstacles_ = new List<Obstacle>();
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

                obstacle.direction_ = (vertices[(i == vertices.Count - 1 ? 0 : i + 1)] - vertices[i]).normalized;

                if (vertices.Count == 2)
                {
                    obstacle.convex_ = true;
                }
                else
                {
                    obstacle.convex_ = (leftOf(vertices[(i == 0 ? vertices.Count - 1 : i - 1)], vertices[i], vertices[(i == vertices.Count - 1 ? 0 : i + 1)]) >= 0.0f);
                }
                
                obstacles_.Add(obstacle);
                obstacleNeighbors_.Add(obstacle);
            }



            return obstacleNo;


        }

        /**
         * <summary>Computes the new velocity of this agent.</summary>
         */
        internal void computeNewVelocity()
        {
            orcaLines_.Clear();

            float invTimeHorizonObst = 1.0f / timeHorizonObst;

            /* Create obstacle ORCA lines. */
            for (int i = 0; i < obstacleNeighbors_.Count; ++i)
            {

                Obstacle obstacle1 = obstacleNeighbors_[i];
                Obstacle obstacle2 = obstacle1.next_;

                Vector2 relativePosition1 = obstacle1.point_ - position_;
                Vector2 relativePosition2 = obstacle2.point_ - position_;

                /*
                 * Check if velocity obstacle of obstacle is already taken care
                 * of by previously constructed obstacle ORCA lines.
                 */
                bool alreadyCovered = false;

                for (int j = 0; j < orcaLines_.Count; ++j)
                {
                    if (Vector3.Cross(invTimeHorizonObst * relativePosition1 - orcaLines_[j].point, orcaLines_[j].direction).z - invTimeHorizonObst * base.Radius >= -Single.Epsilon && Vector3.Cross(invTimeHorizonObst * relativePosition2 - orcaLines_[j].point, orcaLines_[j].direction).z - invTimeHorizonObst * base.Radius >= -Single.Epsilon)
                    {
                        alreadyCovered = true;

                        break;
                    }
                }

                if (alreadyCovered)
                {
                    continue;
                }

                /* Not yet covered. Check for collisions. */
                float distSq1 = (relativePosition1).sqrMagnitude;
                float distSq2 = (relativePosition2).sqrMagnitude;

                float radiusSq = Mathf.Pow((base.Radius), 2);

                Vector2 obstacleVector = obstacle2.point_ - obstacle1.point_;
                float s = (Vector2.Dot(-relativePosition1 , obstacleVector)) / (obstacleVector).sqrMagnitude;
                float distSqLine = (-relativePosition1 - s * obstacleVector).sqrMagnitude;

                Line line;

                if (s < 0.0f && distSq1 <= radiusSq)
                {
                    /* Collision with left vertex. Ignore if non-convex. */
                    if (obstacle1.convex_)
                    {
                        line.point = new Vector2(0.0f, 0.0f);
                        line.direction = (new Vector2(-relativePosition1.y, relativePosition1.x)).normalized;
                        orcaLines_.Add(line);
                    }

                    continue;
                }
                else if (s > 1.0f && distSq2 <= radiusSq)
                {
                    /*
                     * Collision with right vertex. Ignore if non-convex or if
                     * it will be taken care of by neighboring obstacle.
                     */
                    if (obstacle2.convex_ && Vector3.Cross(relativePosition2, obstacle2.direction_).z >= 0.0f)
                    {
                        line.point = new Vector2(0.0f, 0.0f);
                        line.direction = (new Vector2(-relativePosition2.y, relativePosition2.x)).normalized;
                        orcaLines_.Add(line);
                    }

                    continue;
                }
                else if (s >= 0.0f && s < 1.0f && distSqLine <= radiusSq)
                {
                    /* Collision with obstacle segment. */
                    line.point = new Vector2(0.0f, 0.0f);
                    line.direction = -obstacle1.direction_;
                    orcaLines_.Add(line);

                    continue;
                }

                /*
                 * No collision. Compute legs. When obliquely viewed, both legs
                 * can come from a single vertex. Legs extend cut-off line when
                 * non-convex vertex.
                 */

                Vector2 leftLegDirection, rightLegDirection;

                if (s < 0.0f && distSqLine <= radiusSq)
                {
                    /*
                     * Obstacle viewed obliquely so that left vertex
                     * defines velocity obstacle.
                     */
                    if (!obstacle1.convex_)
                    {
                        /* Ignore obstacle. */
                        continue;
                    }

                    obstacle2 = obstacle1;
                    
                    float leg1 = Mathf.Sqrt(distSq1 - radiusSq);
                    leftLegDirection = new Vector2(relativePosition1.x * leg1 - relativePosition1.y * base.Radius, relativePosition1.x * base.Radius + relativePosition1.y * leg1) / distSq1;
                    rightLegDirection = new Vector2(relativePosition1.x * leg1 + relativePosition1.y * base.Radius, -relativePosition1.x * base.Radius + relativePosition1.y * leg1) / distSq1;
                }
                else if (s > 1.0f && distSqLine <= radiusSq)
                {
                    /*
                     * Obstacle viewed obliquely so that
                     * right vertex defines velocity obstacle.
                     */
                    if (!obstacle2.convex_)
                    {
                        /* Ignore obstacle. */
                        continue;
                    }

                    obstacle1 = obstacle2;

                    float leg2 = Mathf.Sqrt(distSq2 - radiusSq);
                    leftLegDirection = new Vector2(relativePosition2.x * leg2 - relativePosition2.y * base.Radius, relativePosition2.x * base.Radius + relativePosition2.y * leg2) / distSq2;
                    rightLegDirection = new Vector2(relativePosition2.x * leg2 + relativePosition2.y * base.Radius, -relativePosition2.x * base.Radius + relativePosition2.y * leg2) / distSq2;
                }
                else
                {
                    /* Usual situation. */
                    if (obstacle1.convex_)
                    {
                        float leg1 = Mathf.Sqrt(distSq1 - radiusSq);
                        leftLegDirection = new Vector2(relativePosition1.x * leg1 - relativePosition1.y * base.Radius, relativePosition1.x * base.Radius + relativePosition1.y * leg1) / distSq1;
                    }
                    else
                    {
                        /* Left vertex non-convex; left leg extends cut-off line. */
                        leftLegDirection = -obstacle1.direction_;
                    }

                    if (obstacle2.convex_)
                    {
                        float leg2 = Mathf.Sqrt(distSq2 - radiusSq);
                        rightLegDirection = new Vector2(relativePosition2.x * leg2 + relativePosition2.y * base.Radius, -relativePosition2.x * base.Radius + relativePosition2.y * leg2) / distSq2;
                    }
                    else
                    {
                        /* Right vertex non-convex; right leg extends cut-off line. */
                        rightLegDirection = obstacle1.direction_;
                    }
                }

                /*
                 * Legs can never point into neighboring edge when convex
                 * vertex, take cutoff-line of neighboring edge instead. If
                 * velocity projected on "foreign" leg, no constraint is added.
                 */

                Obstacle leftNeighbor = obstacle1.previous_;

                bool isLeftLegForeign = false;
                bool isRightLegForeign = false;

                if (obstacle1.convex_ && Vector3.Cross(leftLegDirection, -leftNeighbor.direction_).z >= 0.0f)
                {
                    /* Left leg points into obstacle. */
                    leftLegDirection = -leftNeighbor.direction_;
                    isLeftLegForeign = true;
                }

                if (obstacle2.convex_ && Vector3.Cross(rightLegDirection, obstacle2.direction_).z <= 0.0f)
                {
                    /* Right leg points into obstacle. */
                    rightLegDirection = obstacle2.direction_;
                    isRightLegForeign = true;
                }

                /* Compute cut-off centers. */
                Vector2 leftCutOff = invTimeHorizonObst * (obstacle1.point_ - position_);
                Vector2 rightCutOff = invTimeHorizonObst * (obstacle2.point_ - position_);
                Vector2 cutOffVector = rightCutOff - leftCutOff;

                /* Project current velocity on velocity obstacle. */

                /* Check if current velocity is projected on cutoff circles. */
                float t = obstacle1 == obstacle2 ? 0.5f : (Vector2.Dot((velocity_ - leftCutOff) , cutOffVector)) / (cutOffVector).sqrMagnitude;
                float tLeft = Vector2.Dot((velocity_ - leftCutOff) , leftLegDirection);
                float tRight = Vector2.Dot((velocity_ - rightCutOff) , rightLegDirection);

                if ((t < 0.0f && tLeft < 0.0f) || (obstacle1 == obstacle2 && tLeft < 0.0f && tRight < 0.0f))
                {
                    /* Project on left cut-off circle. */
                    Vector2 unitW = (velocity_ - leftCutOff).normalized;

                    line.direction = new Vector2(unitW.y, -unitW.x);
                    line.point = leftCutOff + base.Radius * invTimeHorizonObst * unitW;
                    orcaLines_.Add(line);

                    continue;
                }
                else if (t > 1.0f && tRight < 0.0f)
                {
                    /* Project on right cut-off circle. */
                    Vector2 unitW = (velocity_ - rightCutOff).normalized;

                    line.direction = new Vector2(unitW.y, -unitW.x);
                    line.point = rightCutOff + base.Radius * invTimeHorizonObst * unitW;
                    orcaLines_.Add(line);

                    continue;
                }

                /*
                 * Project on left leg, right leg, or cut-off line, whichever is
                 * closest to velocity.
                 */
                float distSqCutoff = (t < 0.0f || t > 1.0f || obstacle1 == obstacle2) ? float.PositiveInfinity : (velocity_ - (leftCutOff + t * cutOffVector)).sqrMagnitude;
                float distSqLeft = tLeft < 0.0f ? float.PositiveInfinity : (velocity_ - (leftCutOff + tLeft * leftLegDirection)).sqrMagnitude;
                float distSqRight = tRight < 0.0f ? float.PositiveInfinity : (velocity_ - (rightCutOff + tRight * rightLegDirection)).sqrMagnitude;

                if (distSqCutoff <= distSqLeft && distSqCutoff <= distSqRight)
                {
                    /* Project on cut-off line. */
                    line.direction = -obstacle1.direction_;
                    line.point = leftCutOff + base.Radius * invTimeHorizonObst * new Vector2(-line.direction.y, line.direction.x);
                    orcaLines_.Add(line);

                    continue;
                }

                if (distSqLeft <= distSqRight)
                {
                    /* Project on left leg. */
                    if (isLeftLegForeign)
                    {
                        continue;
                    }

                    line.direction = leftLegDirection;
                    line.point = leftCutOff + base.Radius * invTimeHorizonObst * new Vector2(-line.direction.y, line.direction.x);
                    orcaLines_.Add(line);

                    continue;
                }

                /* Project on right leg. */
                if (isRightLegForeign)
                {
                    continue;
                }

                line.direction = -rightLegDirection;
                line.point = rightCutOff + base.Radius * invTimeHorizonObst * new Vector2(-line.direction.y, line.direction.x);
                orcaLines_.Add(line);
            }

            int numObstLines = orcaLines_.Count;

            float invTimeHorizon = 1.0f / timeHorizon;

            /* Create agent ORCA lines. */
            for (int i = 0; i < agentNeighbors_.Count; ++i)
            {
                RVOVehicle other = agentNeighbors_[i];
                if(other==null)
                {
                    var stuff = true;
                    var ohno = true;
                }
                Vector2 relativePosition = other.position_ - position_;
                Vector2 relativeVelocity = velocity_ - other.velocity_;
                float distSq = (relativePosition).sqrMagnitude;
                float combinedRadius = base.Radius + (other as AutonomousVehicle).Radius;
                float combinedRadiusSq = Mathf.Pow((combinedRadius), 2);

                Line line;
                Vector2 u;

                if (distSq > combinedRadiusSq)
                {
                    /* No collision. */
                    Vector2 w = relativeVelocity - invTimeHorizon * relativePosition;

                    /* Vector from cutoff center to relative velocity. */
                    float wLengthSq =  w.sqrMagnitude;
                    float dotProduct1 = Vector2.Dot(w , relativePosition);

                    if (dotProduct1 < 0.0f && Mathf.Pow((dotProduct1), 2) > combinedRadiusSq * wLengthSq)
                    {
                        /* Project on cut-off circle. */
                        float wLength = Mathf.Sqrt(wLengthSq);
                        Vector2 unitW = w / wLength;

                        line.direction = new Vector2(unitW.y, -unitW.x);
                        u = (combinedRadius * invTimeHorizon - wLength) * unitW;
                    }
                    else
                    {
                        /* Project on legs. */
                        float leg = Mathf.Sqrt(distSq - combinedRadiusSq);

                        if (Vector3.Cross(relativePosition, w).z > 0.0f)
                        {
                            /* Project on left leg. */
                            line.direction = new Vector2(relativePosition.x * leg - relativePosition.y * combinedRadius, relativePosition.x * combinedRadius + relativePosition.y * leg) / distSq;
                        }
                        else
                        {
                            /* Project on right leg. */
                            line.direction = -new Vector2(relativePosition.x * leg + relativePosition.y * combinedRadius, -relativePosition.x * combinedRadius + relativePosition.y * leg) / distSq;
                        }

                        float dotProduct2 = Vector2.Dot(relativeVelocity , line.direction);
                        u = dotProduct2 * line.direction - relativeVelocity;
                    }
                }
                else
                {
                    /* Collision. Project on cut-off circle of time timeStep. */
                    float invTimeStep = 1.0f / 0.25f; //tbd;

                    /* Vector from cutoff center to relative velocity. */
                    Vector2 w = relativeVelocity - invTimeStep * relativePosition;

                    float wLength = w.magnitude;
                    Vector2 unitW = w / wLength;

                    line.direction = new Vector2(unitW.y, -unitW.x);
                    u = (combinedRadius * invTimeStep - wLength) * unitW;
                }

                line.point = velocity_ + 0.5f * u;
                orcaLines_.Add(line);
            }

            int lineFail = linearProgram2(orcaLines_, MaxSpeed, prefVelocity_, false, ref newVelocity_);

            if (lineFail < orcaLines_.Count)
            {
                linearProgram3(orcaLines_, numObstLines, lineFail, MaxSpeed, ref newVelocity_);
            }
        }
                
        
        /**
         * <summary>Solves a one-dimensional linear program on a specified line
         * subject to linear constraints defined by lines and a circular
         * constraint.</summary>
         *
         * <returns>True if successful.</returns>
         *
         * <param name="lines">Lines defining the linear constraints.</param>
         * <param name="lineNo">The specified line constraint.</param>
         * <param name="radius">The radius of the circular constraint.</param>
         * <param name="optVelocity">The optimization velocity.</param>
         * <param name="directionOpt">True if the direction should be optimized.
         * </param>
         * <param name="result">A reference to the result of the linear program.
         * </param>
         */
        private bool linearProgram1(IList<Line> lines, int lineNo, float radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
        {
            float dotProduct = Vector2.Dot(lines[lineNo].point , lines[lineNo].direction);
            float discriminant = Mathf.Pow((dotProduct), 2) + Mathf.Pow((radius), 2) - (lines[lineNo].point).sqrMagnitude;

            if (discriminant < 0.0f)
            {
                /* Max speed circle fully invalidates line lineNo. */
                return false;
            }

            float sqrtDiscriminant = Mathf.Sqrt(discriminant);
            float tLeft = -dotProduct - sqrtDiscriminant;
            float tRight = -dotProduct + sqrtDiscriminant;

            for (int i = 0; i < lineNo; ++i)
            {
                float denominator = Vector3.Cross(lines[lineNo].direction, lines[i].direction).z;
                float numerator = Vector3.Cross(lines[i].direction, lines[lineNo].point - lines[i].point).z;

                if (Math.Abs(denominator) <= Single.Epsilon)
                {
                    /* Lines lineNo and i are (almost) parallel. */
                    if (numerator < 0.0f)
                    {
                        return false;
                    }

                    continue;
                }

                float t = numerator / denominator;

                if (denominator >= 0.0f)
                {
                    /* Line i bounds line lineNo on the right. */
                    tRight = Math.Min(tRight, t);
                }
                else
                {
                    /* Line i bounds line lineNo on the left. */
                    tLeft = Math.Max(tLeft, t);
                }

                if (tLeft > tRight)
                {
                    return false;
                }
            }

            if (directionOpt)
            {
                /* Optimize direction. */
                if (Vector2.Dot(optVelocity , lines[lineNo].direction) > 0.0f)
                {
                    /* Take right extreme. */
                    result = lines[lineNo].point + tRight * lines[lineNo].direction;
                }
                else
                {
                    /* Take left extreme. */
                    result = lines[lineNo].point + tLeft * lines[lineNo].direction;
                }
            }
            else
            {
                /* Optimize closest point. */
                float t = Vector2.Dot(lines[lineNo].direction , (optVelocity - lines[lineNo].point));

                if (t < tLeft)
                {
                    result = lines[lineNo].point + tLeft * lines[lineNo].direction;
                }
                else if (t > tRight)
                {
                    result = lines[lineNo].point + tRight * lines[lineNo].direction;
                }
                else
                {
                    result = lines[lineNo].point + t * lines[lineNo].direction;
                }
            }

            return true;
        }

        /**
         * <summary>Solves a two-dimensional linear program subject to linear
         * constraints defined by lines and a circular constraint.</summary>
         *
         * <returns>The number of the line it fails on, and the number of lines
         * if successful.</returns>
         *
         * <param name="lines">Lines defining the linear constraints.</param>
         * <param name="radius">The radius of the circular constraint.</param>
         * <param name="optVelocity">The optimization velocity.</param>
         * <param name="directionOpt">True if the direction should be optimized.
         * </param>
         * <param name="result">A reference to the result of the linear program.
         * </param>
         */
        private int linearProgram2(IList<Line> lines, float radius, Vector2 optVelocity, bool directionOpt, ref Vector2 result)
        {
            if (directionOpt)
            {
                /*
                 * Optimize direction. Note that the optimization velocity is of
                 * unit length in this case.
                 */
                result = optVelocity * radius;
            }
            else if (optVelocity.sqrMagnitude > Mathf.Pow((radius), 2))
            {
                /* Optimize closest point and outside circle. */
                result = ((optVelocity) * radius).normalized;
            }
            else
            {
                /* Optimize closest point and inside circle. */
                result = optVelocity;
            }

            for (int i = 0; i < lines.Count; ++i)
            {
                if (Vector3.Cross(lines[i].direction, lines[i].point - result).z > 0.0f)
                {
                    /* Result does not satisfy constraint i. Compute new optimal result. */
                    Vector2 tempResult = result;
                    if (!linearProgram1(lines, i, radius, optVelocity, directionOpt, ref result))
                    {
                        result = tempResult;

                        return i;
                    }
                }
            }

            return lines.Count;
        }

        /**
         * <summary>Solves a two-dimensional linear program subject to linear
         * constraints defined by lines and a circular constraint.</summary>
         *
         * <param name="lines">Lines defining the linear constraints.</param>
         * <param name="numObstLines">Count of obstacle lines.</param>
         * <param name="beginLine">The line on which the 2-d linear program
         * failed.</param>
         * <param name="radius">The radius of the circular constraint.</param>
         * <param name="result">A reference to the result of the linear program.
         * </param>
         */
        private void linearProgram3(IList<Line> lines, int numObstLines, int beginLine, float radius, ref Vector2 result)
        {
            float distance = 0.0f;

            for (int i = beginLine; i < lines.Count; ++i)
            {
                if (Vector3.Cross(lines[i].direction, lines[i].point - result).z > distance)
                {
                    /* Result does not satisfy constraint of line i. */
                    IList<Line> projLines = new List<Line>();
                    for (int ii = 0; ii < numObstLines; ++ii)
                    {
                        projLines.Add(lines[ii]);
                    }

                    for (int j = numObstLines; j < i; ++j)
                    {
                        Line line;

                        float determinant = Vector3.Cross(lines[i].direction, lines[j].direction).z;

                        if (Math.Abs(determinant) <= Single.Epsilon)
                        {
                            /* Line i and line j are parallel. */
                            if (Vector2.Dot(lines[i].direction , lines[j].direction) > 0.0f)
                            {
                                /* Line i and line j point in the same direction. */
                                continue;
                            }
                            else
                            {
                                /* Line i and line j point in opposite direction. */
                                line.point = 0.5f * (lines[i].point + lines[j].point);
                            }
                        }
                        else
                        {
                            line.point = lines[i].point + (Vector3.Cross(lines[j].direction, lines[i].point - lines[j].point).z / determinant) * lines[i].direction;
                        }

                        line.direction = (lines[j].direction - lines[i].direction).normalized;
                        projLines.Add(line);
                    }

                    Vector2 tempResult = result;
                    if (linearProgram2(projLines, radius, new Vector2(-lines[i].direction.y, lines[i].direction.x), true, ref result) < projLines.Count)
                    {
                        /*
                         * This should in principle not happen. The result is by
                         * definition already in the feasible region of this
                         * linear program. If it fails, it is due to small
                         * floating point error, and the current result is kept.
                         */
                        result = tempResult;
                    }

                    distance = Vector3.Cross(lines[i].direction, lines[i].point - result).z;
                }
            }
        }
        

        /**
         * <summary>Computes the squared distance from a line segment with the
         * specified endpoints to a specified point.</summary>
         *
         * <returns>The squared distance from the line segment to the point.
         * </returns>
         *
         * <param name="vector1">The first endpoint of the line segment.</param>
         * <param name="vector2">The second endpoint of the line segment.
         * </param>
         * <param name="vector3">The point to which the squared distance is to
         * be calculated.</param>
         */
        internal static float distSqPointLineSegment(Vector2 vector1, Vector2 vector2, Vector2 vector3)
        {
            float r = (Vector2.Dot((vector3 - vector1), (vector2 - vector1))) / (vector2 - vector1).sqrMagnitude;

            if (r < 0.0f)
            {
                return (vector3 - vector1).sqrMagnitude;
            }

            if (r > 1.0f)
            {
                return (vector3 - vector2).sqrMagnitude;
            }

            return (vector3 - (vector1 + r * (vector2 - vector1))).sqrMagnitude;
        }



        /// <summary>Computes the signed distance from a line connecting the
        /// specified points to a specified point.</summary>
        /// <returns>Positive when the point c lies to the left of the line ab.
        /// </returns>
        /// <param name="a">The first point on the line.</param>
        /// <param name="b">The second point on the line.</param>
        /// <param name="c">The point to which the signed distance is to be
        /// calculated.</param>

        internal static float leftOf(Vector2 a, Vector2 b, Vector2 c)
        {
            return Vector3.Cross(a - c, b - a).z;
        }


        protected UnityEngine.Vector3 CalculateForce(UnityEngine.Vector3 oldVector)
        {
            position_ = new Vector2(this.Position.x, this.Position.z);
            velocity_ = new Vector2(this.Velocity.x, this.Velocity.z);
            prefVelocity_ = ((new Vector2(oldVector.x, oldVector.z) - position_)).normalized;
            UnityEngine.Debug.DrawRay(new UnityEngine.Vector3(position_.x, 0, position_.y), new UnityEngine.Vector3(newVelocity_.x, 0, newVelocity_.y), UnityEngine.Color.magenta, 0.2f);
            computeNeighbors();
            computeNewVelocity();
            return new UnityEngine.Vector3(newVelocity_.x, 0, newVelocity_.y);
        }

        /// <summary>
        /// Uses a desired velocity vector to adjust the vehicle's target speed and 
        /// orientation velocity.  Overrides AutonomousVehicle.SetCalculatedVelocity
        /// in order to apply RVO transformations to final velocity.
        /// </summary>
        /// <param name="velocity">Newly calculated velocity</param>
        protected override void SetCalculatedVelocity(UnityEngine.Vector3 velocity)
        {
            UnityEngine.Vector3 adjustedVelocity = CalculateForce(velocity);
            TargetSpeed = velocity.magnitude;
            OrientationVelocity = UnityEngine.Mathf.Approximately(_speed, 0) ? Transform.forward : adjustedVelocity / TargetSpeed;
        }
        
    }
}
