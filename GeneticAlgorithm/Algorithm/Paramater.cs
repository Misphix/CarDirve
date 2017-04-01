using System;

namespace GeneticAlgorithm.Algorithm
{
    class Paramater
    {
        public double Sigma; // σ
        public double[] W;
        public double[] M;

        public Paramater(int dataNumber)
        {
            Random r = new Random();
            for (int i = 0; i < dataNumber; i++)
            {
                W[i] = r.NextDouble();
                M[i] = r.NextDouble();
            }
            Sigma = r.Next(9) + r.NextDouble();
        }
    }
}
