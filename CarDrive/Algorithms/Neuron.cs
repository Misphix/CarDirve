using System;
using System.Diagnostics;

namespace CarDrive.Algorithms
{
    class Neuron
    {
        public double getAngle(double[] _input, double[] _distance, double _sigma)
        {
            Debug.Assert(_input.Length == _distance.Length);
            double vectorValue = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                vectorValue += Math.Pow(_input[i] - _distance[i], 2);
            }

            return Math.Exp((-1) * (vectorValue / (2 * Math.Pow(_sigma, 2))));
        }
    }
}
