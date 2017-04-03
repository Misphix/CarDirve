using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CarDrive.Algorithms;

namespace CarDrive.Controller
{
    abstract class Controller
    {
        public readonly ObservableCollection<Algorithm> Algorithms;
        public Algorithm SelectedAlgorithm { get; set; }
        internal readonly Car Car;
        internal readonly Recorder.Recorder Recorder;
        protected readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        protected double Degree;
        protected double Speed;
        protected const double Move = 5;
        protected const uint Interval = 2000;

        protected Controller(Car.Redraw redraw)
        {
            Car = new Car(redraw);
            Recorder = new Recorder.Recorder();
            Algorithms = new ObservableCollection<Algorithm>
            {
                new NormalAlgorithm(),
                new Algorithms.GeneticAlgorithm()
            };
        }

        public void Start(double speed)
        {
            Speed = speed;
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

        public virtual void HandlerKeyPress(object sender, KeyEventArgs e) { }

        protected abstract void MoveCar(object sender, DoWorkEventArgs e);
    }
}
