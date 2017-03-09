using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CarDrive
{
    /// <summary>
    /// Simulator.xaml 的互動邏輯
    /// </summary>
    public partial class Simulator : UserControl
    {
        private Map _map;

        public Simulator()
        {
            InitializeComponent();
        }

        public void ChangeMap(Map map)
        {
            _map = map;
            DrawMap();
        }

        private void DrawMap()
        {
            Debug.Assert(_map != null);

            foreach (Polyline line in _map.Obstacles)
            {
                Polyline newLine = new Polyline();
                newLine.Points = line.Points;
                newLine.Stroke = Brushes.SlateGray;
                newLine.StrokeThickness = 2;
                newLine.FillRule = FillRule.EvenOdd;
                MapField.Children.Add(newLine);
            }
        }
    }
}
