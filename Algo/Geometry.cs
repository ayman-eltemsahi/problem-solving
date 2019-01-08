using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    class Geometry
    {
        static double Area(Point p1, Point p2, Point p3)
        {
            double g = (p2.X - p1.X) * (p3.Y - p1.Y) + (p3.X - p1.X) * (p2.Y - p1.Y);
            return 0.5 * Math.Abs(g);
        }
        static double Area(Point p1, Point p2, Point p3, Point p4)
        {
            return ((p1.X * p2.Y - p2.X * p1.Y) + (p2.X * p3.Y - p3.X * p2.Y) + (p3.X * p4.Y - p4.X * p3.Y) + (p4.X * p1.Y - p1.X * p4.Y)) / 2.0;
        }
    }
    class Point
    {
        public double X, Y;
        public Point(double x = 0, double y = 0) { X = x; Y = y; }

        internal static Point FromSideAndAngle(double side, double angle)
        {
            double x = side * Math.Cos(angle);
            double y = side * Math.Sin(angle);
            return new Point(x, y);
        }

        internal static Point Middle(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2.0, (p1.Y + p2.Y) / 2.0);
        }

        public override String ToString()
        {
            return String.Format("({0}, {1})", X, Y);
        }

        internal Point Opposite()
        {
            return new Point(-X, -Y);
        }
    }
}
