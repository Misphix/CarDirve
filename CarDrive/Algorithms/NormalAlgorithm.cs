using System;
using System.Diagnostics;

namespace CarDrive.Algorithms
{
    internal class NormalAlgorithm : Algorithm
    {
        public string Name { get; }
        // Forward
        private const double ForwardFar0 = 15, ForwardFar1 = 20;
        private const double ForwardMediumLow0 = 5, ForwardMediumHigh0 = 7, ForwardMediumHigh1 = 8.7, ForwardMediumLow1 = 11.8;
        private const double ForwardClose0 = 9, ForwardClose1 = 4;
        // Difference
        private const double DifferenceLarge0 = 2, DifferenceLarge1 = 6;
        private const double DifferenceMediumLow0 = -7, DifferenceMediumHigh0 = -6, DifferenceMediumHigh1 = 2, DifferenceMediumLow1 = 6;
        private const double DifferenceSmall0 = -1, DifferenceSmall1 = -3;
        // Steering wheel
        private const double DegreeLarge0 = 20, DegreeLarge1 = 35;
        private const double DegreeMediumLow0 = -25, DegreeMediumHigh0 = -20, DegreeMediumHigh1 = 20, DegreeMediumLow1 = 25;
        private const double DegreeSmall0 = -20, DegreeSmall1 = -35;

        private double _farForwardAlpha, _mediumForwardAlpha, _closeForwardAlpha;
        private double _largeDifferenceAlpha, _mediumDifferenceAlpha, _smallDifferenceAlpha;

        internal NormalAlgorithm()
        {
            Name = "Normal Algorithm";
        }

        public double GetDegree(double forward, double difference)
        {

            CalculateForward(forward);
            CalculateDifference(difference);

            // Forword small and difference is large
            double numerator = 0, denominator = 0;
            double alpha = Math.Min(_closeForwardAlpha, _largeDifferenceAlpha);
            double degree = TurnRight(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword small and difference is medium
            alpha = Math.Min(_closeForwardAlpha, _mediumDifferenceAlpha);
            degree = KeepWheel(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword small and difference is small
            alpha = Math.Min(_closeForwardAlpha, _smallDifferenceAlpha);
            degree = TurnLeft(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword medium and difference is large
            alpha = Math.Min(_mediumForwardAlpha, _largeDifferenceAlpha);
            degree = TurnRight(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword medium and difference is medium
            alpha = Math.Min(_mediumForwardAlpha, _mediumDifferenceAlpha);
            degree = KeepWheel(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword medium and difference is small
            alpha = Math.Min(_mediumForwardAlpha, _smallDifferenceAlpha);
            degree = TurnLeft(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword medium and difference is large
            alpha = Math.Min(_farForwardAlpha, _largeDifferenceAlpha);
            degree = TurnRight(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword medium and difference is medium
            alpha = Math.Min(_farForwardAlpha, _mediumDifferenceAlpha);
            degree = KeepWheel(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            // Forword medium and difference is small
            alpha = Math.Min(_farForwardAlpha, _smallDifferenceAlpha);
            degree = TurnLeft(alpha);
            numerator += alpha * degree;
            denominator += alpha;

            double result = numerator / denominator;
            result = double.IsNaN(result) ? 0 : result;

            return result;
        }

        private void CalculateForward(double forward)
        {
            CalculateForwardFar(forward);
            CalculateForwardMedium(forward);
            CalculateForwardClose(forward);
        }

        private void CalculateForwardFar(double forward)
        {
            Debug.Assert(ForwardFar0 <= ForwardFar1);

            if (forward >= ForwardFar1)
            {
                _farForwardAlpha = 1;
            }
            else if (forward <= ForwardFar0)
            {
                _farForwardAlpha = 0;
            }
            else
            {
                _farForwardAlpha = (forward - ForwardFar0) / (ForwardFar1 - ForwardFar0);
            }
        }

        private void CalculateForwardMedium(double forward)
        {
            Debug.Assert(ForwardMediumLow0 <= ForwardMediumHigh0);
            Debug.Assert(ForwardMediumHigh0 <= ForwardMediumHigh1);
            Debug.Assert(ForwardMediumHigh1 <= ForwardMediumLow1);

            if (forward >= ForwardMediumHigh0 && forward <= ForwardMediumHigh1)
            {
                _mediumForwardAlpha = 1;
            }
            else if (forward <= ForwardMediumLow0 || forward >= ForwardMediumLow1)
            {
                _mediumForwardAlpha = 0;
            }
            else if (forward > ForwardMediumLow0 && forward < ForwardMediumHigh0)
            {
                _mediumForwardAlpha = (forward - ForwardMediumLow0) / (ForwardMediumHigh0 - ForwardMediumLow0);
            }
            else if (forward < ForwardMediumLow1 && forward > ForwardMediumHigh1)
            {
                _mediumForwardAlpha = (forward - ForwardMediumLow1) / (ForwardMediumHigh1 - ForwardMediumLow1);
            }
        }

        private void CalculateForwardClose(double forward)
        {
            Debug.Assert(ForwardClose1 <= ForwardClose0);

            if (forward <= ForwardClose1)
            {
                _closeForwardAlpha = 1;
            }
            else if (forward >= ForwardClose0)
            {
                _closeForwardAlpha = 0;
            }
            else
            {
                _closeForwardAlpha = (forward - ForwardClose0) / (ForwardClose1 - ForwardClose0);
            }
        }

        private void CalculateDifference(double difference)
        {
            CalculateDifferenceLarge(difference);
            CalculateDifferenceMedium(difference);
            CalculateDifferenceSmall(difference);
        }

        private void CalculateDifferenceLarge(double difference)
        {
            Debug.Assert(DifferenceLarge0 <= DifferenceLarge1);

            if (difference >= DifferenceLarge1)
            {
                _largeDifferenceAlpha = 1;
            }
            else if (difference <= DifferenceLarge0)
            {
                _largeDifferenceAlpha = 0;
            }
            else
            {
                _largeDifferenceAlpha = (difference - DifferenceLarge0) / (ForwardFar1 - DifferenceLarge0);
            }
        }

        private void CalculateDifferenceMedium(double difference)
        {
            Debug.Assert(DifferenceMediumLow0 <= DifferenceMediumHigh0);
            Debug.Assert(DifferenceMediumHigh0 <= DifferenceMediumHigh1);
            Debug.Assert(DifferenceMediumHigh1 <= DifferenceMediumLow1);

            if (difference >= DifferenceMediumHigh0 && difference <= DifferenceMediumHigh1)
            {
                _mediumDifferenceAlpha = 1;
            }
            else if (difference <= DifferenceMediumLow0 || difference >= DifferenceMediumLow1)
            {
                _mediumDifferenceAlpha = 0;
            }
            else if (difference > DifferenceMediumLow0 && difference < DifferenceMediumHigh0)
            {
                _mediumDifferenceAlpha = (difference - DifferenceMediumLow0) / (DifferenceMediumHigh0 - DifferenceMediumLow0);
            }
            else if (difference < DifferenceMediumLow1 && difference > DifferenceMediumHigh1)
            {
                _mediumDifferenceAlpha = (difference - DifferenceMediumLow1) / (DifferenceMediumHigh1 - DifferenceMediumLow1);
            }
        }

        private void CalculateDifferenceSmall(double difference)
        {
            Debug.Assert(DifferenceSmall1 <= DifferenceSmall0);

            if (difference <= DifferenceSmall1)
            {
                _smallDifferenceAlpha = 1;
            }
            else if (difference >= DifferenceSmall0)
            {
                _smallDifferenceAlpha = 0;
            }
            else
            {
                _smallDifferenceAlpha = (difference - DifferenceSmall0) / (DifferenceSmall1 - DifferenceSmall0);
            }
        }

        private double TurnRight(double alpha)
        {
            Debug.Assert(DegreeLarge0 <= DegreeLarge1);

            double left = DegreeLarge0 + (DegreeLarge1 - DegreeLarge0) * alpha;
            double result = (left + 40) / 2;

            return result;
        }

        private double KeepWheel(double alpha)
        {
            Debug.Assert(DegreeMediumLow0 <= DegreeMediumHigh0);
            Debug.Assert(DegreeMediumHigh0 <= DegreeMediumHigh1);
            Debug.Assert(DegreeMediumHigh1 <= DegreeMediumLow1);

            double left = DegreeMediumLow0 + (DegreeMediumHigh0 - DegreeMediumLow0) * alpha;
            double right = DegreeMediumLow1 - (DegreeMediumLow1 - DegreeMediumHigh1) * alpha;
            double result = (left + right) / 2;

            return result;
        }

        private double TurnLeft(double alpha)
        {
            Debug.Assert(DegreeSmall1 <= DegreeSmall0);

            double right = DegreeSmall0 - (DegreeSmall0 - DegreeSmall1) * alpha;
            double result = (right - 40) / 2;

            return result;
        }
    }
}
