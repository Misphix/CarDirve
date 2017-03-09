using System.Collections.ObjectModel;
using System.Windows.Controls;
using CarDrive.Algorithms;

namespace CarDrive
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class DashBoard : UserControl
    {
        public ObservableCollection<Algorithm> Algorithms { get; private set; }
        public Algorithm SelectedAlgorithm { get; set; }

        public DashBoard()
        {
            InitializeList();
            InitializeComponent();
            Console.Text = "a\r\nb\r\nc\r\nd\r\nb\r\nc\r\nd";
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
    }
}
