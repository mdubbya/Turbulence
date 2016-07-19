using System.Collections.Generic;
using UnityEngine;

public class MathExtension
{
    public enum Direction { left,right,none };


    //returns none when directly forward or backward
    public static Direction AngleDirection(Vector3 forwd, Vector3 targetDirection, Vector3 up)
    {
        Vector3 perpendicular = Vector3.Cross(forwd, targetDirection);
        float direction = Vector3.Dot(perpendicular, up);

        if (direction > 0.0f)
        {
            return Direction.right;
        }
        else if (direction < 0.0f)
        {
            return Direction.left;
        }
        else
        {
            return Direction.none;
        }
    }


    //Calculate the intersection point of two lines. Returns true if lines intersect, otherwise false.
    //Note that in 3d, two lines do not intersect most of the time.  Also note that the in parameters
    //take the vector as an origin and a direction/magnitude, not two endpoints
    public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);
        
        //is coplanar, and not parrallel
        if (crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }
        else
        {
            intersection = Vector3.zero;
            return false;
        }
    }


    public static Vector3 RayTangentToCircle(Vector3 circleCenter, float circleRadius, Vector3 point, bool returnPositiveTangent = true)
    {
        Vector3 direction = circleCenter - point;
        float length = direction.magnitude;
        float angle = Mathf.Atan2(direction.z, direction.x);

        float tangentLength = Mathf.Sqrt(Mathf.Pow(length, 2) - Mathf.Pow(circleRadius, 2));
        float tangentAngle = Mathf.Asin(circleRadius / length);


        float positiveTangent = angle + tangentAngle;
        float negativeTangent = angle - tangentAngle;

        if (returnPositiveTangent)
        {
            return new Vector3(Mathf.Cos(positiveTangent), 0, Mathf.Sin(positiveTangent));
        }
        else
        {
            return new Vector3(Mathf.Cos(negativeTangent), 0, Mathf.Sin(negativeTangent));
        }
    }


    //the polygon points must be in counter-clockwise order
    public static bool IsPointInPolygon(List<Vector3> polygon, Vector3 point)
    {
        bool isInside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            if (((polygon[i].z > point.z) != (polygon[j].z > point.z)) &&
            (point.x < (polygon[j].x - polygon[i].x) * (point.z - polygon[i].z) / (polygon[j].z - polygon[i].z) + polygon[i].x))
            {
                isInside = !isInside;
            }
        }
        return isInside;
    }

    
    public static Vector3 ClosestPointOnSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        float l2 = Mathf.Pow(Vector3.Distance(start, end), 2);
        if (l2 == 0.0)
        {
            return start;
        }
        float t = Mathf.Max(0, Mathf.Min(1, Vector3.Dot(point - start, end - start) / l2));
        return start + t * (end - start);
    }
}

