using System.Collections.ObjectModel;
using System.Windows.Controls;
using CarDrive.Algorithms;

namespace CarDrive.Ui
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class DashBoard
    {
        public ObservableCollection<IAlgorithm> Algorithms { get; private set; }
        internal Controller.Controller Controller
        {
            private get { return _controller; }
            set
            {
                _controller = value;
                foreach (IAlgorithm algorithm in value.Algorithms)
                {
                    Algorithms.Add(algorithm);
                }
            }
        }
        public IAlgorithm SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                _selectedAlgorithm = value;
                Controller.SelectedAlgorithm = value;
            }
        }
        private Controller.Controller _controller;
        private IAlgorithm _selectedAlgorithm;

        public DashBoard()
        {
            Algorithms = new ObservableCollection<IAlgorithm>();
            InitializeComponent();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Console.ScrollToEnd();
        }
    }
}
