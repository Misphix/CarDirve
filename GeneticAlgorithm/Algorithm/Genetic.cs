using GeneticAlgorithm.Info;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneticAlgorithm.Algorithm
{
    class Genetic
    {
        private readonly int _population, _maxIteration, _tolerance;
        private readonly double _mutationRate, _crossoverRate;
        private readonly List<Paramater> _param = new List<Paramater>();

        public List<Data> Train4D { private get; set; }
        public List<Data> Train6D { private get; set; }

        public Genetic(int population, int maxIteration, int tolerance, double mutation, double crossover)
        {
            _population = population;
            _maxIteration = maxIteration;
            _tolerance = tolerance;
            _mutationRate = mutation;
            _crossoverRate = crossover;
        }

        public void Start(int neuralSize)
        {
            for (int i = 0; i < neuralSize; i++)
            {
                _param.Add(new Paramater(4));
            }
        }

        private double Phi(Data data, int dataSize, int neuralNumber)
        {
            Debug.Assert(dataSize == 3 || dataSize == 5);

            double vectorDistancePower2 = 0;
            if (dataSize == 3)
            {
                double[] vector =
                {
                    (data.ForwardDistance - _param[neuralNumber].M[0]),
                    (data.LeftDistance - _param[neuralNumber].M[0]),
                    (data.RightDistance - _param[neuralNumber].M[0])
                };
                vectorDistancePower2 = CalculateDistancePower2(vector);
            }
            else
            {
                double[] vector =
                {
                    (data.X - _param[neuralNumber].M[0]),
                    (data.Y - _param[neuralNumber].M[0]),
                    (data.ForwardDistance - _param[neuralNumber].M[0]),
                    (data.LeftDistance - _param[neuralNumber].M[0]),
                    (data.RightDistance - _param[neuralNumber].M[0])
                };
                vectorDistancePower2 = CalculateDistancePower2(vector);
            }

            double result = Math.Exp(-1 * vectorDistancePower2 / (2 * Math.Pow(_param[neuralNumber].Sigma, 2)));

            return result;
        }

        private double GeneticVector(double value, int neuralSize)
        {
            return (value + 2) * neuralSize + 1;
        }

        private double CalculateDistancePower2(double[] vector)
        {
            double total = 0;

            foreach (double num in vector)
            {
                total += Math.Pow(num, 2);
            }

            return total;
        }
    }
}
