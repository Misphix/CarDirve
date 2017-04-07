using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace CarDrive.Algorithms
{
    class GeneticAlgorithm : IAlgorithm
    {
        public string Name { get; }
        private const string Path = "individual";
        private int _neuralSize, _dataSize;
        private double _theta;
        private double[] _weight, _distance, _sigma;


        public GeneticAlgorithm()
        {
            Name = "Genetic Algorithm";
            ReadIndividual();

        }

        public double GetDegree(double forward, double left, double right)
        {
            Rbf rbf = new Rbf(_neuralSize);
            rbf.SetParameter(_theta, _weight, _distance, _sigma);

            return rbf.GetOutput(forward, right, left);
        }

        private void ReadIndividual()
        {
            Debug.Assert(File.Exists(Path));

            string[] content = File.ReadAllLines(Path);
            foreach (string line in content)
            {
                string[] tokens = line.Split(':');
                switch (tokens[0])
                {
                    case "Data":
                        _dataSize = int.Parse(tokens[1]);
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
            string[] tokens = data.Split(' ');
            List<double> weight = new List<double>();
            List<double> distance = new List<double>();
            List<double> sigma = new List<double>();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (i == 0)
                {
                    _theta = double.Parse(tokens[i]);
                }
                else if (i > 0 && i <=_neuralSize)
                {
                    weight.Add(double.Parse(tokens[i]));
                }
                else if (i > _neuralSize && i <= _neuralSize * (_dataSize + 1))
                {
                    distance.Add(double.Parse(tokens[i]));
                }
                else if (i > _neuralSize * (_dataSize + 1))
                {
                    sigma.Add(double.Parse(tokens[i]));
                }
            }

            _weight = weight.ToArray();
            _distance = distance.ToArray();
            _sigma = sigma.ToArray();
        }
    }
}
