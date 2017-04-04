using System;
using System.Collections.Generic;

namespace GeneticAlgorithm.Algorithm
{
    public class Paramater
    {
        private static Random rand = new Random(DateTime.Now.Second);
        private const double _MaxW = 1, _MaxSigma = 10, _MaxM = 30;
        private const double _MinW = 0, _MinSigam = 0, _MinM = 0;
        private double _w;
        private double _sigma;
        // σ
        public double Sigma
        {
            get { return _sigma; }
            set
            {
                _sigma = value;
                if (value > _MaxSigma)
                {
                    _sigma = 10 * rand.NextDouble();
                }
                if (value < _MinSigam)
                {
                    Sigma = 2 * rand.NextDouble();
                }
            }
        }
        public double W
        {
            get { return _w; }
            set
            {
                _w = value;
                if (value > _MaxW)
                {
                    _w = _MaxW * rand.NextDouble();
                }
                if (value < _MinW)
                {
                    _w = 0.2 * rand.NextDouble();
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

        public void Crossover(Paramater other, bool far, int crossoverBit)
        {
            switch (far)
            {
                case false:
                    if (crossoverBit == 0)
                    {
                        var nums = CrossoverClose(Sigma, other.Sigma);
                        Sigma = nums.Item1;
                        other.Sigma = nums.Item2;
                    }
                    else if (crossoverBit == 1)
                    {
                        var nums = CrossoverClose(W, other.W);
                        W = nums.Item1;
                        other.W = nums.Item2;
                    }
                    else
                    {
                        int i = crossoverBit - 2;
                        var nums = CrossoverClose(M[i], other.M[i]);
                        if (nums.Item1 > _MaxM)
                        {
                            M[i] = _MaxM * rand.NextDouble();
                        }
                        else if (nums.Item1 < _MinM)
                        {
                            M[i] = rand.NextDouble();
                        }
                        else
                        {
                            M[i] = nums.Item1;
                        }

                        if (nums.Item2 > _MaxM)
                        {
                            other.M[i] = _MaxM * rand.NextDouble();
                        }
                        else if (nums.Item1 < _MinM)
                        {
                            other.M[i] = rand.NextDouble();
                        }
                        else
                        {
                            other.M[i] = nums.Item2;
                        }
                    }
                    break;
                case true:
                    if (crossoverBit == 0)
                    {
                        var nums = CrossoverFar(Sigma, other.Sigma);
                        Sigma = nums.Item1;
                        other.Sigma = nums.Item2;
                    }
                    else if (crossoverBit == 1)
                    {
                        var nums = CrossoverFar(W, other.W);
                        W = nums.Item1;
                        other.W = nums.Item2;
                    }
                    else
                    {
                        int i = crossoverBit - 2;
                        var nums = CrossoverFar(M[i], other.M[i]);
                        
                        if (nums.Item1 > _MaxM)
                        {
                            M[i] = _MaxM * rand.NextDouble();
                        }
                        else if (nums.Item1 < _MinM)
                        {
                            M[i] = rand.NextDouble();
                        }
                        else
                        {
                            M[i] = nums.Item1;
                        }

                        if (nums.Item2 > _MaxM)
                        {
                            other.M[i] = _MaxM * rand.NextDouble();
                        }
                        else if (nums.Item1 < _MinM)
                        {
                            other.M[i] = rand.NextDouble();
                        }
                        else
                        {
                            other.M[i] = nums.Item2;
                        }
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
            }
            else if (mutationBit == 1)
            {
                int controlBit = rand.Next(2);
                double noise = rand.NextDouble();
                W = controlBit == 1 ? W + s * noise : W - s * noise;
            }
            else
            {
                double result = 0;

                int controlBit = rand.Next(2);
                double noise = rand.Next(30) + rand.NextDouble();
                result = controlBit == 1 ? M[mutationBit - 2] + s * noise : M[mutationBit - 2] - s * noise;

                if (result > _MaxM)
                {
                    M[mutationBit -2] = _MaxM * rand.NextDouble();
                }
                else if (result < _MinM)
                {
                    M[mutationBit -2] = rand.NextDouble();
                }
                else
                {
                    M[mutationBit - 2] = result;
                }
            }
        }
    }
}
