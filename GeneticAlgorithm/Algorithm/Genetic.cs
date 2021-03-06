﻿using GeneticAlgorithm.Info;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    public class Genetic
    {
        private readonly Random rand = new Random();
        private readonly int _population, _maxIteration;
        private readonly double _mutationRate, _crossoverRate, _tolerance;
        private List<Individual> _individuals = new List<Individual>();
        private int _neuralSize;
        private double _totalScore, _maxScore;

        internal List<Data> Train4D { private get; set; }
        internal List<Data> Train6D { private get; set; }

        public Genetic(int population, int maxIteration, double tolerance, double mutation, double crossover, int neuralSize)
        {
            _population = population;
            _maxIteration = maxIteration;
            _tolerance = tolerance;
            _mutationRate = mutation;
            _crossoverRate = crossover;
            _neuralSize = neuralSize;
        }

        public Individual Start()
        {
            // initialize individual into population
            for (int i = 0; i < _population; i++)
            {
                _individuals.Add(new Individual(_neuralSize, DataType.WithoutPosition, FitnessFunction));
            }

            for (int i = 0; i < _maxIteration; i++)
            {
                _totalScore = 0;
                _maxScore = 0;
                foreach (Individual individual in _individuals)
                {
                    _totalScore += individual.Score;
                    _maxScore = Math.Max(individual.Score, _maxScore);
                    if (individual.MaxErrorDegree < _tolerance)
                    {
                        return individual;
                    }
                }
                List<Individual> pool = Reproduction();
                _individuals = Crossover(pool);
                Mutation();
            }

            Individual minErrorIndividual = _individuals[0];
            foreach (Individual individual in _individuals)
            {
                if (individual.Score < minErrorIndividual.Score)
                {
                    minErrorIndividual = individual;
                }
            }

            return minErrorIndividual;
        }

        private List<Individual> Reproduction()
        {
            List<Individual> pool = new List<Individual>();
            for (int i = 0; i < _individuals.Count; i++)
            {
                int number = (int) Math.Round((_maxScore - _individuals[i].Score) / _totalScore * _individuals.Count);
                for (int j = 0; j < number; j++)
                {
                    pool.Add(_individuals[i].Clone());
                    if (pool.Count >= _individuals.Count)
                    {
                        break;
                    }
                }

                if (pool.Count >= _individuals.Count)
                {
                    break;
                }
            }

            if (pool.Count < _individuals.Count)
            {
                int remain = _individuals.Count - pool.Count;
                for (int i = 0; i < remain; i++)
                {
                    int random = rand.Next(_individuals.Count);
                    pool.Add(_individuals[random].Clone());
                }
            }

            return pool;
        }

        private List<Individual> Crossover(List<Individual> pool)
        {
            List<Individual> individuals = new List<Individual>();

            foreach (Individual individual in pool)
            {
                if (rand.NextDouble() > _crossoverRate)
                {
                    individuals.Add(individual.Clone());
                }
                else
                {
                    int target = rand.Next(_population);
                    Individual i1 = individual.Clone();
                    Individual i2 = pool[target].Clone();

                    i1.Crossover(i2);
                    individuals.Add(i1);
                }
            }

            return individuals;
        }

        private void Mutation()
        {
            foreach (Individual individual in _individuals)
            {
                if (rand.NextDouble() > _mutationRate)
                {
                    continue;
                }
                int mutationBit = rand.Next(individual.GeneticVectorSize);
                individual.Mutation(mutationBit);
            }
        }

        private double FitnessFunction(Individual individual, DataType type)
        {
            List<Data> dataList = type == DataType.WithoutPosition ? Train4D : Train6D;
            double result = 0;

            for (int i = 0; i < dataList.Count; i++)
            {
                double nomoralizeErrorDegree = dataList[i].DegreeNormalize - TargetFunction(dataList[i], individual, type);
                result += Math.Pow(nomoralizeErrorDegree, 2);
                individual.MaxErrorDegree = Math.Max(Math.Abs(nomoralizeErrorDegree) * 80, individual.MaxErrorDegree);
            }

            return result / 2;
        }

        public double TargetFunction(Data data, Individual individual, DataType type)
        {
            double result = 0;
            for (int i = 0; i < _neuralSize; i++)
            {
                result += individual.Param[i].W * Phi(data, individual.Param[i], type);
            }

            return result + individual.Theta;
        }

        private double Phi(Data data, Paramater param , DataType type)
        {
            double[] vector;
            switch (type)
            {
                case DataType.WithoutPosition:
                    vector = new double[]
                    {
                        (data.ForwardDistance - param.M[0]),
                        (data.LeftDistance - param.M[1]),
                        (data.RightDistance - param.M[2])
                    };
                    break;
                case DataType.WithPosition:
                    vector = new double[]
                    {
                        (data.X - param.M[0]),
                        (data.Y - param.M[1]),
                        (data.ForwardDistance - param.M[2]),
                        (data.LeftDistance - param.M[3]),
                        (data.RightDistance - param.M[4])
                    };
                    break;
                default:
                    throw new ArgumentException();
            }

            double vectorDistancePower2 = CalculateDistancePower2(vector);
            double result = Math.Exp(-1 * vectorDistancePower2 / (2 * Math.Pow(param.Sigma, 2)));

            return result;
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
