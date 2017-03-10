using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CarDrive.Ui
{
    /// <summary>
    /// Simulator.xaml 的互動邏輯
    /// </summary>
    public partial class Simulator : UserControl
    {
        private Map _map;
        private delegate void Refresh();
        private double CanvasWidth => MapField.ActualWidth;
        private double CanvasHeight => MapField.ActualHeight;
        private double _obstacleWidth, _obstacleHeight;

        private Map Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
                SetObstacleWidth();
                SetbstaclesHeight();
            }
        }

        public Simulator()
        {
            InitializeComponent();
        }

        public void ChangeMap(Map map)
        {
            Map = map;
            Render();
        }

        private void Render()
        {
            Refresh refresh = ClearMapField;
            refresh += DrawMap;
            Dispatcher.Invoke(refresh);
        }

        private void ClearMapField()
        {
            MapField.Children.Clear();
        }

        private void DrawMap()
        {
            Debug.Assert(Map != null);

            MapField.LayoutTransform = new ScaleTransform(Map.CanvasTransform, Map.CanvasTransform);
            MapField.UpdateLayout();
            foreach (Polyline line in Map.Obstacles)
            {
                Polyline newLine = new Polyline
                {
                    Points = TranslateCoordiantes(line.Points),
                    Stroke = Brushes.SlateGray,
                    StrokeThickness = 1,
                    FillRule = FillRule.EvenOdd
                };
                MapField.Children.Add(newLine);
            }
        }

        private PointCollection TranslateCoordiantes(PointCollection pointCollection)
        {
            PointCollection result = new PointCollection();

            foreach (Point point in pointCollection)
            {
                result.Add(TranslateCoordinate(point));
            }

            return result;
        }

        private Point TranslateCoordinate(Point point)
        {
            double shiftX = (CanvasWidth - _obstacleWidth) / 2, shiftY = (CanvasHeight - _obstacleHeight) / 2;
            Point result = new Point
            {
                X = point.X + shiftX,
                Y = CanvasHeight - point.Y - shiftY
            };

            return result;
        }

        private void SetObstacleWidth()
        {
            double minX = double.MaxValue, maxX = double.MinValue;

            foreach (Polyline mapObstacle in Map.Obstacles)
            {
                foreach (Point point in mapObstacle.Points)
                {
                    minX = (point.X < minX) ? point.X : minX;
                    maxX = (point.X > maxX) ? point.X : maxX;
                }
            }

            _obstacleWidth = maxX - minX;
        }

        private void SetbstaclesHeight()
        {
            double minY = double.MaxValue, maxY = double.MinValue;

            foreach (Polyline mapObstacle in Map.Obstacles)
            {
                foreach (Point point in mapObstacle.Points)
                {
                    minY = (point.X < minY) ? point.X : minY;
                    maxY = (point.X > maxY) ? point.X : maxY;
                }
            }

            _obstacleHeight = maxY - minY;
        }

        private void MapField_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Map != null)
            {
                Render();
            }
        }
    }
}
