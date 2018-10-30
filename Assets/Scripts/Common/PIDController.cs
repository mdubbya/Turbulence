using System;


namespace Common
{
    public class PIDController
    {
        private float _integralTerm=0;
        private float _previousError=0;

        public float proportionalGain
        {
            get { return _proportionalGain; }
            set { _proportionalGain = value; }
        }
        private float _proportionalGain=0;

        public float integralGain
        {
            get { return _integralGain;}
            set { _integralGain = value;}
        }

        private float _integralGain=0;
        public float derivativeGain
        {
            get { return _derivativeGain;}
            set { _derivativeGain = value;}
        }
        private float _derivativeGain=0;

        public PIDController(float pGain =0, float iGain =0, float dGain=0)
        {
            _proportionalGain = pGain;
            _integralGain = iGain;
            _derivativeGain = dGain;
        }

        public float GetUpdatedOutput(float processVariable, float setPoint, float deltaTime)
        {
            float error = processVariable - setPoint;
            float pTerm = error * _proportionalGain;
            _integralTerm += (error * _integralGain);
            float dTerm = ((error-_previousError)/deltaTime)*_derivativeGain;
            _previousError = error;
            return pTerm+_integralTerm+dTerm;
        }

        public float GetUpdatedOutput(float error, float deltaTime)
        {
            float pTerm = error * _proportionalGain;
            _integralTerm += (error * _integralGain);
            float dTerm = ((error-_previousError)/deltaTime)*_derivativeGain;
            _previousError = error;
            return pTerm+_integralTerm+dTerm;
        }
    }
}