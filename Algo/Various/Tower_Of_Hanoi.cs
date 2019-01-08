using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Various
{
    public static class Tower_Of_Hanoi
    {
        public static void TowerOfHanoi(int n, List<int> source, List<int> target, List<int> aux) {
            if (n > 0) {
                TowerOfHanoi(n - 1, source, aux, target);
                MoveDisk(source, target);
                TowerOfHanoi(n - 1, aux, target, source);
            }
        }

        public static void TowerOfHanoi(int n, List<int> source, List<int> target, List<int> aux, List<int> aux2) {
            if (n == 1) {
                MoveDisk(source, target);
            } else if (n == 2) {
                MoveDisk(source, aux);

                MoveDisk(source, target);

                MoveDisk(aux, target);

            } else {

                TowerOfHanoi(n - 2, source, aux, aux2, target);

                MoveDisk(source, aux2);
                MoveDisk(source, target);
                MoveDisk(aux2, target);

                TowerOfHanoi(n - 2, aux, target, source, aux2);
            }
        }

        private static void MoveDisk(List<int> source, List<int> target, bool displayProgress = true) {
            moves_hanoi++;
            target.Add(source[source.Count - 1]);
            source.RemoveAt(source.Count - 1);

            if (displayProgress) {
                Console.WriteLine("A: " + string.Join(" ", A_hanoi));
                Console.WriteLine("B: " + string.Join(" ", B_hanoi));
                Console.WriteLine("C: " + string.Join(" ", C_hanoi));
                Console.WriteLine("D: " + string.Join(" ", D_hanoi));
                Console.ReadLine();
            }
        }

        static int moves_hanoi = 0;
        static List<int> A_hanoi = new List<int>();
        static List<int> B_hanoi = new List<int>();
        static List<int> C_hanoi = new List<int>();
        static List<int> D_hanoi = new List<int>();
        public static void doHanoi(int n = 10) {
            for (int i = n; i > 0; i--) {
                A_hanoi.Add(i);
            }

            moves_hanoi = 0;
            TowerOfHanoi(n, A_hanoi, D_hanoi, B_hanoi, C_hanoi);
            Console.WriteLine(moves_hanoi + " move" + (moves_hanoi != 1 ? "s" : ""));
        }

    }
}
