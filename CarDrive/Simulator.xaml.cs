using System.Collections.Generic;
using System.Windows;
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
        public Simulator()
        {
            InitializeComponent();
            Polyline myLine = new Polyline();
            myLine.Stroke = System.Windows.Media.Brushes.SlateGray;
            myLine.StrokeThickness = 2;
            myLine.FillRule = FillRule.EvenOdd;
            PointCollection points = new PointCollection(
                new List<Point> { 
                    new Point(-6, 0), new Point(-6, 22), new Point(18, 22), new Point(24, 37)});
            myLine.Points = points;
            MapField.Children.Add(myLine);
        }
    }
}
