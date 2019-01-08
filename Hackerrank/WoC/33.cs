using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text;
using System.IO;

class Week_Of_Code_31
{
    static void Main2(String[] args) {
        Console.SetIn(new System.IO.StreamReader("input"));
        var tmp = Console.ReadLine().Split(' ');
        int px = int.Parse(tmp[0]);
        int py = int.Parse(tmp[1]);

        tmp = Console.ReadLine().Split(' ');
        int fx = int.Parse(tmp[0]);
        int fy = int.Parse(tmp[1]);

        tmp = Console.ReadLine().Split(' ');
        int rows = int.Parse(tmp[0]);
        int cols = int.Parse(tmp[1]);

        Point[,] grid = new Point[rows, cols];
        for (int i = 0; i < rows; i++) {
            var tmpp = Console.ReadLine();
            for (int j = 0; j < cols; j++) {
                grid[i, j] = new Point(i, j);
                if (tmpp[j] == '%') grid[i, j].IsObstacle = true;
            }
        }

        Queue<Point> Q = new Queue<Point>();
        Q.Enqueue(grid[px, py]);
        grid[px, py].d = 0;
        Point food = grid[fx, fy];

        List<Point> expanded = new List<Point> { grid[px, px] };
        while (Q.Count > 0 && food.d == int.MaxValue) {
            Point cur = Q.Dequeue();
            int d = cur.d + 1;

            foreach (var neighbour in neighbours(grid, rows, cols, cur.X, cur.Y)) {
                if (neighbour.d > d) {
                    neighbour.d = d;
                    neighbour.cameFrom = cur;
                    if (!expanded.Contains(neighbour)) {
                        Q.Enqueue(neighbour);
                        expanded.Add(neighbour);
                    }
                    if (neighbour == food) break;
                }
            }
        }


        Console.WriteLine(expanded.Count);
        Console.WriteLine(String.Join("\n", expanded.Select(x => x.X + " " + x.Y)));

        List<Point> path = new List<Point>();
        path.Add(food);
        while (food.cameFrom != null && food.cameFrom != grid[px, py]) {
            food = food.cameFrom;
            path.Add(food);
        }
        path.Add(food.cameFrom);

        path.Reverse();
        Console.WriteLine(path.Count - 1);
        Console.WriteLine(String.Join("\n", path.Select(x => x.X + " " + x.Y)));
    }

    static IEnumerable<Point> neighbours(Point[,] grid, int rows, int cols, int x, int y) {
        if (x > 0 && !grid[x - 1, y].IsObstacle) yield return grid[x - 1, y];
        if (x + 1 < rows && !grid[x + 1, y].IsObstacle) yield return grid[x + 1, y];

        if (y > 0 && !grid[x, y - 1].IsObstacle) yield return grid[x, y - 1];
        if (y + 1 < cols && !grid[x, y + 1].IsObstacle) yield return grid[x, y + 1];

    }

    static int manhattan(Point a, Point b) {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    class Point
    {
        public int X, Y, d;
        public bool IsObstacle;
        public Point cameFrom;

        public Point(int x = 0, int y = 0) { X = x; Y = y; d = int.MaxValue; }
    }
}
