namespace CarDrive.Algorithms
{
    public interface Algorithm
    {
        string Name { get; }
        /// <summary>
        /// Get the steering wheel's degree
        /// </summary>
        /// <param name="forward">The distnace of forward.</param>
        /// <param name="difference">The value of (left distance) - (right distance).</param>
        /// <returns></returns>
        double GetDegree(double forward, double difference);
    }
}
