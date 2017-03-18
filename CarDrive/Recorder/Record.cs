using System;
using System.Windows;

namespace CarDrive.Recorder
{
    class Record
    {
        public Point Position;
        public double LeftDistance { get; }
        public double ForwardDistance { get; }
        public double RightDistance { get; }
        public double Degree { get; }

        internal Record(Point pos, double left, double forward, double right, double degree)
        {
            Position = pos;
            LeftDistance = left;
            ForwardDistance = forward;
            RightDistance = right;
            Degree = degree;
        }

        public override string ToString()
        {
            return $"{Math.Round(Position.X, 7)} {Math.Round(Position.Y, 7)} {ToString4D()}";
        }

        internal string ToString4D()
        {
            return $"{Math.Round(ForwardDistance, 7)} {Math.Round(LeftDistance, 7)} {Math.Round(RightDistance, 7)} {Math.Round(Degree, 7)}";
        }
    }
}
