using GeneticAlgorithm.Algorithm;
using GeneticAlgorithm.Info;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GeneticAlgorithm
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string path = "../../Data";
        private DataReader _dataReader;
        private bool _canExecuted = true;

        public MainWindow()
        {
            InitializeComponent();
            _dataReader = new DataReader(path);
        }

        private void CheckTextBox(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (IsTextInteger(textBox.Text))
            {
                textBox.Background = Brushes.White;
            }
            else
            {
                textBox.Background = Brushes.Red;
            }
        }

        private bool IsTextInteger(string text)
        {
            Regex regex = new Regex("[^0-9.-]");
            return !regex.IsMatch(text);
        }

        private void PlayCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _canExecuted = false;
            int population = int.Parse(Population.Text);
            int tolerance = int.Parse(Accept.Text);
            int maxIteration = int.Parse(MaxIteration.Text);
            double mutation = double.Parse(Mutation.Text);
            double crossover = double.Parse(Crossover.Text);
            int neural = int.Parse(Neural.Text);

            Genetic genetic = new Genetic(population, maxIteration, tolerance, mutation, crossover)
            {
                Train4D = _dataReader.Data4D,
                Train6D = _dataReader.Data6D
            };
            Console.Text = genetic.Start(neural).ToString();
            _canExecuted = true;
        }

        private void PlayCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _canExecuted;
        }
    }
}
