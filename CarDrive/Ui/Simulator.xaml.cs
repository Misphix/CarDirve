using System;
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
    partial class Simulator : UserControl
    {
        private Map _map;
        private Car _car;
        private delegate void Refresh();
        private Refresh refresh;
        private double StrokeWidth => 2 / Map.CanvasTransform;
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
                _car.Center = Map.StartPoint;
                SetObstacleWidth();
                SetbstaclesHeight();
            }
        }

        public Simulator()
        {
            InitializeComponent();
            _car = new Car();
            refresh = ClearMapField;
            refresh += DrawMap;
            refresh += DrawCar;
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
                    StrokeThickness = StrokeWidth,
                    FillRule = FillRule.EvenOdd
                };
                MapField.Children.Add(newLine);
            }
        }

        private void DrawCar()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = _car.Radius * 2;
            ellipse.Width = _car.Radius * 2;
            ellipse.Stroke = Brushes.SlateGray;
            ellipse.StrokeThickness = StrokeWidth;
            Point center = TranslateCoordinate(_car.Center);
            Canvas.SetLeft(ellipse, center.X - _car.Radius);
            Canvas.SetTop(ellipse, center.Y - _car.Radius);
            MapField.Children.Add(ellipse);

            Line direction = new Line();
            direction.X1 = center.X;
            direction.Y1 = center.Y;
            direction.X2 = center.X + _car.Radius * Math.Cos(_car.FaceRadian);
            direction.Y2 = center.Y - _car.Radius * Math.Sin(_car.FaceRadian);
            direction.Stroke = Brushes.SlateGray;
            direction.StrokeThickness = StrokeWidth;
            MapField.Children.Add(direction);
        }

        public void ChangeMap(Map map)
        {
            Map = map;
            Render();
        }

        private void Render()
        {
            Dispatcher.Invoke(refresh);
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
