using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    class Paramater
    {
        private static Random rand = new Random(DateTime.Now.Second);
        public double Sigma; // σ
        public double W;
        public List<double> M;

        public Paramater(int dataNumber)
        {
            M = new List<double>();
            for (int i = 0; i < dataNumber; i++)
            {
                M.Add(rand.NextDouble());
            }
            W = rand.NextDouble();
            Sigma = rand.Next(9) + rand.NextDouble();
        }

        private Paramater()
        {
            M = new List<double>();
        }

        public Paramater Clone()
        {
            Paramater p = new Paramater()
            {
                W = W,
                Sigma = Sigma
            };
            for (int i = 0; i < M.Count; i++)
            {
                p.M.Add(M[i]);
            }

            return p;
        }

        public void Crossover(Paramater other, bool far)
        {
            switch (far)
            {
                case false:
                    var nums = CrossoverClose(Sigma, other.Sigma);
                    Sigma = nums.Item1;
                    other.Sigma = nums.Item2;

                    nums = CrossoverClose(W, other.W);
                    W = nums.Item1;
                    other.W = nums.Item2;

                    for (int i = 0; i < M.Count; i++)
                    {
                        nums = CrossoverClose(M[i], other.M[i]);
                        M[i] = nums.Item1;
                        other.M[i] = nums.Item2;
                    }
                    break;
                case true:
                    nums = CrossoverFar(Sigma, other.Sigma);
                    Sigma = nums.Item1;
                    other.Sigma = nums.Item2;

                    nums = CrossoverFar(W, other.W);
                    W = nums.Item1;
                    other.W = nums.Item2;

                    for (int i = 0; i < M.Count; i++)
                    {
                        nums = CrossoverFar(M[i], other.M[i]);
                        M[i] = nums.Item1;
                        other.M[i] = nums.Item2;
                    }
                    break;
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
    }
}
