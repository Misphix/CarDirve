using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Shapes;

namespace CarDrive
{
    class MapLoader
    {
        public Map LoadMap(string file)
        {
            Debug.Assert(File.Exists(file));

            string[] content = File.ReadAllLines(file);

            Map map = new Map(System.IO.Path.GetFileNameWithoutExtension(file));
            var result = ContentParser(content);
            map.StartPoint = result.startPoint;
            map.Obstacles = result.obstacles;
            map.CanvasTransform = result.canvasTransform;

            return map;
        }

        private (Point startPoint, List<Polyline> obstacles, double canvasTransform) ContentParser(string[] content)
        {
            List<Polyline> shapes = new List<Polyline>();
            Point startPoint = new Point();
            double canvasTransform = 1;

            foreach (string line in content)
            {
                string type = line.Trim().Split(':')[0];
                string data = line.Trim().Split(':')[1];

                switch (type)
                {
                    case "StarPoint":
                        startPoint = ParseStartPoint(data);
                        break;
                    case "Wall":
                        shapes.Add(ParseWall(data));
                        break;
                    case "CanvasTransform":
                        canvasTransform = Convert.ToDouble(data.Trim());
                        break;
                    default:
                        throw new InvalidDataException();
                }
            }

            return (startPoint, shapes, canvasTransform);
        }

        private Point ParseStartPoint(string data)
        {
            Point start = ParsePoint(data);

            return start;
        }

        private Polyline ParseWall(string data)
        {
            Polyline line = new Polyline();
            string[] points = data.Trim().Split(';');

            foreach (string point in points)
            {
                line.Points.Add(ParsePoint(point));
            }

            return line;
        }

        private Point ParsePoint(string data)
        {
            Point point = new Point();
            try
            {
                data = Regex.Replace(data.Trim(), @"\(|\)", "");
                point = Point.Parse(data);
            }
            catch (FormatException e)
            {
                throw new InvalidDataException(e.Message);
            }

            return point;
        }
    }
}
