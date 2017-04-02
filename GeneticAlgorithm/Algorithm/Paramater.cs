using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    class Paramater
    {
        private static Random r = new Random(DateTime.Now.Second);
        public double Sigma; // σ
        public double W;
        public List<double> M;

        public Paramater(int dataNumber)
        {
            M = new List<double>();
            for (int i = 0; i < dataNumber; i++)
            {
                M.Add(r.NextDouble());
            }
            W = r.NextDouble();
            Sigma = r.Next(9) + r.NextDouble();
        }
    }
}
