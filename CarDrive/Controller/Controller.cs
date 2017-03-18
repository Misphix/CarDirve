using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CarDrive.Controller
{
    abstract class Controller
    {
        public readonly Car Car;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        protected double Degree;
        private double _speed;
        protected const double Move = 5;
        private const uint Interval = 2000;

        protected Controller(Car.Redraw redraw)
        {
            Car = new Car(redraw);
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
            Car.Reset();
            Degree = 0;
        }

        private void MoveCar(object sender, DoWorkEventArgs e)
        {
            while (!_backgroundWorker.CancellationPending)
            {
                Thread.Sleep((int)(Interval / _speed));
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Car.Move(Degree);
                });
                Degree /= 2;
            }
        }

        public virtual void HandlerKeyPress(object sender, KeyEventArgs e) { }  
    }
}
