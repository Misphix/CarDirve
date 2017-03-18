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
        public Algorithm SelectedAlgorithm { get; set; }

        public DashBoard()
        {
            InitializeList();
            InitializeComponent();
        }

        private void InitializeList()
        {
            Algorithms = new ObservableCollection<Algorithm>();
            Algorithms.Add(new NormalAlgorithm("a"));
            var b = new NormalAlgorithm("b");
            Algorithms.Add(b);
            Algorithms.Add(new NormalAlgorithm("c"));
            SelectedAlgorithm = b;
        }

        private void OnContentChanged(object sender, TextChangedEventArgs e)
        {
            Console.ScrollToEnd();
        }
    }
}
