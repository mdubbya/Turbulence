//The MIT License(MIT)
//Copyright(c) 2008 Daniel Brauer
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
//and associated documentation files (the "Software"), to deal in the Software without 
//restriction, including without limitation the rights to use, copy, modify, merge, publish, 
//distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom 
//the Software is furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all copies or 
//substantial portions of the Software.THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


using System;
using UnityEngine;

namespace AI.PathCalculation
{
    public static class PathCalculationUtilities
    {
        //first-order intercept using absolute target position
        public static Vector3 GetFirstOrderIntercept
        (
            Vector3 shooterPosition,
            Vector3 shooterVelocity,
            float shotSpeed,
            Vector3 targetPosition,
            Vector3 targetVelocity
        )
        {
            Vector3 targetRelativePosition = targetPosition - shooterPosition;
            Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
            float t = FirstOrderInterceptTime
            (
                shotSpeed,
                targetRelativePosition,
                targetRelativeVelocity
            );
            return targetPosition + t * (targetRelativeVelocity);
        }
        //first-order intercept using relative target position
        public static float FirstOrderInterceptTime
        (
            float shotSpeed,
            Vector3 targetRelativePosition,
            Vector3 targetRelativeVelocity
        )
        {
            float velocitySquared = targetRelativeVelocity.sqrMagnitude;
            if (velocitySquared < 0.001f)
                return 0f;

            float a = velocitySquared - shotSpeed * shotSpeed;

            //handle similar velocities
            if (Mathf.Abs(a) < 0.001f)
            {
                float t = -targetRelativePosition.sqrMagnitude /
                (
                    2f * Vector3.Dot
                    (
                        targetRelativeVelocity,
                        targetRelativePosition
                    )
                );
                return Mathf.Max(t, 0f); //don't shoot back in time
            }

            float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
            float c = targetRelativePosition.sqrMagnitude;
            float determinant = b * b - 4f * a * c;

            if (determinant > 0f)
            { //determinant > 0; two intercept paths (most common)
                float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                        t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
                if (t1 > 0f)
                {
                    if (t2 > 0f)
                        return Mathf.Min(t1, t2); //both are positive
                    else
                        return t1; //only t1 is positive
                }
                else
                    return Mathf.Max(t2, 0f); //don't shoot back in time
            }
            else if (determinant < 0f) //determinant < 0; no intercept path
                return 0f;
            else //determinant = 0; one intercept path, pretty much never happens
                return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
        }

        public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
        {		
            //get vector from point on line to point in space
            Vector3 linePointToPoint = point - linePoint;
    
            float t = Vector3.Dot(linePointToPoint, lineVec);
    
            return linePoint + lineVec * t;
	    }

        public static Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
        {
    
            Vector3 vector = linePoint2 - linePoint1;
    
            Vector3 projectedPoint = ProjectPointOnLine(linePoint1, vector.normalized, point);
    
            int side = PointOnWhichSideOfLineSegment(linePoint1, linePoint2, projectedPoint);
    
            //The projected point is on the line segment
            if(side == 0){
    
                return projectedPoint;
            }
    
            if(side == 1){
    
                return linePoint1;
            }
    
            if(side == 2){
    
                return linePoint2;
            }
    
            //output is invalid
            return Vector3.zero;
        }

        public static Vector3 ClosestPointOnLine(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
        {
            Vector3 a = point-linePoint1;
            Vector3 b = (linePoint2 - linePoint1).normalized;
            float segmentLength = Vector3.Distance(linePoint1,linePoint2);
            float dotProduct = Vector3.Dot(b,a);

            if(dotProduct <=0)
            {
                return linePoint1;
            }

            if(dotProduct >= segmentLength)
            {
                return linePoint2;
            }

            return linePoint1 + (b * dotProduct);
        }

        public static int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
        {
    
            Vector3 lineVec = linePoint2 - linePoint1;
            Vector3 pointVec = point - linePoint1;
    
            float dot = Vector3.Dot(pointVec, lineVec);
    
            //point is on side of linePoint2, compared to linePoint1
            if(dot > 0){
    
                //point is on the line segment
                if(pointVec.magnitude <= lineVec.magnitude){
    
                    return 0;
                }
    
                //point is not on the line segment and it is on the side of linePoint2
                else{
    
                    return 2;
                }
            }
    
            //Point is not on side of linePoint2, compared to linePoint1.
            //Point is not on the line segment and it is on the side of linePoint1.
            else{
    
                return 1;
            }
        }
    }
}