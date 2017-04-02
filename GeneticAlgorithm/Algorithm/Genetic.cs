using GeneticAlgorithm.Info;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    class Genetic
    {
        private readonly Random rand = new Random();
        private readonly int _population, _maxIteration;
        private readonly double _mutationRate, _crossoverRate, _tolerance;
        private List<Individual> _individuals = new List<Individual>();
        private int _neuralSize;
        private double _totalScore, _maxScore;

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

            // initialize individual into population
            for (int i = 0; i < _population; i++)
            {
                _individuals.Add(new Individual(neuralSize, DataType.WithoutPosition, FitnessFunction));
            }

            for (int i = 0; i < _maxIteration; i++)
            {
                _totalScore = 0;
                _maxScore = 0;
                foreach (Individual individual in _individuals)
                {
                    _totalScore += individual.Score;
                    _maxScore = individual.Score > _maxScore ? individual.Score : _maxScore;
                    if (individual.MaxErrorDegree < _tolerance)
                    {
                        return individual;
                    }
                }
                List<Individual> pool = Reproduction();
                _individuals = Crossover(pool);
            }

            return null;
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

            while (individuals.Count < pool.Count)
            {
                int i = rand.Next(pool.Count), j = rand.Next(pool.Count);
                if (i == j || rand.NextDouble() > _crossoverRate)
                {
                    continue;
                }
                Individual i1 = pool[i].Clone(), i2 = pool[j].Clone();
                i1.Crossover(i2);
                individuals.Add(i1);
                individuals.Add(i2);
            }

            return individuals;
        }

        private double FitnessFunction(Individual individual, DataType type)
        {
            List<Data> dataList = type == DataType.WithoutPosition ? Train4D : Train6D;
            double result = 0;

            for (int i = 0; i < dataList.Count; i++)
            {
                double nomoralizeErrorDegree = dataList[i].DegreeNormalize - TargetFunction(dataList[i], individual, type);
                result += Math.Pow(nomoralizeErrorDegree, 2);
                individual.MaxErrorDegree = Math.Abs(nomoralizeErrorDegree) > individual.MaxErrorDegree ? Math.Abs(nomoralizeErrorDegree) * 80 : individual.MaxErrorDegree;
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

            return result + individual.Theta;
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
