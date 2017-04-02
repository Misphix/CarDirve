namespace GeneticAlgorithm.Info
{
    class Data
    {
        public readonly double X, Y, LeftDistance, ForwardDistance, RightDistance, Degree;
        public double DegreeNormalize => (Degree + 40) / 80;

        public Data(double x, double y, double forward, double left, double right, double degree)
        {
            X = x;
            Y = y;
            LeftDistance = left;
            ForwardDistance = forward;
            RightDistance = right;
            Degree = degree;
        }
    }
}
