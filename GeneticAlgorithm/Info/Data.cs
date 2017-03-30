namespace GeneticAlgorithm.Info
{
    class Data
    {
        private readonly double _x, _y, _leftDistance, _forwardDistance, _rightDistance, _degree;

        public Data(double x, double y, double forward, double left, double right, double degree)
        {
            _x = x;
            _y = y;
            _leftDistance = left;
            _forwardDistance = forward;
            _rightDistance = right;
            _degree = degree;
        }
    }
}
