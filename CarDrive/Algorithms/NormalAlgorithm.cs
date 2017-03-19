using System;

namespace CarDrive.Algorithms
{
    internal class NormalAlgorithm : Algorithm
    {
        public string Name { get; }

        internal NormalAlgorithm()
        {
            Name = "Normal Algorithm";
        }

        public double GetDegree(double forward, double difference)
        {
            return 0;
        }
    }
}
