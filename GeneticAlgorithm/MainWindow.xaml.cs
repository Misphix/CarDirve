using GeneticAlgorithm.Info;
using System.Windows;

namespace GeneticAlgorithm
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string path = "../../Data";
        private DataReader _dataReader;

        public MainWindow()
        {
            InitializeComponent();
            _dataReader = new DataReader(path);
        }
    }
}
