using System;
using System.Windows.Input;

namespace CarDrive.Controller
{
    class HumanController : Controller
    {
        public HumanController(Car.Redraw redraw) : base(redraw) { }

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
