using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CarDrive.Controller
{
    class HumanController : IController
    {
        private readonly Car _car;
        private double _degree = 0, _speed = 20;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private const double Move = 5;
        private const uint Interval = 2000;

        public HumanController(Car car)
        {
            _car = car;
        }

        public void Start(double speed)
        {
            _speed = speed;
            _degree = 0;
            
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
            _car.Reset();
            _degree = 0;
        }

        private void MoveCar(object sender, DoWorkEventArgs e)
        {
            while (!_backgroundWorker.CancellationPending)
            {
                Thread.Sleep((int)(Interval / _speed));
                _car.Move(_degree);
                _degree /= 2;
            }
        }

        public void HandlerKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    _degree = Math.Abs(_degree - Move) > 40 ? -40 : _degree - Move;
                    break;
                case Key.Right:
                    _degree = Math.Abs(_degree + Move) > 40 ? 40 : _degree + Move;
                    break;
            }
        }
    }
}
