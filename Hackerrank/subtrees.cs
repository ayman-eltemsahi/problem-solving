using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Hackerrank
{
    class subtrees {
        static Random ran = new Random();
        const string filename = "C:\\stars2.sublime";
        public static void Subtree_Expectation() {
            bool r = false;
            //r = true;
            if (r) {
                CreateTestCase();
            } else {
                var file = File.ReadAllLines(filename); int ifile = 0;
                int q = int.Parse(file[ifile++]);
                q = 1;
                while (q-- > 0) {
                    Subtree_Expectation_Util(file, ref ifile);
                }


                if (filename == "C:\\stars2.sublime") Console.WriteLine(49754.2991584484);
                else Console.WriteLine("5014.33127474\n4939.94099606\n5054.65301316\n5006.52614232\n5003.58045888\n4974.01477154\n5006.22406666\n4966.82907911\n4999.90139600\n4950.24824118");
            }
        }


        static double top, bot;
        static int mx;
        static double[] wValue;
        static Complex[] fire;
        static void Subtree_Expectation_Util(String[] file, ref int ifile) {
            int n = int.Parse(file[ifile++]);
            int[] w = Array.ConvertAll(file[ifile++].Trim().Split(' '), int.Parse);
            wValue = Array.ConvertAll(file[ifile++].Trim().Split(' '), double.Parse);
            mx = wValue.Length;
            gn = (int)Math.Pow(2, Math.Ceiling(Math.Log(mx, 2)));
            gn1d = 1.0 / (double)gn;

            sr = getPrimitiveRootOfUnity(gn);
            sr_ = new Complex(1, 0);
            for (int i = 0; i < gn - 1; i++) sr_ *= sr;


            HashSet<int>[] adj = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();

            for (int i = 0; i < n - 1; i++) {
                var t_m_p = file[ifile++].Split(' ');
                int _l_ = int.Parse(t_m_p[0]) - 1;
                int _r_ = int.Parse(t_m_p[1]) - 1;
                adj[_l_].Add(_r_); adj[_r_].Add(_l_);
            }

            top = bot = 0;
            var df = findSubsNew(0, -1, adj, w);



            Console.WriteLine("top = {0}", top);
            Console.WriteLine("bot = {0}", bot);
            //Console.WriteLine("mx  = {0}", mx.ToString("N0"));
            Console.WriteLine(top / bot);

        }

        static int gn;
        static double gn1d;
        static Complex sr, sr_;
        static Dictionary<int, double> globe = new Dictionary<int, double>();
        static Dictionary<int, double> findSubs(int fr, int no, HashSet<int>[] adj, int[] w) {
            var d = new Dictionary<int, double> { { w[fr], 1 } };

            foreach (var node in adj[fr].Where(x => x != no)) {
                var cv = findSubs(node, fr, adj, w);
                if (d.Count * cv.Count > 350000) {
                    MergeDictionaries2Normal(d, cv);
                } else MergeDictionaries(d, cv);
            }

            foreach (var v in d) {
                top += wValue[v.Key] * v.Value;
                bot += v.Value;
            }

            return d;
        }

        static Complex[] findSubsNew(int fr, int no, HashSet<int>[] adj, int[] w) {
            var d = new Dictionary<int, double> { { w[fr], 1 } };

            //Add(globe, w[fr], 1);

            List<Complex[]> all = new List<Complex[]>();
            foreach (var node in adj[fr].Where(x => x != no)) {
                var cv = findSubsNew(node, fr, adj, w);
                all.Add(cv);
            }

            Complex[] dfft = createFFT(d);

            if (all.Count > 0) {
                for (int i = 0; i < gn; i++) {
                    foreach (var cv in all) {
                        dfft[i] *= cv[i];
                    }
                }
            }

            AssignTopBot(dfft);

            return dfft;
        }

        static Dictionary<int, double> map = new Dictionary<int, double>();
        static void AssignTopBot(Complex[] dfft) {
            var inverse = FFT(dfft, gn, sr_);

            for (int i = 0; i < mx; i++) {
                double ds = Math.Round(inverse[i].Real * gn1d);
                if (ds > 0) {
                    if (map.ContainsKey(i)) map[i] += ds; else map[i] = ds;
                }
            }

            foreach (var v in map) {
                top += wValue[v.Key] * v.Value;
                bot += v.Value;
            }
        }

        static Complex[] createFFT(Dictionary<int, double> d) {
            Complex[] A = new Complex[gn];
            foreach (var k in d.Keys) A[k] = new Complex(d[k], 0);
            return FFT(A, gn, sr);
        }

        static void MergeDictionaries2(Dictionary<int, double> d, Dictionary<int, double> cv) {
            int max = d.Keys.Max() + cv.Keys.Max() + 1;
            double[] A = new double[max + 1], B = new double[max + 1];
            int len = max * 2 + 2;
            int n = (int)Math.Pow(2, 1 + Math.Ceiling(Math.Log(len, 2)));

            if (fire != null && fire.Length < n) fire = null;

            if (fire == null) foreach (var key in d.Keys) { A[key] = d[key]; }

            foreach (var key in cv.Keys) { B[key] = cv[key]; }

            MultiplyTwoPolynomialsFFT(d, n, A, B, max);
        }

        static void MultiplyTwoPolynomialsFFT(Dictionary<int, double> d, int n, double[] a, double[] b, int max) {
            Complex[] A = new Complex[n], B = new Complex[n];

            if (fire == null) for (int i = 0; i < a.Length; i++) A[i] = new Complex(a[i], 0);
            for (int i = 0; i < b.Length; i++) B[i] = new Complex(b[i], 0);

            Complex w = getPrimitiveRootOfUnity(n);
            Complex[] F_A;
            if (fire == null) F_A = FFT(A, n, w); else { F_A = fire; fire = null; }

            Complex[] F_B = FFT(B, n, w);

            Complex w_ = new Complex(1, 0);
            for (int i = 0; i < n - 1; i++) {
                F_A[i] *= F_B[i];
                w_ *= w;
            }
            F_A[n - 1] *= F_B[n - 1];

            fire = F_A;
            Complex[] C = FFT(F_A, n, w_);

            var n1 = 1.0 / n;

            for (int i = 0; i < max; i++) {
                double s = (double)Math.Round(C[i].Real * n1);
                if (s != 0) {
                    if (d.ContainsKey(i)) d[i] += s; else d[i] = s;
                }
            }

        }

        static void MergeDictionaries2Normal(Dictionary<int, double> d, Dictionary<int, double> cv) {
            int max = d.Keys.Max() + cv.Keys.Max();
            double[] A = new double[max + 1], B = new double[max + 1];

            foreach (var key in d.Keys) { A[key] = d[key]; }
            foreach (var key in cv.Keys) { B[key] = cv[key]; }

            MultiplyTwoPolynomialsFFTNormal(d, A, B, max);

        }

        static void round(Complex[] F, int j) {
            F[j] = new Complex(Math.Round(F[j].Real), Math.Round(F[j].Imaginary));
        }

        static Complex getPrimitiveRootOfUnity(int gen) {
            return new Complex(Math.Cos(2 * Math.PI / gen), Math.Sin(2 * Math.PI / gen));
        }

        static void MergeDictionaries(Dictionary<int, double> d, Dictionary<int, double> cv) {
            int tt; double dd;

            var tmp = new Dictionary<int, double>(d);

            foreach (var item in cv) {
                tt = item.Key; dd = item.Value;
                foreach (var taken in tmp) {
                    Add(d, taken.Key + tt, taken.Value * dd);
                }
            }
        }

        static void Add(Dictionary<int, double> dic, int i, double j) {
            if (i > mx) return;
            if (dic.ContainsKey(i)) dic[i] += j; else dic[i] = j;
        }

        static void MultiplyTwoPolynomialsFFTNormal(Dictionary<int, double> d, double[] a, double[] b, int max) {
            int len = Math.Max(a.Length, b.Length);
            int n = (int)Math.Pow(2, 1 + Math.Ceiling(Math.Log(len, 2)));

            Complex[] A = new Complex[n], B = new Complex[n];

            for (int i = 0; i < a.Length; i++) A[i] = new Complex(a[i], 0);
            for (int i = 0; i < b.Length; i++) B[i] = new Complex(b[i], 0);

            Complex w = getPrimitiveRootOfUnity(n);
            Complex[] F_A = FFT(A, n, w);
            Complex[] F_B = FFT(B, n, w);

            Complex w_ = new Complex(1, 0);
            for (int i = 0; i < n - 1; i++) {
                F_A[i] *= F_B[i];
                w_ *= w;
            }
            F_A[n - 1] *= F_B[n - 1];

            Complex[] C = FFT(F_A, n, w_);

            var n1 = 1.0 / n;

            for (int i = 0; i <= max; i++) {
                double s = (double)Math.Round(C[i].Real * n1);
                if (s != 0) {
                    if (d.ContainsKey(i)) d[i] += s; else d[i] = s;
                }
            }
        }

        static void CreateTestCase() {
            int max = 0;
            int n = 50;
            int tot = 0;
            var st = new StringBuilder();
            st.Append("1\n" + n + "\n");
            for (int i = 0; i < n; i++) {
                int x = ran.Next(max += 7);
                //max -= x;
                tot += x;
                st.Append(x + " ");
            }
            st.Append("\n");
            for (int i = 0; i <= tot; i++) {
                int x = ran.Next(100000);
                st.Append(x).Append(" ");
            }
            st.Append("\n");
            bool[,] w = new bool[n, n];
            for (int i = 1; i < n; i++) {
                int x = ran.Next(0, i);
                st.Append((x + 1) + " " + (i + 1) + "\n");
            }
            File.WriteAllText("C:\\stars.sublime", st.ToString());
        }

        static Complex[] FFT(Complex[] A, int m, Complex w) {
            if (m == 1) return A;

            Complex[] A_even = new Complex[m / 2], A_odd = new Complex[m / 2];

            for (int i = 0; i < m; i += 2) {
                A_even[i / 2] = A[i];
                A_odd[i / 2] = A[i + 1];
            }
            Complex[] F_even = FFT(A_even, m / 2, w * w);
            Complex[] F_odd = FFT(A_odd, m / 2, w * w);
            Complex[] F = new Complex[m];
            Complex x = new Complex(1, 0);
            for (int j = 0; j < m / 2; ++j) {
                F[j] = F_even[j] + x * F_odd[j];
                F[j + m / 2] = F_even[j] - x * F_odd[j];
                x = x * w;
            }
            return F;
        }

    }
}
