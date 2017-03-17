using System.Windows;

namespace CarDrive.Recorder
{
    class Record
    {
        private Point _position;
        private double _leftDistance, _forwardDistance, _rightDistance, _degree;

        Record(Point pos, double left, double forward, double right, double degree)
        {
            _position = pos;
            _leftDistance = left;
            _forwardDistance = forward;
            _rightDistance = right;
            _degree = degree;
        }

    }
}
