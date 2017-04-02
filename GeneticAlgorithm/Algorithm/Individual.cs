using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    class Individual
    {
        public double Score => _ff(this, _type);
        public double MaxErrorDegree = 0;
        public double Theta;
        public readonly List<Paramater> Param;
        public delegate double FitnessFunction(Individual individual, DataType type);
        private FitnessFunction _ff;
        private DataType _type;
        private static Random r = new Random(DateTime.Now.Second);

        public Individual(int neuralSize, DataType type, FitnessFunction ff)
        {
            _type = type;
            _ff = ff;
            int paramNumber = _type == DataType.WithoutPosition ? 3 : 5;

            Param = new List<Paramater>();
            Theta = r.NextDouble();

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
    }
}
