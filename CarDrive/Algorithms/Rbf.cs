using System;
using System.Diagnostics;

namespace CarDrive.Algorithms
{
    class Rbf
    {
        private int neuronCount = 0; // J number of neuron
        private double[] weights; // w
        private double[] distance; // m
        private double[] sigma;
        private double theta;
        Neuron neuron;


        public Rbf(int _neuronNumber)
        {
            Debug.Assert(_neuronNumber > 0);
            neuronCount = _neuronNumber;

            neuron = new Neuron();

        }

        public void SetParameter(double _theta, double[] _weights, double[] _distance, double[] _sigma)
        {
            Debug.Assert(neuronCount == _weights.Length);

            weights = _weights;
            theta = _theta;
            sigma = _sigma;
            distance = _distance;
        }

        public int GetNeuronCount()
        {
            return neuronCount;
        }

        public double GetOutput(double[] _inputDistance)
        {
            Debug.Assert(_inputDistance.Length == distance.Length / neuronCount);
            Debug.Assert(neuronCount == sigma.Length);
            int dimensions = distance.Length / neuronCount;
            double value = theta;
            for (int i = 0; i < neuronCount; i++)
            {
                double result = neuron.getAngle(_inputDistance, CopyOfRange(distance, i * dimensions, i * dimensions + dimensions), this.sigma[i]);
                value += weights[i] * result;
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
