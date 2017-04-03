using CarDrive.Recorder;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using static CarDrive.MainWindow;

namespace CarDrive.Controller
{
    class AlgorithController : Controller
    {
        public AlgorithController(Car.Redraw redraw) : base(redraw) { }

        protected override void MoveCar(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!_backgroundWorker.CancellationPending)
                {
                    Thread.Sleep((int)(Interval / Speed));

                    Record record = new Record(Car.Center, Car.Left, Car.Forward, Car.Right, Degree);
                    Recorder.Add(record);
                    Degree = SelectedAlgorithm.GetDegree(Car.Forward, Car.Left, Car.Right);
                    Car.Move(Degree);
                }
            }
            catch(NullReferenceException)
            {
                _backgroundWorker.CancelAsync();
                MessageBox.Show("Please specify the alogrithm");
                _status = Status.Stop;
            }
        }
    }
}
