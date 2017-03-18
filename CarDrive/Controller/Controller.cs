using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using CarDrive.Recorder;

namespace CarDrive.Controller
{
    abstract class Controller
    {
        public readonly Car Car;
        public readonly Recorder.Recorder Recorder;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        protected double Degree;
        private double _speed;
        protected const double Move = 5;
        private const uint Interval = 2000;

        protected Controller(Car.Redraw redraw)
        {
            Car = new Car(redraw);
            Recorder = new Recorder.Recorder();
        }

        public void Start(double speed)
        {
            _speed = speed;
            Degree = 0;

            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += MoveCar;
            _backgroundWorker.RunWorkerAsync();
        }

        public void Pause()
        {
            _backgroundWorker.CancelAsync();
        }

        public void Stop()
        {
            _backgroundWorker.CancelAsync();
            Degree = 0;
            Recorder.Clear();
            Car.Reset();
        }

        private void MoveCar(object sender, DoWorkEventArgs e)
        {
            while (!_backgroundWorker.CancellationPending)
            {
                Thread.Sleep((int)(Interval / _speed));

                Record record = new Record(Car.Center, Car.Left, Car.Forward, Car.Right, Car.FaceAngle);
                Recorder.Add(record);
                Car.Move(Degree);

                Degree /= 2;
            }
        }

        public virtual void HandlerKeyPress(object sender, KeyEventArgs e) { }  
    }
}
