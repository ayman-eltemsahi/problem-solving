using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam {
    class QP4 {
        const double A45 = 45 * Math.PI / 180.0;
        const double A135 = 135 * Math.PI / 180.0;
        const double A225 = 225 * Math.PI / 180.0;
        const double A315 = 315 * Math.PI / 180.0;
        const double A270 = 270 * Math.PI / 180.0;
        const double ERROR = 0.000000001;
        static double SIDE = Math.Sqrt(3.0 / 4.0);// 0.5 / Math.Cos(A45);
        static StringBuilder sb = new StringBuilder();
        static void Main2() {

            double x = 0.5, y = 0.5, z = 0.5;
            var radius = Math.Sqrt(x * x + y * y + z * z);
            var theta = Math.Acos(z / radius);
            var phi = Math.Atan2(y, x);

            //theta = theta * 180.0 / Math.PI;
            //phi = phi * 180.0 / Math.PI;
            var ang1 = new double[]
            {
                A45, A135, A225, A315, A45, A135, A225, A315
            };
            var ang2 = new double[]
            {
                theta, theta, theta, theta, -theta, -theta,-theta, -theta
            };

            var ps2 = new List<Point>();
            foreach (var item in Enumerable.Zip(ang1, ang2, (a, b) => Tuple.Create(a, b))) {
                ps2.Add(Point.FromSideAndAngle3(SIDE, item.Item2, item.Item1));
            }

            foreach (var item in ps2) {
                Console.WriteLine(item);
            }
            return;

            var ps = new Point[]
            {
                new Point(0.5,0.5,0.5),
                new Point(0.5,-0.5,0.5),
                new Point(-0.5,0.5,0.5),
                new Point(-0.5,-0.5,0.5),
                new Point(0.5,0.5,-0.5),
                new Point(0.5,-0.5,-0.5),
                new Point(-0.5,0.5,-0.5),
                new Point(-0.5,-0.5,-0.5)
            };

            Console.SetIn(new System.IO.StreamReader("input"));
            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++) {
                sb.AppendLine($"Case #{i}:");
                solve();
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static void solve() {
            double A = double.Parse(Console.ReadLine());

            double lo = 0, hi = A45, mid = 0;
            double cA = 1;
            Point p1 = new Point(0.5, 0.5);
            Point p2 = p1.Opposite();
            Point p3 = new Point(0.5, -0.5);
            Point p4 = p3.Opposite();

            while (Math.Abs(A - cA) > ERROR) {
                mid = 0.5 * (lo + hi);

                double x = SIDE * Math.Cos(mid);
                double y = SIDE * Math.Sin(mid);

                p1 = Point.FromSideAndAngle(SIDE, mid);
                p2 = p1.Opposite();

                p3 = Point.FromSideAndAngle(SIDE, A270 + mid);
                p4 = p3.Opposite();

                cA = Math.Abs(p1.X - p2.X);
                if (cA < A) hi = mid;
                else lo = mid;
            }

            Point c1 = Point.Middle(p1, p3);
            Point c2 = Point.Middle(p1, p4);
            sb.AppendLine(c1.X + " " + c1.Y + " " + 0);
            sb.AppendLine(c2.X + " " + c2.Y + " " + 0);
            sb.AppendLine("0 0 0.5");
        }

    }

    class Point {
        public double X, Y, Z;
        public Point(double x = 0, double y = 0, double z = 0) { X = x; Y = y; Z = z; }
        internal static Point FromSideAndAngle(double side, double angle) {
            double x = side * Math.Cos(angle);
            double y = side * Math.Sin(angle);
            return new Point(x, y);
        }

        internal static Point Middle(Point p1, Point p2) {
            return new Point((p1.X + p2.X) / 2.0, (p1.Y + p2.Y) / 2.0);
        }

        internal static Point FromSideAndAngle3(double side, double a1, double a2) {
            double x = side * Math.Sin(a1) * Math.Cos(a2);
            double y = side * Math.Sin(a1) * Math.Sin(a2);
            double z = side * Math.Cos(a1);
            return new Point(x, y, z);
        }

        internal Point Opposite() {
            return new Point(-X, -Y);
        }
        public override string ToString() {
            return $"({X}, {Y}, {Z})";
        }
    }
}
