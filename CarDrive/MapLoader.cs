using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Shapes;

namespace CarDrive
{
    public class MapLoader
    {
        public Map LoadMap(string file)
        {
            Debug.Assert(File.Exists(file));

            string[] content = File.ReadAllLines(file);

            Map map = new Map(System.IO.Path.GetFileNameWithoutExtension(file));
            var result = ContentParser(content);
            map.StartPoint = result.startPoint;
            map.Obstacles = result.obstacles;

            return map;
        }

        private (Point startPoint, List<Polyline> obstacles) ContentParser(string[] content)
        {
            List<Polyline> shapes = new List<Polyline>();
            Point startPoint = new Point();

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
                    default:
                        throw new InvalidDataException();
                }
            }

            return (startPoint, shapes);
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
                point.X = Convert.ToDouble(data.Split(',')[0]);
                point.Y = Convert.ToDouble(data.Split(',')[1]);
            }
            catch (IndexOutOfRangeException e)
            {
                throw new InvalidDataException(e.Message);
            }

            return point;
        }
    }
}
