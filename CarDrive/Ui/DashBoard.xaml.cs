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
        public ObservableCollection<Algorithm> Algorithms { get; private set; }
        internal Controller.Controller Controller
        {
            private get { return _controller; }
            set
            {
                _controller = value;
                foreach (Algorithm algorithm in value.Algorithms)
                {
                    Algorithms.Add(algorithm);
                }
            }
        }
        public Algorithm SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                _selectedAlgorithm = value;
                Controller.SelectedAlgorithm = value;
            }
        }
        private Controller.Controller _controller;
        private Algorithm _selectedAlgorithm;

        public DashBoard()
        {
            Algorithms = new ObservableCollection<Algorithm>();
            InitializeComponent();
        }

        private void OnContentChanged(object sender, TextChangedEventArgs e)
        {
            Console.ScrollToEnd();
        }
    }
}
