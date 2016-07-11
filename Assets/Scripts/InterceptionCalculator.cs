using System;
using UnityEngine;

//credit for the math 
//http://www.codeproject.com/Articles/990452/Interception-of-Two-Moving-Objects-in-D-Space

public class InterceptionCalculator
{
    public Vector3? GetResult(Transform chaser, float chaserSpeed, Transform runner)
    {
        //define known quantities
        Vector3 Pc = chaser.position; //position of chaser
        float Sc = chaserSpeed; //speed of chaser
        Vector3 Pr = runner.position; //position of runner
        Vector3 Vr = runner.GetComponent<Rigidbody>().velocity; //velocity of runner
        float Sr = Vr.magnitude; //speed of runner     
        Vector3 D = Pc - Pr; //vector from the runner to the chaser
        float d = D.magnitude; //distance from the runner to the chaser
        float? t = 0; //time we will calculate to intersect the runner and chaser
        float cosTheta = (Vector3.Dot(D, Vr)) / (d * Sr); //cosine of angle between D and Vr

       
        //use law of cosines on triangle whose sides are lengths Sc*t, Sr*t, and d
        //law of cosines => C^2 = A^2 + B^2 - 2*A*B*cosTheta
        //plug Sc, t, and d into law of cosines yields (Sc*t)^2 = (Sr*t)^2 + d^2 - 2*d*Sr*t*cosTheta
        //algebraic manipulation gives quadratic equation of (Sc^2 - Sr^2)*t^2 + 2*d*Sr*cosTheta*t - d^2 = 0
        t = SolveQuadraticEquation((Sc * Sc) - (Sr * Sr), 2 * d * Sr * cosTheta, -(d * d));

        if(t!=null && t!=0 && Sr!=0 && Sc!=0)
        {
            return Pr + (Vr * t);
        }
        else
        {
            return null;
        }
    }

    public Vector3? GetResult(Vector3 chaserPosition, float chaserSpeed, Vector3 runnerPosition, Vector3 runnerVelocity)
    {
        //define known quantities
        Vector3 Pc = chaserPosition; //position of chaser
        float Sc = chaserSpeed; //speed of chaser
        Vector3 Pr = runnerPosition; //position of runner
        Vector3 Vr = runnerVelocity; //velocity of runner
        float Sr = Vr.magnitude; //speed of runner     
        Vector3 D = Pc - Pr; //vector from the runner to the chaser
        float d = D.magnitude; //distance from the runner to the chaser
        float? t = 0; //time we will calculate to intersect the runner and chaser
        float cosTheta = (Vector3.Dot(D, Vr)) / (d * Sr); //cosine of angle between D and Vr


        //use law of cosines on triangle whose sides are lengths Sc*t, Sr*t, and d
        //law of cosines => C^2 = A^2 + B^2 - 2*A*B*cosTheta
        //plug Sc, t, and d into law of cosines yields (Sc*t)^2 = (Sr*t)^2 + d^2 - 2*d*Sr*t*cosTheta
        //algebraic manipulation gives quadratic equation of (Sc^2 - Sr^2)*t^2 + 2*d*Sr*cosTheta*t - d^2 = 0
        t = SolveQuadraticEquation((Sc * Sc) - (Sr * Sr), 2 * d * Sr * cosTheta, -(d * d));

        if (t != null && t != 0 && Sr != 0 && Sc != 0)
        {
            return Pr + (Vr * t);
        }
        else
        {
            return null;
        }
    }

    //Find x for quadratic formula in form ax^2 + bx + c = 0
    //using equation x = (-b +/- sqrt(b^2 - 4*a*c))/2a
    //this method will return the smallest possible solution, or null if no solution
    private float? SolveQuadraticEquation(float a, float b, float c)
    {
        if (a != 0)
        { 
            float sqrt = ((b * b) - 4 * a * c);
            if (sqrt >= 0)
            {
                sqrt = (float)Math.Sqrt(sqrt);
                float solutionOne = (-b + sqrt) / 2 * a;
                float solutionTwo = (-b - sqrt) / 2 * a;
                if (solutionOne >= 0 && solutionTwo >= 0)
                {
                    return solutionOne < solutionTwo ? solutionOne : solutionTwo;
                }
                else if (solutionOne < 0 && solutionTwo < 0)
                {
                    return null;
                }
                else
                {
                    return solutionOne >= 0 ? solutionOne : solutionTwo;
                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
