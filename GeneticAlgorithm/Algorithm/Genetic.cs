using GeneticAlgorithm.Info;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    class Genetic
    {
        private readonly int _population, _maxIteration, _tolerance;
        private readonly double _mutationRate, _crossoverRate;
        private List<Individual> _individuals = new List<Individual>();
        private int _neuralSize;

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

        public Individual Start(int neuralSize)
        {
            _neuralSize = neuralSize;

            for (int i = 0; i < _population; i++)
            {
                _individuals.Add(new Individual(neuralSize, DataType.WithoutPosition));
            }

            for (int i = 0; i < _maxIteration; i++)
            {
                foreach (Individual individual in _individuals)
                {
                    if (FitnessFunction(individual, DataType.WithoutPosition) < _tolerance)
                    {
                        return individual;
                    }
                }
            }

            return null;
        }

        private double FitnessFunction(Individual individual, DataType type)
        {
            List<Data> dataList = type == DataType.WithoutPosition ? Train4D : Train6D;
            double result = 0;

            for (int i = 0; i < dataList.Count; i++)
            {
                result += dataList[i].DegreeNormalize - TargetFunction(dataList[i], individual, type);
            }

            return result / 2;
        }

        private double TargetFunction(Data data, Individual individual, DataType type)
        {
            double result = 0;
            for (int i = 0; i < _neuralSize; i++)
            {
                result += individual.Param[i].W * Phi(data, individual, type, i);
            }

            return result;
        }

        private double Phi(Data data, Individual individual , DataType type, int neuralNumber)
        {
            double[] vector;
            switch (type)
            {
                case DataType.WithoutPosition:
                    vector = new double[]
                    {
                        (data.ForwardDistance - individual.Param[neuralNumber].M[0]),
                        (data.LeftDistance - individual.Param[neuralNumber].M[0]),
                        (data.RightDistance - individual.Param[neuralNumber].M[0])
                    };
                    break;
                case DataType.WithPosition:
                    vector = new double[]
                    {
                        (data.X - individual.Param[neuralNumber].M[0]),
                        (data.Y - individual.Param[neuralNumber].M[0]),
                        (data.ForwardDistance - individual.Param[neuralNumber].M[0]),
                        (data.LeftDistance - individual.Param[neuralNumber].M[0]),
                        (data.RightDistance - individual.Param[neuralNumber].M[0])
                    };
                    break;
                default:
                    throw new ArgumentException();
            }

            double vectorDistancePower2 = CalculateDistancePower2(vector);
            double result = Math.Exp(-1 * vectorDistancePower2 / (2 * Math.Pow(individual.Param[neuralNumber].Sigma, 2)));

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
