using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank {
    class Week_of_Code___20 {
        public static void restartStopWatch() { Program.stopwatch.Restart(); }
        #region ___Non_Divisible_Subset
        public static void Non_Divisible_Subset() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int mod = int.Parse(tmp[1]);
            int[] nodes = Array.ConvertAll(Console.ReadLine().Split(' '), x => int.Parse(x) % mod);
            int[] nums = new int[mod];

            for (int _ = 0; _ < n; _++) {
                nums[nodes[_]]++;
            }

            int r = 0;
            if (nums[0] > 0) r++;
            int i = 1, j = mod - 1;
            while (i <= j) {
                if (i == j) { r++; } else {
                    r += Math.Max(nums[i], nums[j]);
                }
                i++;
                j--;
            }
            Console.WriteLine(r);
        }
        #endregion

        #region ___Synchronous_Shopping
        static HashSet<int>[] adjacent;
        static int[][] map;
        static int[] start, end, timeCount;
        static int n, m, k;
        static int wanted = int.MaxValue;
        public static void Synchronous_Shopping() {
            var file = File.ReadAllLines("D:\\prim.sublime");
            int indexFile = 0;
            var tmp = file[indexFile++].Split(' ');
            n = int.Parse(tmp[0]);  // number of shopping centers
            m = int.Parse(tmp[1]);  // number or roads in between
            k = int.Parse(tmp[2]);  // number of types of fish

            // read the (number of fish in the shop) and (the kinds of fish)
            map = new int[n][];
            for (int i = 0; i < n; i++) {
                tmp = file[indexFile++].Split(' ');
                map[i] = new int[int.Parse(tmp[0])];
                for (int j = 0; j < map[i].Length; j++) {
                    map[i][j] = int.Parse(tmp[j + 1]);
                }
            }

            // read the way beween the shop and the time
            start = new int[m]; end = new int[m]; timeCount = new int[m];
            for (int i = 0; i < m; i++) {
                tmp = file[indexFile++].Split(' ');
                start[i] = int.Parse(tmp[0]);
                end[i] = int.Parse(tmp[1]);
                timeCount[i] = int.Parse(tmp[2]);
            }

            restartStopWatch();

            adjacent = new HashSet<int>[n];
            for (int i = 0; i < n; i++) { adjacent[i] = new HashSet<int>(); }
            for (int i = 0; i < m; i++) {
                adjacent[start[i] - 1].Add(end[i]);
                adjacent[end[i] - 1].Add(start[i]);
            }

            List<int>[] shops = new List<int>[k];
            for (int i = 0; i < k; i++) { shops[i] = new List<int>(); }

            bool[] obtained = new bool[k];
            for (int i = 0; i < map[0].Length; i++) { obtained[map[0][i] - 1] = true; }
            for (int i = 0; i < map[n - 1].Length; i++) { obtained[map[n - 1][i] - 1] = true; }

            for (int i = 0; i < n; i++) {
                var curmap = map[i];
                for (int j = 0; j < curmap.Length; j++) {
                    shops[curmap[j] - 1].Add(i + 1);
                }
            }

            // take the points the must be taken
            HashSet<int> takenPoints = new HashSet<int>();
            for (int i = 0; i < k; i++) {
                if (shops[i].Count == 1) {
                    takenPoints.Add(shops[i][0]);
                    obtained[i] = true;
                }
            }

            // take all the fish from the points that must be taken
            foreach (var point in takenPoints) {
                var th = map[point - 1];
                for (int i = 0; i < th.Length; i++) {
                    obtained[th[i] - 1] = true;
                }
            }

            // we have all the points that must be taken and all the fish in them is {obtained};

            getPems(shops, takenPoints, obtained);

            Console.WriteLine(wanted);
        }

        private static void getPems(List<int>[] shops, HashSet<int> takenPoints, bool[] obtained) {
            if (gotAllFish(obtained)) {
                ConnectThePoints(takenPoints);
                return;
            }
            for (int i = 0; i < k; i++) {
                if (obtained[i]) continue;

                for (int j = 0; j < shops[i].Count; j++) {
                    HashSet<int> ls = copyList(takenPoints);
                    ls.Add(shops[i][j]);
                    obtained[i] = true;
                    getPems(shops, ls, obtained);
                    obtained[i] = false;
                }


            }
        }

        private static void ConnectThePoints(HashSet<int> takenPoints) {
            // adjacent, start, end, timeCount
            var final = takenPoints.ToArray();
            var len = final.Length;

            List<int>[] sets = new List<int>[len];
            for (int i = 0; i < len; i++) {
                sets[i] = new List<int> { final[i] };
            }

            bool collecting = true;
            while (collecting) {
                collecting = false;
                for (int i = 0; i < len; i++) {
                    for (int j = len - 1; j >= 0; j--) {
                        if (i == j) continue;
                        bool tr = true;
                        foreach (var p1 in sets[i]) {
                            foreach (var p2 in sets[j]) {
                                if (adjacent[p2 - 1].Contains(p1)) {
                                    collecting = true;
                                    tr = false;
                                    sets[i].AddRange(sets[j]);
                                    sets[j].Clear();
                                    break;
                                }
                            }   // end second foreach
                            if (!tr) break;
                        }   // end first foreach
                    }   // end second for
                }   // end first for
            }   // end while

            // now we have some sets of points, and we need to connect them into two roads or less
            var leftDistance = new List<int>();
            var rightDistance = new List<int>();

            foreach (List<int> set in sets) {
                if (set.Count == 0) continue;
                int l = -1, r = -1;
                if (set.Contains(1)) l = 1;
                if (set.Contains(n)) r = n;

                if (l == -1 && r == -1) {
                    foreach (int point in set) {
                        int howManyAdj = 0;
                        foreach (var adj in adjacent[point - 1]) {
                            if (set.Contains(adj)) howManyAdj++;
                        }
                        if (howManyAdj < 2) { if (l == -1) l = point; else r = point; }
                    }
                } else if (l == -1) {
                    foreach (int point in set) {
                        int howManyAdj = 0;
                        foreach (var adj in adjacent[point - 1]) {
                            if (set.Contains(adj)) howManyAdj++;
                        }
                        if (howManyAdj < 2) l = point;
                    }
                } else if (r == -1) {
                    foreach (int point in set) {
                        int howManyAdj = 0;
                        foreach (var adj in adjacent[point - 1]) {
                            if (set.Contains(adj)) howManyAdj++;
                        }
                        if (howManyAdj < 2) r = point;
                    }
                }
                findDistanceDistance = int.MaxValue;
                FindDistance(1, 1, l, -1, 0);
                leftDistance.Add(findDistanceDistance);

                findDistanceDistance = int.MaxValue;
                FindDistance(r, r, n, -1, 0);
                rightDistance.Add(findDistanceDistance);

            }
        }

        static int findDistanceDistance;
        private static void FindDistance(int abscomm, int commence, int fin, int proh, int current) {
            if (current >= findDistanceDistance) return;
            if (commence == fin) { findDistanceDistance = Math.Min(findDistanceDistance, current); return; }

            foreach (var p in adjacent[commence - 1]) {
                if (p == proh || p == abscomm || current >= findDistanceDistance) continue;
                FindDistance(abscomm, p, fin, commence, current + timeCount[p - 1]);
            }
        }

        private static bool gotAllFish(bool[] obtained) {
            foreach (var a in obtained) { if (!a) return false; }
            return true;
        }

        static HashSet<int> copyList(HashSet<int> ls) {
            var r = new HashSet<int>();
            foreach (var v in ls) { r.Add(v); }
            return r;
        }
        #endregion

        #region ___Simple_Game
        const int mod = 1000000007;
        static short[] gr;
        //static int _r = 0;
        public static void Simple_Game() {
            var tmp = "248 5 4".Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int k = int.Parse(tmp[2]);
            m = 5;
            if (m == 1) { Console.WriteLine(1); return; }
            if (n == m) { Console.WriteLine(0); return; }
            if (n == m + 1) { Console.WriteLine(m); return; }

            int[] m5 = { };
            var diff = new int[10][];
            diff[5] = m5;

            string _s = "";
            switch (k) {
                case 2: _s = "0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0"; break;
                case 3: _s = "0,0,1,2,3,1,4,3,2,4,5,6,7,8,9,7,6,9,8,11,10,12,13,10,11,13,12,15,14,16,17,5,15,17,16,19,18,20,21,18,19,21,20,23,22,25,24,22,23,24,25,26,27,29,28,27,26,28,29,30,31,14,32,31,30,32,33,34,35,37,36,35,34,36,37,38,39,40,41,39,38,41,40,43,42,44,45,42,43,45,44,47,46,48,49,46,47,49,48,51,50,52,53,50,51,53,52,55,54,57,56,54,55,56,57,58,59,61,60,59,58,60,61,62,63,64,65,63,62,65,64,67,66,68,69,66,67,69,68,71,70,73,72,70,71,72,73,74,75,77,76,75,74,76,77,78,79,81,80,79,78,80,81,82,83,85,84,83,82,84,85,86,87,88,89,87,86,89,88,91,90,92,93,90,91,93,92,95,94,96,97,94,95,97,96,99,98,100,101,33,99,101,100,103,102,105,104,102,103,104,105,106,107,109,108,107,106,108,109,110,111,113,112,111,110,112,113,114,115,117,116,115,114,116,117,118,119,120,121,119,118,121,120,123,122,124,125,122,123,125,124,127,126,128,129,126,127,129,128,131,130,132,133,130,131,133,132,135,134,137,136,134,135,136,137,138,139,141,140,139,138,140,141,142,143,145,144,143,142,144,145,146,147,149,148,147,146,148,149,150,151,152,153,151,150,153,152,155,154,156,157,154,155,157,156,159,158,160,161,158,159,161,160,163,162,164,165,162,163,165,164,167,166,169,168,166,167,168,169,170,171,98,172,171,170,172,173,174,175,177,176,175,174,176,177,178,179,181,180,179,178,180,181,182,183,184,185,183,182,185,184,187,186,188,189,186,187,189,188,191,190,193,192,190,191,192,193,194,195,197,196,195,194,196,197,198,199,200,201,199,198,201,200,203,202,204,205,202,203,205,204,207,206,208,209,206,207,209,208,211,210,212,213,210,211,213,212,215,214,217,216,214,215,216,217,218,219,221,220,219,218,220,221,222,223,225,224,223,222,224,225,226,227,229,228,227,226,228,229,230,231,232,233,231,230,233,232,235,234,236,237,234,235,237,236,239,238,240,241,238,239,241,240,243,242,244,245,242,243,245,244,247,246,249,248,246,247,248,249,250,251,253,252,251,250,252,253,254,255,256,257,173,254,257,256,259,258,260,261,258,259,261,260,263,262,265,264,262,263,264,265,266,267,269,268,267,266,268,269,270,271,273,272,271,270,272,273,274,275,277,276,275,274,276,277,278,279,280,281,279,278,281,280,283,282,284,285,282,283,285,284,287,286,288,289,286,287,289,288,291,290,292,293,290,291,293,292,295,294,297,296,294,295,296,297,298,299,301,300,299,298,300"; break;
                default: _s = "0,0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339,340,341,342,343,344,345,346,347,348,349,350,351,352,353,354,355,356,357,358,359,360,361,362,363,364,365,366,367,368,369,370,371,372,373,374,375,376,377,378,379,380,381,382,383,384,385,386,387,388,389,390,391,392,393,394,395,396,397,398,399,400,401,402,403,404,405,406,407,408,409,410,411,412,413,414,415,416,417,418,419,420,421,422,423,424,425,426,427,428,429,430,431,432,433,434,435,436,437,438,439,440,441,442,443,444,445,446,447,448,449,450,451,452,453,454,455,456,457,458,459,460,461,462,463,464,465,466,467,468,469,470,471,472,473,474,475,476,477,478,479,480,481,482,483,484,485,486,487,488,489,490,491,492,493,494,495,496,497,498,499,500,501,502,503,504,505,506,507,508,509,510,511,512,513,514,515,516,517,518,519,520,521,522,523,524,525,526,527,528,529,530,531,532,533,534,535,536,537,538,539,540,541,542,543,544,545,546,547,548,549,550,551,552,553,554,555,556,557,558,559,560,561,562,563,564,565,566,567,568,569,570,571,572,573,574,575,576,577,578,579,580,581,582,583,584,585,586,587,588,589,590,591,592,593,594,595,596,597,598,599,600"; break;
            }

            gr = Array.ConvertAll(_s.Split(','), x => short.Parse(x));

            //if (m < 15 || n <= 35 || (m == 5 && n <= 275) || (m == 6 && n <= 130) || (m == 7 && n <= 80) || (m == 8 && n <= 55)) {
            //    int r = 0;
            //    searchsimplegame(n, m, 0, ref r);
            //    Console.WriteLine(r);
            //    Console.WriteLine(151348015 + "        1570 ms");
            //    return;
            //}
            var dic = new Dictionary<int, int>();
            PopulatePerms(dic);
            //foreach (var ke in dic.Keys) {
            //    if (dic[ke] == 112740) {
            //        Console.WriteLine(ke);
            //        Console.WriteLine("".PadLeft(300, '&'));
            //    }
            //}
            //Console.WriteLine("nope");
            //for (int i = 1; i < 7; i++) {
            //    Console.WriteLine(i + "      " + gr[i]);
            //}
            //Console.WriteLine(searchsimplegame_special(1,10, 4, 0));


            Console.WriteLine(501375);
            Console.ReadLine();

            for (int i = 100; i < 601; i++) {
                int kk = 0;
                searchsimplegame(i, m, 0, ref kk);
                Console.WriteLine(kk);
                Console.WriteLine(dic[key(i, m)]);
                Console.WriteLine((dic[key(i, m)] - kk) + "       " + i + "            " + gr[i]);

                if (kk == dic[key(i, m)]) {
                    Console.Write("____________  ");
                } else {
                    Console.WriteLine("".PadLeft(50, '|'));
                }
                Console.ReadLine();
            }
        }

        private static int searchsimplegame_special(int s, int n, int m, int xor) {
            if (m <= 2) {
                Console.WriteLine("    " + n);
                //Console.ReadLine();
                if ((xor ^ gr[s] ^ gr[n]) == 0) { return 1; }
                return 0;
            }
            int r = 0;
            for (int i = s; i <= n - m + 1; i++) {
                Console.WriteLine(s.ToString().PadLeft(4 - m, ' '));
                r += searchsimplegame_special(i, n - i, m - 1, xor ^ gr[i]);
            }
            Console.WriteLine("".PadLeft(40, '-'));
            return r;
        }
        static bool odd(int i) { return i % 2 == 1; }
        private static void PopulatePerms(Dictionary<int, int> dic) {
            // num / 1
            for (int i = 1; i < 601; i++) {
                dic.Add(key(i, 1), 1);
            }
            // num / num
            for (int i = 2; i < 10; i++) {
                dic.Add(key(i, i), 1);
            }
            // num / m
            for (int m_ = 2; m_ < 11; m_++) {
                for (int i = m_ + 1; i < 601; i++) {
                    int fin = 0;
                    for (int j = i - 1; j >= m_ - 1; j--) {
                        fin += dic[key(j, m_ - 1)];
                        fin %= mod;
                    }
                    dic.Add(key(i, m_), fin);
                }
            }
        }
        static bool isone(int i, int j) { return ((odd(i) && !odd(j)) || (!odd(i) && odd(j))); }
        static int key(int large, int j) { return large * 100 + j; }
        static void searchsimplegame(int n, int m, int xor, ref int k) {
            if (m == 2) {
                if ((xor ^ gr[n - 1]) != 0) k++;
                int nm2 = n - m + 2, i = 2;
                while (i < nm2) {
                    if ((xor ^ gr[i] ^ gr[n - i]) != 0) k++;
                    i++;
                }
                k %= mod;
            } else {
                int nm2 = n - m + 2, i = 1, m1 = m - 1;
                while (i < nm2) {
                    searchsimplegame(n - i, m1, xor ^ gr[i], ref k);
                    i++;
                }
            }
        }
        //static void searchsimplegame2(int n, int m, int xor) {

        //    if (m == 1) {
        //        if ((xor ^ gr[n]) != 0) { _r++; _r %= mod; }
        //        return;
        //    }

        //    searchsimplegame2(n - 1, m - 1, xor);

        //    for (int i = 2; i <= n - m + 1; i++) {
        //        searchsimplegame2(n - i, m - 1, xor ^ gr[i]);
        //    }
        //}
        static void grundy(int k, int max) {
            gr[0] = 0;
            gr[1] = 0;
            gr[2] = 1;

            HashSet<int> less = new HashSet<int>();
            HashSet<int> grun = new HashSet<int>();

            for (int n = 3; n <= max; n++) {

                less.Clear();
                grun.Clear();

                for (int i = k; i >= 2; i--) {
                    getPiles(n, i, 0, less);
                }

                short g = -1;
                for (short i = 0; i < 10000; i++) {
                    if (!less.Contains(i)) { g = i; break; }
                }
                gr[n] = g;
            }
        }

        private static void getPiles(int n, int k, int xor, HashSet<int> less) {

            if (k == 1) {
                less.Add(xor ^ gr[n]);
                return;
            }

            for (int i = 1; i <= n - k + 1; i++) {
                getPiles(n - i, k - 1, xor ^ gr[i], less);
            }

        }
        static void grundy() {
            //gr[1] = 0;

            //List<int> lst = new List<int>();
            //for (int i = 2; i <= 100005; i++) {
            //    lst.Clear();
            //    lst.Add(-1);
            //    int p = (int)Math.Sqrt(i);

            //    for (int j = 1; j <= p; j++)
            //        if (i % j == 0) {
            //            if (j % 2 != 0) lst.Add(gr[i / j]);
            //            if ((i / j) % 2 != 0) lst.Add(gr[j]);
            //        }
            //    lst.Add(10000000);
            //    lst.Sort();

            //    for (int j = 1; j < lst.Count; j++) {
            //        if (Math.Abs(lst[j] - lst[j - 1]) > 1) gr[i] = lst[j - 1] + 1;
            //    }
            //}
        }

        #endregion

        #region ___Jogging_Cats
        public static void Jogging_Cats() {
            var tmp = Console.ReadLine().Split(' ');

            Program.stopwatch.Restart();

            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int[] start = new int[m], end = new int[m];
            var adj = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                start[i] = int.Parse(tmp[0]);
                end[i] = int.Parse(tmp[1]);
                adj[start[i] - 1].Add(end[i]);
                adj[end[i] - 1].Add(start[i]);
            }

            int _jc = 0;
            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    int index = 0;
                    foreach (var li in adj[i]) {
                        index++;
                        int ind2 = index;
                        if (adj[j].Contains(li)) {
                            foreach (var lj in adj[i]) {
                                ind2--;
                                if (ind2 >= 0) continue;
                                if (adj[j].Contains(lj)) _jc++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(_jc / 2);
        }
        #endregion

        #region ___Cat_cation_Rentals
        public static void Cat_cation_Rentals() {
            //var tmp = Console.ReadLine().Split(' ');
            //int n = int.Parse(tmp[0]);  // the number of rental requests
            //int d = int.Parse(tmp[1]);  // the number of days the property is available for
            //int k = int.Parse(tmp[2]);  // the number of queries

            //int[] L = new int[n], R = new int[n], Duration = new int[n];
            //// n lines (L,R)
            //for (int i = 0; i < n; i++) {
            //    tmp = Console.ReadLine().Split(' ');
            //    L[i] = int.Parse(tmp[0]);
            //    R[i] = int.Parse(tmp[1]);
            //    Duration[i] = R[i] - L[i] + 1;
            //}
            var s = File.ReadAllLines("C:\\cation1.sublime");
            int index = 0;
            var tmp = s[index++].Split(' ');
            int n = int.Parse(tmp[0]);  // the number of rental requests
            int d = int.Parse(tmp[1]);  // the number of days the property is available for
            int k = int.Parse(tmp[2]);  // the number of queries

            if (d == 1) {
                while (k-- > 0) {
                    Console.ReadLine();
                    Console.WriteLine(1);
                }
                return;
            }

            int[] L = new int[n], R = new int[n], Duration = new int[n];

            // n lines (L,R)
            for (int i = 0; i < n; i++) {
                tmp = s[index++].Split(' ');
                L[i] = int.Parse(tmp[0]);
                R[i] = int.Parse(tmp[1]);
                Duration[i] = R[i] - L[i] + 1;
            }
            var sb = new StringBuilder();

            if (k <= 300) {
                while (k-- > 0) {
                    int qq = int.Parse(s[index++]);
                    sb.Append(Calculate_b(L, R, Duration, n, qq));
                }
                Console.WriteLine(sb.ToString());
                return;
            }
            restartStopWatch();


            int[] map = new int[d];
            for (int i = 0; i < n; i++) { map[i] = -1; }
            PopulateSimilar(n, L, R, Duration, map, 1, n);


            while (k-- > 0) {
                int q = int.Parse(s[index++]);

                sb.Append(map[q - 1] + "  ");
            }

            Console.WriteLine(sb.ToString());
            Console.WriteLine();
        }

        static void PopulateSimilar(int n, int[] L, int[] R, int[] Duration, int[] map, int from, int to) {
            int l = map[from - 1];
            int r = map[to - 1];
            if (l == -1) { l = Calculate_b(L, R, Duration, n, from); map[from - 1] = l; }
            if (r == -1) { r = Calculate_b(L, R, Duration, n, to); map[to - 1] = r; }

            if (to - from < 2) return;

            if (l == r) {
                for (int i = from - 1; i < to; i++) {
                    map[i] = l;
                }
                return;
            }

            int mid = (from + to) / 2;
            PopulateSimilar(n, L, R, Duration, map, from, mid);
            PopulateSimilar(n, L, R, Duration, map, mid, to);
        }

        static int Calculate_b(int[] L, int[] R, int[] Duration, int n, int q) {
            HashSet<int> hash = new HashSet<int>();
            int g = 0;
            while (g < n && Duration[g] < q) g++;
            if (g == n) { return 0; }
            int _b = Duration[g];
            hash.Add(g);

            for (int i = g + 1; i < n; i++) {
                if (Duration[i] < q) continue;

                bool overlap = false;
                foreach (var j in hash) {
                    if (L[j] <= R[i] && L[i] <= R[j]) { overlap = true; break; }
                }

                if (!overlap) { _b += Duration[i]; hash.Add(i); }
            }
            return _b;
        }
        #endregion

    }
}
