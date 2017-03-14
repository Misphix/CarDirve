using System.Windows.Input;

namespace CarDrive.Controller
{
    interface IController
    {
        void Start(double speed);
        void Pause();
        void Stop();
        void HandlerKeyPress(object sender, KeyEventArgs e);
    }
}
