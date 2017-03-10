using System.Windows;

namespace CarDrive
{
    public class Car
    {
        public Point Center { get; set; }
        public double FaceAngle { get; private set; }
        public double Radius { get; }

        public Car()
        {
            FaceAngle = 90;
            Radius = 3;
        }

        public double GetDistanceLeft()
        {
            double distance = 0;

            return distance;
        }

        public double GetDistanceForward()
        {
            double distance = 0;

            return distance;
        }

        public double GetDistanceRight()
        {
            double distance = 0;

            return distance;
        }
    }
}
