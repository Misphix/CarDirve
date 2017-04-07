namespace CarDrive.Algorithms
{
    public interface IAlgorithm
    {
        string Name { get; }
        /// <summary>
        /// Get the steering wheel's degree
        /// </summary>
        /// <param name="forward">The distnace of forward.</param>
        /// <param name="left">The distance of left sensor.</param>
        /// <param name="right">The distance of right sensor.</param>
        /// <returns>The degree steering wheel should be.</returns>
        double GetDegree(double forward, double left, double right);
    }
}
