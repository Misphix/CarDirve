using System;
using System.Diagnostics;

namespace CarDrive.Algorithms
{
    internal class NormalAlgorithm : Algorithm
    {
        public string Name { get; }
        private const double ForwardFar0 = 12, ForwardFar1 = 14;
        private const double ForwardMedLow0 = 5, ForwardMedHigh0 = 7.5, ForwardMedHigh1 = 7.5, ForwardMedLow1 = 14;
        private const double ForwardClose0 = 7, ForwardClose1 = 5;
        private double _farForwardAlpha, _medForwardAlpha, _closeForwardAlpha;

        internal NormalAlgorithm()
        {
            Name = "Normal Algorithm";
        }

        public double GetDegree(double forward, double difference)
        {
            // left - right far: >9, med: 9~-9, small: <-9 
            CalculateForward(forward);
            return 0;
        }

        private void CalculateForward(double forward)
        {
            CalculateForwardFar(forward);
            CalculateForwardMed(forward);
            CalculateForwardClose(forward);
        }

        private void CalculateForwardFar(double forward)
        {
            Debug.Assert(ForwardFar0 <= ForwardFar1);

            if (forward >= ForwardFar1)
            {
                _farForwardAlpha = 1;
            }
            else if (forward <= ForwardFar0)
            {
                _farForwardAlpha = 0;
            }
            else
            {
                _farForwardAlpha = (forward - ForwardFar0) / (ForwardFar1 - ForwardFar0);
            }
        }

        private void CalculateForwardMed(double forward)
        {
            Debug.Assert(ForwardMedLow0 <= ForwardMedHigh0);
            Debug.Assert(ForwardMedHigh0 <= ForwardMedHigh1);
            Debug.Assert(ForwardMedHigh1 <= ForwardMedLow1);

            if (forward >= ForwardMedHigh0 && forward <= ForwardMedHigh1)
            {
                _medForwardAlpha = 1;
            }
            else if (forward <= ForwardMedLow0 || forward >= ForwardMedLow1)
            {
                _medForwardAlpha = 0;
            }
            else if (forward > ForwardMedLow0 && forward < ForwardMedHigh0)
            {
                _medForwardAlpha = (forward - ForwardMedLow0) / (ForwardMedHigh0 - ForwardMedLow0);
            }
            else if (forward < ForwardMedLow1 && forward > ForwardMedHigh1)
            {
                _medForwardAlpha = (forward - ForwardMedLow1) / (ForwardMedHigh1 - ForwardMedLow1);
            }
        }

        private void CalculateForwardClose(double forward)
        {
            Debug.Assert(ForwardClose1 <= ForwardClose0);

            if (forward <= ForwardClose1)
            {
                _closeForwardAlpha = 1;
            }
            else if (forward >= ForwardClose0)
            {
                _closeForwardAlpha = 0;
            }
            else
            {
                _closeForwardAlpha = (forward - ForwardClose0) / (ForwardClose1 - ForwardClose0);
            }
        }
    }
}
