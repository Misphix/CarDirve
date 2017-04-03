namespace CarDrive.Algorithms
{
    public interface Algorithm
    {
        string Name { get; }
        /// <summary>
        /// Get the steering wheel's degree
        /// </summary>
        /// <param name="forward">The distnace of forward.</param>
        /// <param name="difference">The value of (right distance) - (left distance).</param>
        /// <returns></returns>
        double GetDegree(double forward, double difference);

        double GetDegree(double forward, double left, double right);
    }
}
