using System;
using System.Diagnostics;

namespace CarDrive.Algorithms
{
    class Rbf
    {
        private readonly int _neuronCount; // J number of neuron
        private double[] _weights; // w
        private double[] _distance; // m
        private double[] _sigma;
        private double _theta;
        private readonly Neuron _neuron;


        public Rbf(int neuronNumber)
        {
            Debug.Assert(neuronNumber > 0);
            _neuronCount = neuronNumber;

            _neuron = new Neuron();

        }

        public void SetParameter(double theta, double[] weights, double[] distance, double[] sigma)
        {
            Debug.Assert(_neuronCount == weights.Length);

            _weights = weights;
            _theta = theta;
            _sigma = sigma;
            _distance = distance;
        }

        public int GetNeuronCount()
        {
            return _neuronCount;
        }

        public double GetOutput(double forward, double right, double left)
        {
            Debug.Assert(_neuronCount == _sigma.Length);

            int dimensions = _distance.Length / _neuronCount;
            double value = _theta;
            double[] inputDistance = { forward, right, left };
            for (int i = 0; i < _neuronCount; i++)
            {
                double result = _neuron.GetAngle(inputDistance, CopyOfRange(_distance, i * dimensions, i * dimensions + dimensions), _sigma[i]);
                value += _weights[i] * result;
            }
            return value;
        }

        private double[] CopyOfRange(double[] src, int start, int end)
        {
            int len = end - start;
            double[] dest = new double[len];
            Array.Copy(src, start, dest, 0, len);
            return dest;
        }
    }
}
