using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    class Individual
    {
        public double Theta;
        public readonly List<Paramater> Param;

        public Individual(int neuralSize, DataType type)
        {
            Random r = new Random();
            int paramNumber = type == DataType.WithoutPosition ? 3 : 5;

            Param = new List<Paramater>();
            Theta = r.NextDouble();

            for (int i = 0; i < neuralSize; i++)
            {
                Param.Add(new Paramater(paramNumber));
            }
        }
    }
}
