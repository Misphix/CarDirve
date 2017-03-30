using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GeneticAlgorithm.Info
{
    class DataReader
    {
        public List<Data> Data4D { get; private set; }
        public List<Data> Data6D { get; private set; }

        public DataReader(string path)
        {
            Data4D = new List<Data>();
            Data6D = new List<Data>();

            Debug.Assert(Directory.Exists(path));
            string[] files = Directory.GetFiles(path);
            Debug.Assert(files.Length > 0);

            foreach (string file in files)
            {
                ReadFile(file);
            }
        }

        private void ReadFile(string file)
        {
            string[] content = File.ReadAllLines(file);

            foreach (string line in content)
            {
                string[] tokens = line.Split(' ');

                if (tokens.Length == 4)
                {
                    AddData(tokens[0], tokens[1], tokens[2], tokens[3]);
                }
                else if (tokens.Length == 6)
                {
                    AddData(tokens[0], tokens[1], tokens[2], tokens[3], tokens[4], tokens[5]);
                }
                else
                {
                    throw new InvalidDataException(@"Data in file is wrong format");
                }
            }
        }

        private void AddData(string forward, string left, string right, string degree)
        {
            double xD = 0, yD = 0;
            double forwardD = double.Parse(forward);
            double leftD = double.Parse(left);
            double rightD = double.Parse(right);
            double degreeD = double.Parse(degree);

            Data4D.Add(new Data(xD, yD, forwardD, leftD, rightD, degreeD));
        }

        private void AddData(string x, string y, string forward, string left, string right, string degree)
        {
            double xD = double.Parse(x);
            double yD = double.Parse(y);
            double forwardD = double.Parse(forward);
            double leftD = double.Parse(left);
            double rightD = double.Parse(right);
            double degreeD = double.Parse(degree);

            Data6D.Add(new Data(xD, yD, forwardD, leftD, rightD, degreeD));
        }
    }
}
