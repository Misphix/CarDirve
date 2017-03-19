using CarDrive.Recorder;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace CarDrive.Controller
{
    internal class HumanController : Controller
    {
        public HumanController(Car.Redraw redraw) : base(redraw) { }

        protected override void MoveCar(object sender, DoWorkEventArgs e)
        {
            while (!_backgroundWorker.CancellationPending)
            {
                Thread.Sleep((int)(Interval / Speed));

                Record record = new Record(Car.Center, Car.Left, Car.Forward, Car.Right, Degree);
                Recorder.Add(record);
                Car.Move(Degree);

                Degree /= 2;
            }
        }

        public override void HandlerKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    Degree = Math.Abs(Degree - Move) > 40 ? -40 : Degree - Move;
                    break;
                case Key.Right:
                    Degree = Math.Abs(Degree + Move) > 40 ? 40 : Degree + Move;
                    break;
            }
        }
    }
}
