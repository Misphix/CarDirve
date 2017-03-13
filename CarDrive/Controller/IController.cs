using System.Windows.Input;

namespace CarDrive.Controller
{
    interface IController
    {
        void Start();
        void HandlerKeyPress(object sender, KeyEventArgs e);
    }
}
