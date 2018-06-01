using UnityEngine;

[SerializeField]
public class ShipMovementProperties : MonoBehaviour
{
    public float maxSpeed;
    public float thrust;

    public float angularThrustProportionalGain;
    public float angularThrustIntegralGain;
    public float angularThrustDerivativeGain;

    public float maxAngularThrust;
}

