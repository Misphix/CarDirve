using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    public class Paramater
    {
        private static Random rand = new Random(DateTime.Now.Second);
        private double _w;
        public double Sigma; // σ
        public double W
        {
            get { return _w; }
            set
            {
                _w = value;
                if (value > 1)
                {
                    _w = 1;
                }
                if (value < 0)
                {
                    _w = 0;
                }
            }
        }
        public List<double> M;

        public Paramater(int dataNumber)
        {
            M = new List<double>();
            for (int i = 0; i < dataNumber; i++)
            {
                M.Add(rand.NextDouble() + rand.Next(30));
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

        public void Mutation(int mutationBit)
        {
            double s = 0.2;

            if (mutationBit == 0)
            {
                int controlBit = rand.Next(2);
                double noise = rand.Next(9) + rand.NextDouble();
                Sigma = controlBit == 1 ? Sigma + s * noise : Sigma - s * noise;
                // theta
            }
            else if (mutationBit == 1)
            {
                int controlBit = rand.Next(2);
                double noise = rand.NextDouble();
                W = controlBit == 1 ? W + s * noise : W - s * noise;
            }
            else
            {
                int controlBit = rand.Next(2);
                double noise = rand.Next(30) + rand.NextDouble();
                M[mutationBit - 2] = controlBit == 1 ? M[mutationBit - 2] + s * noise : M[mutationBit - 2] - s * noise;
            }
        }
    }
}
