using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank
{
    class Zalando_CodeSprint {
        #region ___Which_Warehouses_Can_Fulfill_These_Orders
        public static void Which_Warehouses_Can_Fulfill_These_Orders() {
            var tmp = Console.ReadLine().Split(' ');
            int w = int.Parse(tmp[0]);  // warehouses
            int b = int.Parse(tmp[1]);  // customer orders
            int p = int.Parse(tmp[2]);  // products

            decimal[][] warehouses = new decimal[w][];
            for (int i = 0; i < w; i++) {
                warehouses[i] = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToDecimal);
            }

            while (b-- > 0) {
                decimal[] orders = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToDecimal);

                // chooose all
                foreach (var ware in warehouses) {
                    addW(orders, ware);
                }
                if (anyPos(orders)) { Console.WriteLine(-1); continue; }

                ex = 0;
                tryExcluding(orders, warehouses, 0, 0);
                Console.WriteLine(w - ex);
            }
        }
        static int ex = 0;
        static void tryExcluding(decimal[] orders, decimal[][] warehouses, int p, int excluded) {
            ex = Math.Max(ex, excluded);

            for (int i = p; i < warehouses.Length; i++) {
                removeW(orders, warehouses[i]);
                if (anyPos(orders)) { addW(orders, warehouses[i]); continue; }
                tryExcluding(orders, warehouses, i + 1, excluded + 1);
                addW(orders, warehouses[i]);
            }
        }

        static void removeW(decimal[] orders, decimal[] ware) {
            for (int i = 0; i < ware.Length; i++) {
                orders[i] += ware[i];
            }
        }

        static void addW(decimal[] orders, decimal[] ware) {
            for (int i = 0; i < ware.Length; i++) {
                orders[i] -= ware[i];
            }
        }

        static bool anyPos(decimal[] orders) {
            for (int i = 0; i < orders.Length; i++) {
                if (orders[i] > 0) return true;
            }
            return false;
        }

        #endregion

        #region ___Match_the_Shoes
        public static void Match_the_Shoes() {
            var tmp = Console.ReadLine().Split(' ');
            int k = int.Parse(tmp[0]);  // The number of most popular shoes you must suggest.
            int m = int.Parse(tmp[1]);  // The number of distinct shoe IDs.
            int n = int.Parse(tmp[2]);  // The number of orders.


            int[] ids = new int[m + 1];
            bool[] taken = new bool[m + 1];
            for (int i = 0; i < n; i++) {
                ids[int.Parse(Console.ReadLine())]++;
            }

            while (k-- > 0) {
                int s = getNext(ids, taken);
                taken[s] = true;
                Console.WriteLine(s);
            }

        }

        static int getNext(int[] ids, bool[] taken) {
            int cur = int.MaxValue;
            int freq = 0;
            for (int i = 1; i < ids.Length; i++) {
                if (taken[i]) continue;
                if (ids[i] > freq) { freq = ids[i]; cur = i; } else if (ids[i] == freq) {
                    cur = Math.Min(cur, i);
                }
            }
            return cur;
        }
        #endregion

        #region ___The_Inquiring_Manager
        public static void The_Inquiring_Manager() {
            int n = int.Parse(Console.ReadLine());
            var T = new List<decimal>();
            var P = new List<decimal>();
            while (n-- > 0) {
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
                if (arr.Length == 2) {
                    // inquiring
                    decimal time = arr[1];
                    decimal val = -1;
                    for (int i = T.Count - 1; i >= 0; i--) {
                        if (time - T[i] < 60) { val = Math.Max(val, P[i]); } else break;
                    }
                    Console.WriteLine(val);
                } else {
                    int index = T.BinarySearch(arr[2]);
                    if (index < 0) {
                        T.Add(arr[2]); P.Add(arr[1]);
                    } else {
                        P[index] = Math.Max(P[index], arr[1]);
                    }
                }
            }
        }
        #endregion

        #region ___Minimal_Wrapping_Surface_Area
        public static void Minimal_Wrapping_Surface_Area() {
            float n = float.Parse(Console.ReadLine());
            var tmp = Console.ReadLine().Split(' ');
            int w = int.Parse(tmp[0]);
            int l = int.Parse(tmp[1]);
            int h = int.Parse(tmp[2]);
            if (n == 1) {
                Console.WriteLine(area(w, h, l));
            } else {
                int surf = int.MaxValue;
                for (float k = 1; k < n; k++) {
                    for (float m = 1; m < n; m++) {
                        if (k * m > n) break;
                        surf = Math.Min(surf, area((int)(k * w), (int)(m * l), (int)(h * Math.Ceiling(n / (k * m)))));
                    }
                }
                Console.WriteLine(surf);
            }
        }
        static int area(int w, int h, int l) {
            return 2 * (w * l + w * h + l * h);
        }
        #endregion

        #region ___Make_Our_Customers_Happy
        public static void Make_Our_Customers_Happy() {
            int[] N = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

            int m = int.Parse(Console.ReadLine()); // number of orders

            int[][] orders = new int[m][];
            bool[] fulfilled = new bool[m];
            for (int i = 0; i < m; i++) {
                orders[i] = Array.ConvertAll(Console.ReadLine().Split(','), x => Convert.ToChar(x) - 65);
                Array.Sort(orders[i]);
            }

            // ones
            for (int i = 0; i < m; i++) {
                if (orders[i].Length == 1 && N[orders[i][0]] > 0) {
                    fulfilled[i] = true;
                    N[orders[i][0]]--;
                }
            }

            int bad = 0;

            int o = 0;
            while (o != -1) {
                o = getNextOrder(N, orders, fulfilled);
                if (o == -1) break;

                if (N[orders[o][0]] <= 0) { fulfilled[o] = true; bad++; continue; }
                N[orders[o][0]]--;

                if (orders[o].Length <= 1) { fulfilled[o] = true; continue; }
                if (N[orders[o][1]] <= 0) { fulfilled[o] = true; bad++; continue; }
                N[orders[o][1]]--;

                if (orders[o].Length == 2) { fulfilled[o] = true; continue; }
                if (N[orders[o][2]] <= 0) { fulfilled[o] = true; bad++; continue; }
                N[orders[o][2]]--;

                fulfilled[o] = true;
            }

            int served = 0;
            for (int i = 0; i < m; i++) {
                if (fulfilled[i]) served++;
            }

            Console.WriteLine(served - bad);
        }

        static int getNextOrder(int[] N, int[][] orders, bool[] fulfilled) {
            int order = -1;
            int val = -1;
            for (int i = 0; i < orders.Length; i++) {
                if (fulfilled[i]) continue;

                foreach (var o in orders[i]) N[o]--;
                int v = ValueOfOrder(N);
                foreach (var o in orders[i]) N[o]++;

                if (v > val) {
                    val = v;
                    order = i;
                }
            }
            return order;
        }
        static int ValueOfOrder(int[] N) {
            if (N[0] < 0 || N[1] < 0 || N[2] < 0) return -100;
            int[] a = new int[3];
            a[0] = N[0]; a[1] = N[1]; a[2] = N[2];
            Array.Sort(a);
            return a[2] + a[1] * 100 + a[0] * 10000;
        }
        #endregion

        #region ___Does_It_Fit
        public static void Does_It_Fit() {
            var tmp = Console.ReadLine().Split(' ');
            int W = int.Parse(tmp[0]);
            int H = int.Parse(tmp[1]);

            int n = int.Parse(Console.ReadLine());

            while (n-- > 0) {
                string[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => x);
                if (arr.Length == 2) {
                    // circle
                    int r = 2 * int.Parse(arr[1]);
                    Console.WriteLine(r <= W && r <= H ? "YES" : "NO");
                } else {
                    int w = int.Parse(arr[1]);
                    int h = int.Parse(arr[2]);
                    if ((w <= W && h <= H) || (w <= H && h <= W) || rot(w, h, W, H)) {
                        Console.WriteLine("YES");
                    } else Console.WriteLine("NO");
                }
            }
        }

        static bool rot(int w, int h, int W, int H) {
            for (double theta = 0; theta < 90; theta += 0.1) {
                double theta2 = 90 - theta;

                double a = h * Math.Cos(Math.PI * theta / 180.0) + w * Math.Cos(Math.PI * theta2 / 180.0);
                double b = h * Math.Cos(Math.PI * theta2 / 180.0) + w * Math.Cos(Math.PI * theta / 180.0);
                if (a <= 0 || b <= 0) continue;
                if ((a <= W && b <= H) || (a <= H && b <= W)) return true;
            }
            return false;
        }
        #endregion

        #region ___Processing_Time_Inside_a_Warehouse
        public static void Processing_Time_Inside_a_Warehouse() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            Array.Sort(arr);
            int time = 0;

            decimal low = 0, high = 10000000000000000000;
            while (low + 1 < high) {
                decimal mid = Math.Floor((low + high) / 2);
                decimal nn = 0;
                for (int i = 0; i < m; i++) {
                    nn += mid / arr[i];
                }
                if (nn < n) low = mid; else high = mid;
            }

            decimal nnn = 0;
            for (int i = 0; i < m; i++) {
                nnn += Math.Floor(low / arr[i]);
            }
            if (nnn >= n) Console.WriteLine(low);
            else {
                Console.WriteLine(high);
            }


            while (n > 0) {
                time++;
                for (int i = 0; i < m; i++) {
                    if (arr[i] > time) break;
                    if (time % arr[i] == 0) n--;
                }
            }
            Console.WriteLine(time);
        }
        #endregion

        #region ___Give_Me_the_Order
        public static void Give_Me_the_Order() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            if (n == 1) { Console.WriteLine(arr[0]); return; }
            Array.Reverse(arr);
            var list = new List<int>(arr);
            int q = int.Parse(Console.ReadLine());
            while (q-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int l = n - int.Parse(tmp[1]);
                int r =  n - int.Parse(tmp[0])-l+1;

                
                list.AddRange(list.GetRange(l, r ));
                list.RemoveRange(l, r );
            }
            for (int i = 0; i < n; i++) {
                Console.Write(list[n - i - 1] + " ");
            }
            Console.WriteLine();

            //var head = new Node(arr[0]);
            //var tail = new Node(arr[1]);

            //head.next = tail;

            //tail.previous = head;
            //tail.next = null;

            //for (int i = 2; i < n; i++) {
            //    var tmp = new Node(arr[i]);
            //    tmp.previous = tail;
            //    tail.next = tmp;
            //    tail = tmp;
            //    tail.next = null;
            //}


            //int q = int.Parse(Console.ReadLine());
            //while (q-- > 0) {
            //    var tmp = Console.ReadLine().Split(' ');
            //    int l = int.Parse(tmp[0]);
            //    int r = int.Parse(tmp[1]);
            //    l--; r--;
            //    if (l == 0) continue;

            //    int ll = 0;
            //    Node tmpL = head;
            //    while (ll < l) { tmpL = tmpL.next; ll++; }

            //    //if (l == r) {
            //    //    tmpL.previous.next = tmpL.next;
            //    //    tmpL.previous = null;
            //    //    tmpL.next = head;
            //    //    head = tmpL;
            //    //    continue;
            //    //}

            //    Node tmpR = tmpL;
            //    while (ll < r) { tmpR = tmpR.next; ll++; }
            //    Console.WriteLine("tmpL : " + tmpL.id + "    tmpL.previous : " + tmpL.previous.id);
            //    Console.WriteLine("tmpR : " + tmpR.id + "    tmpR.previous : " + tmpR.previous.id);

            //    tmpL.previous.next = tmpR.next;
            //    head.previous = tmpR;
            //    if (tmpR.next != null) tmpR.next.previous = tmpL.previous;
            //    tmpR.next = head;

            //    tmpL.previous = null;
            //    head = tmpL;
            //    head.previous= null;
            //}
            //for (int i = 0; i < n; i++) { Console.Write(head.id + " "); head = head.next; } Console.WriteLine();
        }

        #endregion
    }
    class Node {
        public int id;
        public Node next = null;
        public Node previous = null;

        public Node(int d) {
            id = d;
        }
    }
}
