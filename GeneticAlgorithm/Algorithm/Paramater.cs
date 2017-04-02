using System;

namespace GeneticAlgorithm.Algorithm
{
    class Paramater
    {
        public double Sigma; // σ
        public double W;
        public double[] M;

        public Paramater(int dataNumber)
        {
            Random r = new Random();
            for (int i = 0; i < dataNumber; i++)
            {
                M[i] = r.NextDouble();
            }
            W = r.NextDouble();
            Sigma = r.Next(9) + r.NextDouble();
        }
    }
}
