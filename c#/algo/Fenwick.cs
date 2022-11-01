using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Fenwick
    {
        public static long query_fenwick(long[] fenwick, int i) {
            i++;
            long sum = 0;
            for (; i > 0; i -= i & (-i)) sum = (sum + fenwick[i]);
            return sum;
        }

        public static void updatefenwick(long[] fenwick, int n, int i, long val) {
            i++;
            for (; i <= n; i += i & (-i)) fenwick[i] = (fenwick[i] + val);
        }
        public static int query_fenwick(int[] fenwick, int i) {
            i++;
            int sum = 0;
            for (; i > 0; i -= i & (-i)) sum = (sum + fenwick[i]);
            return sum;
        }

        public static void updatefenwick(int[] fenwick, int n, int i, int val) {
            i++;
            for (; i <= n; i += i & (-i)) fenwick[i] = (fenwick[i] + val);
        }
    }
}
