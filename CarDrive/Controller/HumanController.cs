using System.Windows.Input;

namespace CarDrive.Controller
{
    class HumanController : IController
    {
        private readonly Car _car;
        private double degree = 0;
        private const double Move = 0.01;

        public HumanController(Car car)
        {
            _car = car;
        }

        public void Start()
        {
            _car.Move(40);
        }

        public void HandlerKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    degree += Move;
                    break;
                case Key.Right:
                    degree -= Move;
                    break;
            }
        }
    }
}
