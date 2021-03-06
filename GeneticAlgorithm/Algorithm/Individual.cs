﻿using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    public class Individual
    {
        public double Score => _ff == null ? 1000 : _ff(this, _type);
        public double MaxErrorDegree = 0;
        public double Theta
        {
            get { return _theta; }
            set
            {
                _theta = value;
                if (value < _MinTheta)
                {
                    _theta = 0.2 * rand.NextDouble();
                }
                if (value > _MaxTheta)
                {
                    _theta = _MaxTheta * rand.NextDouble();
                }
            }
        }
        public readonly List<Paramater> Param;
        public delegate double FitnessFunction(Individual individual, DataType type);
        public int GeneticVectorSize => VectorSize();
        private FitnessFunction _ff;
        private double _theta;
        private DataType _type;
        private static Random rand = new Random(DateTime.Now.Second);
        private int _paramNumber;
        private const double _MaxTheta = 1, _MinTheta = 0;

        public Individual(int neuralSize, DataType type, FitnessFunction ff)
        {
            _type = type;
            _ff = ff;
            _paramNumber = _type == DataType.WithoutPosition ? 3 : 5;
            Param = new List<Paramater>();
            Theta = rand.NextDouble();

            for (int i = 0; i < neuralSize; i++)
            {
                Param.Add(new Paramater(_paramNumber));
            }
        }

        public Individual(int neuralSize, DataType type)
        {
            _type = type;
            _paramNumber = _type == DataType.WithoutPosition ? 3 : 5;
            Param = new List<Paramater>();

            for (int i = 0; i < neuralSize; i++)
            {
                Param.Add(new Paramater(_paramNumber));
            }
        }

        public Individual(DataType type, FitnessFunction ff)
        {
            _type = type;
            _ff = ff;
            Param = new List<Paramater>();
        }

        public override string ToString()
        {
            string result = $"{Theta:F7}";
            string m = string.Empty, sigma = string.Empty;
            foreach (Paramater param in Param)
            {
                result += $" {param.W:F7}";
                sigma += $" {param.Sigma:F7}";
                foreach (double num in param.M)
                {
                    m += $" {num:F7}";
                }
            }
            result += m + sigma;

            return result;
        }

        public void ParseData(string data)
        {
            string[] numbers = data.Split(' ');
            Theta = double.Parse(numbers[0]);
            string m = string.Empty, sigma = string.Empty;
            for (int i = 1; i < numbers.Length; i++)
            {
                if (i <= Param.Count)
                {
                    Param[i - 1].W = double.Parse(numbers[i]);
                }
                else if (i > Param.Count && i <= Param.Count * (_paramNumber + 1))
                {
                    int index = (i - Param.Count - 1) / _paramNumber;
                    int remain = (i - Param.Count - 1) % _paramNumber;
                    Param[index].M[remain] = double.Parse(numbers[i]);
                }
                else
                {
                    int index = (i - Param.Count * (_paramNumber + 1) - 1) % Param.Count;
                    Param[index].Sigma = double.Parse(numbers[i]);
                }
            }
        }

        public Individual Clone()
        {
            Individual i = new Individual(_type, _ff)
            {
                Theta = Theta
            };
            for (int j = 0; j < Param.Count; j++)
            {
                i.Param.Add(Param[j].Clone());
            }

            return i;
        }

        public void Crossover(Individual other)
        {
            int isOnePoint = rand.Next(2);
            int s = rand.Next(VectorSize());
            int e = isOnePoint == 1 ? s : rand.Next(VectorSize());
            int start = Math.Min(s, e), end = Math.Max(s, e);
            int paramSize = _type == DataType.WithoutPosition ? 3 + 2 : 5 + 2;

            switch (rand.Next(2))
            {
                case 0:
                    if (start == 0)
                    {
                        var nums = CrossoverClose(Theta, other.Theta);
                        Theta = nums.Item1;
                        other.Theta = nums.Item2;
                    }

                    if (end > 0)
                    {
                        int startParam = (start - 1) / paramSize;

                        for(int i = start == 0 ? 1 : start; i <= end; i++)
                        {
                            int param = (i - 1) / paramSize;
                            int crossoverBit = (i - 1) % paramSize;
                            Param[param].Crossover(other.Param[param], false, crossoverBit);
                        }
                    }
                    break;
                case 1:
                    if (start == 0)
                    {
                        var nums = CrossoverFar(Theta, other.Theta);
                        Theta = nums.Item1;
                        other.Theta = nums.Item2;
                    }

                    if (end > 0)
                    {
                        int startParam = (start - 1) / paramSize;

                        for (int i = start == 0 ? 1 : start; i <= end; i++)
                        {
                            int param = (i - 1) / paramSize;
                            int crossoverBit = (i - 1) % paramSize;
                            Param[param].Crossover(other.Param[param], true, crossoverBit);
                        }
                    }
                    break;
            }
        }

        public void Mutation(int mutationBit)
        {
            int p = _type == DataType.WithoutPosition ? 3 : 5;

            double s = 0.2;

            if (mutationBit == 0)
            {
                int controlBit = rand.Next(2);
                Theta = controlBit == 1 ? Theta + s * rand.NextDouble() : Theta - s * rand.NextDouble();
                // theta
            }
            else
            {
                int index = (mutationBit - 1) / (p + 2);
                int remain = (mutationBit - 1) % (p + 2);
                Param[index].Mutation(remain);
            }
        }

        private (double, double) CrossoverClose(double x1, double x2)
        {
            double sigma = rand.NextDouble();
            double r1 = x1 + sigma * (x2 - x1), r2 = x2 - sigma * (x2 - x1);

            return (r1, r2);
        }

        private (double, double) CrossoverFar(double x1, double x2)
        {
            double sigma = rand.NextDouble();
            double r1 = x1 + sigma * (x1 - x2), r2 = x2 - sigma * (x1 - x2);

            return (r1, r2);
        }

        private int VectorSize()
        {
            int p = _type == DataType.WithoutPosition ? 3 : 5;
            int j = Param.Count;

            return (p + 2) * j + 1;
        }
    }
}
