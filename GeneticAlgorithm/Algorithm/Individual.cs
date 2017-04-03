using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    class Individual
    {
        public double Score => _ff(this, _type);
        public double MaxErrorDegree = 0;
        public double Theta
        {
            get { return _theta; }
            set
            {
                _theta = value;
                if (value < 0)
                {
                    _theta = 0;
                }
                if (value > 1)
                {
                    _theta = 1;
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

        public Individual(int neuralSize, DataType type, FitnessFunction ff)
        {
            _type = type;
            _ff = ff;
            int paramNumber = _type == DataType.WithoutPosition ? 3 : 5;

            Param = new List<Paramater>();
            Theta = rand.NextDouble();

            for (int i = 0; i < neuralSize; i++)
            {
                Param.Add(new Paramater(paramNumber));
            }
        }

        private Individual(DataType type, FitnessFunction ff)
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
            switch (rand.Next(2))
            {
                case 0:
                    var nums = CrossoverClose(Theta, other.Theta);
                    Theta = nums.Item1;
                    other.Theta = nums.Item2;

                    for (int i = 0; i < Param.Count; i++)
                    {
                        Param[i].Crossover(other.Param[i], false);
                    }
                    break;
                case 1:
                    nums = CrossoverFar(Theta, other.Theta);
                    Theta = nums.Item1;
                    other.Theta = nums.Item2;

                    for (int i = 0; i < Param.Count; i++)
                    {
                        Param[i].Crossover(other.Param[i], true);
                    }
                    break;
            }
        }

        public void Mutation(int mutationBit)
        {
            int p = _type == DataType.WithoutPosition ? 3 : 5;
            int j = Param.Count;

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
