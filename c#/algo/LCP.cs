using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class LCP
    {
        class suffix
        {
            public int index;
            public int rankA;
            public int rankB;
            public int n;
            public suffix(int index, int rankA, int rankB, int n) {
                this.index = index; this.rankA = rankA; this.rankB = rankB; this.n = n;
            }
        };

        public static List<int> buildSuffixArray(string txt, int n) {
            List<suffix> suffixes = new List<suffix>();

            for (int i = 0; i < n; i++) {
                suffix tmp = new suffix(i, txt[i] - 'a',
                                        ((i + 1) < n) ? (txt[i + 1] - 'a') : -1, n);
                suffixes.Add(tmp);
            }

            suffixes = suffixes.OrderBy(x => x.rankA).ThenBy(x => x.rankB).ToList();

            int[] ind = new int[n];

            for (int k = 4; k < 2 * n; k = k * 2) {
                int rank = 0;
                int prev_rank = suffixes[0].rankA;
                var sf = suffixes[0];
                sf.rankA = rank;
                ind[suffixes[0].index] = 0;

                for (int i = 1; i < n; i++) {
                    sf = suffixes[i];

                    if (suffixes[i].rankA == prev_rank &&
                            suffixes[i].rankB == suffixes[i - 1].rankB) {
                        prev_rank = suffixes[i].rankA;
                        sf.rankA = rank;
                    } else {
                        prev_rank = suffixes[i].rankA;
                        sf.rankA = ++rank;
                    }
                    ind[suffixes[i].index] = i;
                }

                for (int i = 0; i < n; i++) {
                    sf = suffixes[i];
                    int nextindex = suffixes[i].index + k / 2;
                    sf.rankB = (nextindex < n) ?
                                          suffixes[ind[nextindex]].rankA : -1;
                }

                suffixes = suffixes.OrderBy(x => x.rankA).ThenBy(x => x.rankB).ToList();
            }

            List<int> suffixArr = new List<int>();
            for (int i = 0; i < n; i++)
                suffixArr.Add(suffixes[i].index);

            return suffixArr;
        }

        public static List<int> kasai(string txt, List<int> suffixArr) {
            int n = suffixArr.Count;

            List<int> lcp = new List<int>();
            for (int i = 0; i < n; i++) lcp.Add(0);

            List<int> invSuff = new List<int>();
            for (int i = 0; i < n; i++) invSuff.Add(0);

            for (int i = 0; i < n; i++)
                invSuff[suffixArr[i]] = i;

            int k = 0;

            for (int i = 0; i < n; i++) {
                if (invSuff[i] == n - 1) {
                    k = 0;
                    continue;
                }

                int j = suffixArr[invSuff[i] + 1];

                while (i + k < n && j + k < n && txt[i + k] == txt[j + k])
                    k++;

                lcp[invSuff[i]] = k;

                if (k > 0)
                    k--;
            }

            return lcp;
        }

        public static int[] buildSuffixArrayForMany(string[] str, int[] lastN) {
            var suffixes = new List<suffix>();
            int add = 0, n;
            foreach (var item in str) {
                n = item.Length;
                for (int i = 0; i < n; i++) {
                    suffixes.Add(new suffix(i + add, item[i] - 'a', ((i + 1) < n) ? (item[i + 1] - 'a') : -1, n + add));
                }
                add += n;
            }

            n = str.Sum(x => x.Length);

            suffixes = suffixes.OrderBy(x => x.rankA).ThenBy(x => x.rankB).ToList();
            int[] ind = new int[n];
            for (int k = 4; k < 2 * n; k = k * 2) {
                int rank = 0, prev_rank = suffixes[0].rankA;
                var sf = suffixes[0];
                sf.rankA = rank;
                ind[suffixes[0].index] = 0;
                for (int i = 1; i < n; i++) {
                    sf = suffixes[i];
                    if (suffixes[i].rankA == prev_rank && suffixes[i].rankB == suffixes[i - 1].rankB) {
                        prev_rank = suffixes[i].rankA; sf.rankA = rank;
                    } else { prev_rank = suffixes[i].rankA; sf.rankA = ++rank; }
                    ind[suffixes[i].index] = i;
                }
                for (int i = 0; i < n; i++) {
                    sf = suffixes[i];
                    int nextindex = suffixes[i].index + k / 2;
                    sf.rankB = (nextindex < sf.n) ? suffixes[ind[nextindex]].rankA : -1;
                }
                suffixes = suffixes.OrderBy(x => x.rankA).ThenBy(x => x.rankB).ToList();
            }
            var suffixArr = new int[n];
            for (int i = 0; i < n; i++) { suffixArr[i] = suffixes[i].index; lastN[i] = suffixes[i].n; }
            return suffixArr;
        }

        public static int[] buildLCPArrayForMany(string[] strarr, int[] suffixArr, int[] lastN) {
            var str = strarr.Aggregate("", (x, y) => x + y);
            int n = suffixArr.Length;
            int[] lcp = new int[n], invSuff = new int[n];
            for (int i = 0; i < n; i++) invSuff[suffixArr[i]] = i;
            int k = 0;
            for (int i = 0; i < n; i++) {
                if (invSuff[i] == n - 1) { k = 0; continue; }
                int j = suffixArr[invSuff[i] + 1];

                int ni = 0, li = 0;
                while (i >= ni) ni += strarr[li++].Length;

                int nj = 0, lj = 0;
                while (j >= nj) nj += strarr[lj++].Length;
                while (i + k < ni && j + k < nj && str[i + k] == str[j + k]) k++;
                lcp[invSuff[i]] = k;
                if (k > 0) k--;
            }
            return lcp;
        }
    }
}
