using System;
using System.Windows;

namespace CarDrive.Recorder
{
    internal class Record
    {
        private const double Tolerance = double.Epsilon * 20;
        public Point Position;
        private readonly double _leftDistance, _forwardDistance, _rightDistance, _degree;

        internal Record(Point pos, double left, double forward, double right, double degree)
        {
            Position = pos;
            _leftDistance = left;
            _forwardDistance = forward;
            _rightDistance = right;
            _degree = degree;
        }

        public override string ToString()
        {
            return $"{Position.X:F7} {Position.Y:F7} {ToString4D()}";
        }

        internal string ToString4D()
        {
            string forward = Math.Abs(_forwardDistance - double.MaxValue) < Tolerance ? "MAX" : $"{_forwardDistance:F7}";
            string left = Math.Abs(_leftDistance - double.MaxValue) < Tolerance ? "MAX" : $"{_leftDistance:F7}";
            string right = Math.Abs(_rightDistance - double.MaxValue) < Tolerance ? "MAX" : $"{_rightDistance:F7}";

            return $"{forward} {left} {right} {_degree:F7}\r\n";
        }
    }
}
