using System;
using GeneticAlgorithm.Algorithm;
using System.IO;
using System.Diagnostics;

namespace CarDrive.Algorithms
{
    class GeneticAlgorithm : Algorithm
    {
        public string Name { get; }
        private const string path = "individual";
        private DataType _type;
        private int _neuralSize, _dataSize;

        public GeneticAlgorithm()
        {
            Name = "Genetic Algorithm";
            Genetic genetic = new Genetic(0, 0, 0, 0, 0);
            ReadIndividual();
        }

        public double GetDegree(double forward, double difference)
        {
            throw new NotImplementedException();
        }

        public double GetDegree(double forward, double left, double right)
        {
            throw new NotImplementedException();
        }

        private void ReadIndividual()
        {
            Debug.Assert(File.Exists(path));

            string[] content = File.ReadAllLines(path);
            foreach (string line in content)
            {
                string[] tokens = line.Split(':');
                switch (tokens[0])
                {
                    case "Data":
                        _dataSize = int.Parse(tokens[1]);
                        if (_dataSize == 3)
                        {
                            _type = DataType.WithoutPosition;
                        }
                        else if (_dataSize == 5)
                        {
                            _type = DataType.WithPosition;
                        }
                        break;
                    case "Neural":
                        _neuralSize = int.Parse(tokens[1]);
                        break;
                    case "Individual":
                        ParseIndividual(tokens[1]);
                        break;
                    default:
                        throw new InvalidDataException();
                }
            }
        }

        private void ParseIndividual(string data)
        {
            data = data.Trim();
            Individual individual = new Individual(_neuralSize, _type);
            individual.ParseData(data);
        }
    }
}
