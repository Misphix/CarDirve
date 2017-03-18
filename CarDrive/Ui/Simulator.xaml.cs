using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CarDrive.Controller;
using CarDrive.Recorder;

namespace CarDrive.Ui
{
    /// <summary>
    /// Simulator.xaml 的互動邏輯
    /// </summary>
    partial class Simulator
    {
        private Map _map;
        private delegate void Refresh();
        private readonly Refresh _refresh;
        private double StrokeWidth => 2 / Map.CanvasTransform;
        private double CanvasWidth => MapField.ActualWidth;
        private double CanvasHeight => MapField.ActualHeight;
        private double _obstacleWidth, _obstacleHeight;

        private readonly Controller.Controller _controller;

        private Map Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
                _controller.Car.Obstacles = Map.Obstacles;
                _controller.Car.Center = Map.StartPoint;
                SetObstacleWidth();
                SetbstaclesHeight();
            }
        }

        public Simulator()
        {
            InitializeComponent();
            _refresh = ClearMapField;
            _refresh += DrawMap;
            _refresh += DrawCar;
            _refresh += DrawPath;
            _refresh += UpdateConsole;
            _controller = new HumanController(Render);
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
                    Points = TranslateCoordinates(line.Points),
                    Stroke = Brushes.SlateGray,
                    StrokeThickness = StrokeWidth,
                    FillRule = FillRule.EvenOdd
                };
                MapField.Children.Add(newLine);
            }
        }

        private void DrawCar()
        {
            Ellipse ellipse = new Ellipse
            {
                Height = _controller.Car.Radius * 2,
                Width = _controller.Car.Radius * 2,
                Stroke = Brushes.SlateGray,
                StrokeThickness = StrokeWidth
            };
            Point center = TranslateCoordinate(_controller.Car.Center);
            Canvas.SetLeft(ellipse, center.X - _controller.Car.Radius);
            Canvas.SetTop(ellipse, center.Y - _controller.Car.Radius);
            MapField.Children.Add(ellipse);

            Line direction = new Line
            {
                X1 = center.X,
                Y1 = center.Y,
                X2 = center.X + _controller.Car.Radius * Math.Cos(_controller.Car.FaceRadian),
                Y2 = center.Y - _controller.Car.Radius * Math.Sin(_controller.Car.FaceRadian),
                Stroke = Brushes.SlateGray,
                StrokeThickness = StrokeWidth
            };
            MapField.Children.Add(direction);
        }

        private void DrawPath()
        {
            Polyline line = new Polyline
            {
                Stroke = Brushes.Red,
                StrokeThickness = StrokeWidth,
                FillRule = FillRule.EvenOdd
            };

            foreach (Record record in _controller.Recorder.Records)
            {
                line.Points.Add(TranslateCoordinate(record.Position));
            }

            MapField.Children.Add(line);
        }

        private void UpdateConsole()
        {
            DashBoard.Console.Text = _controller.Recorder.ToString();
        }

        public void Start(double speed)
        {
            _controller.Start(speed);
        }

        public void Pause()
        {
            _controller.Pause();
        }

        public void Stop()
        {
            _controller.Stop();
        }

        public void ChangeMap(Map map)
        {
            Map = map;
            Render();
        }

        private void Render()
        {
            Dispatcher.Invoke(_refresh);
        }

        private PointCollection TranslateCoordinates(PointCollection pointCollection)
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

        private void Simulator_OnLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            Debug.Assert(window != null);
            window.KeyDown += _controller.HandlerKeyPress;
        }
    }
}
