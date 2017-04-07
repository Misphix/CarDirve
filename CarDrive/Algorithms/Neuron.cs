using System;
using System.Diagnostics;

namespace CarDrive.Algorithms
{
    class Neuron
    {
        public double GetAngle(double[] input, double[] distance, double sigma)
        {
            Debug.Assert(input.Length == distance.Length);
            double vectorValue = 0;
            for (int i = 0; i < input.Length; i++)
            {
                vectorValue += Math.Pow(input[i] - distance[i], 2);
            }

            return Math.Exp((-1) * (vectorValue / (2 * Math.Pow(sigma, 2))));
        }
    }
}
