/*
 * Agent.cs
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
using UnityEngine;
using System.Linq;

namespace AI.RVO
{
    /**
     * <summary>Defines an agent in the simulation.</summary>
     */
    [RequireComponent(typeof(AIShipMovementController))]
    public class RVOAgent : RVOObject
    {
        public float neighborDist;
        public float timeHorizon;
        public float maxSpeed;

        private List<Line> orcaLines = new List<Line>();
        private List<RVOObject> agentNeighbors = new List<RVOObject>();
        private Vector2 prefVelocity;
        private Vector3 targetPosition;

        [SerializeField]
        private int _priority;
        public int priority
        {
            get { return _priority; }
            set { _priority = value; }
        }


        public Vector3 GetRVOPosition(Vector3 currentMoveTarget)
        {
            Vector3 intermediatePrefVelocity = currentMoveTarget - transform.position;
            prefVelocity = new Vector2(intermediatePrefVelocity.x, intermediatePrefVelocity.z);
            computeNeighbors();
            Vector2 newVelocity = computeNewVelocity();
            targetPosition = transform.position + new Vector3(newVelocity.x, 0, newVelocity.y);

            return targetPosition;
        }


        /**
         * <summary>Computes the neighbors of this agent.</summary>
         */
        public void computeNeighbors()
        {
                var neighbors = Physics.OverlapSphere(transform.position, neighborDist);
                agentNeighbors= (from p in neighbors where p.GetComponent<RVOObject>()!=null select p.GetComponent<RVOObject>()).ToList();
        }

        /**
         * <summary>Computes the new velocity of this agent.</summary>
         */
        public Vector2 computeNewVelocity()
        {
            orcaLines.Clear();
            Vector2 newVelocity = new Vector2();
            float invTimeHorizon = 1.0f / timeHorizon;

            /* Create agent ORCA lines. */
            for (int i = 0; i < agentNeighbors.Count; ++i)
            {
                RVOObject other = agentNeighbors[i];

                Vector2 relativePosition = other.position - position;
                Vector2 relativeVelocity = velocity - other.velocity;
                float distSq = (relativePosition).sqrMagnitude;
                float combinedRadius = radius + other.radius;
                float combinedRadiusSq = Mathf.Pow(combinedRadius,2f);

                Line line;
                Vector2 u;

                if (distSq > combinedRadiusSq)
                {
                    /* No collision. */
                    Vector2 w = relativeVelocity - invTimeHorizon * relativePosition;

                    /* Vector from cutoff center to relative velocity. */
                    float wLengthSq = w.sqrMagnitude;
                    float dotProduct1 = Vector2.Dot(w, relativePosition);

                    if (dotProduct1 < 0.0f && Mathf.Pow(dotProduct1,2) > combinedRadiusSq * wLengthSq)
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

                        if (RVOMath.det(relativePosition, w) > 0.0f)
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
                    float invTimeStep = 1.0f / Time.fixedDeltaTime;

                    /* Vector from cutoff center to relative velocity. */
                    Vector2 w = relativeVelocity - invTimeStep * relativePosition;

                    float wLength = w.magnitude;
                    Vector2 unitW = w / wLength;

                    line.direction = new Vector2(unitW.y, -unitW.x);
                    u = (combinedRadius * invTimeStep - wLength) * unitW;
                }

                line.point = velocity + 0.5f * u;
                orcaLines.Add(line);
            }

            int lineFail = linearProgram2(orcaLines, maxSpeed, prefVelocity, false, ref newVelocity);

            if (lineFail < orcaLines.Count)
            {
                linearProgram3(orcaLines, lineFail, maxSpeed, ref newVelocity);
            }
            return newVelocity;
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
            float discriminant = Mathf.Pow(dotProduct,2) + Mathf.Pow(radius,2) - lines[lineNo].point.sqrMagnitude;

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
                float denominator = RVOMath.det(lines[lineNo].direction, lines[i].direction);
                float numerator = RVOMath.det(lines[i].direction, lines[lineNo].point - lines[i].point);

                if (Mathf.Abs(denominator) <= float.Epsilon)
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
            else if (optVelocity.sqrMagnitude > Mathf.Pow(radius,2))
            {
                /* Optimize closest point and outside circle. */
                result = optVelocity.normalized * radius;
            }
            else
            {
                /* Optimize closest point and inside circle. */
                result = optVelocity;
            }

            for (int i = 0; i < lines.Count; ++i)
            {
                if (RVOMath.det(lines[i].direction, lines[i].point - result) > 0.0f)
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
        private void linearProgram3(IList<Line> lines, int beginLine, float radius, ref Vector2 result)
        {
            float distance = 0.0f;

            for (int i = beginLine; i < lines.Count; ++i)
            {
                if (RVOMath.det(lines[i].direction, lines[i].point - result) > distance)
                {
                    /* Result does not satisfy constraint of line i. */
                    IList<Line> projLines = new List<Line>();
                                       

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

                    distance = RVOMath.det(lines[i].direction, lines[i].point - result);
                }
            }
        }
    }
}
