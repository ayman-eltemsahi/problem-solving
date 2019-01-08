using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public static class Algorithm {
        public static void Swap<T>(ref T a, ref T b) {
            var temp = a;
            a = b;
            b = temp;
        }

        public static T Max<T>(params T[] a) {
            var ans = a[0];
            var comp = Comparer<T>.Default;
            for (var i = 1; i < a.Length; i++) ans = comp.Compare(ans, a[i]) >= 0 ? ans : a[i];
            return ans;
        }

        public static T Min<T>(params T[] a) {
            var ans = a[0];
            var comp = Comparer<T>.Default;
            for (var i = 1; i < a.Length; i++) ans = comp.Compare(ans, a[i]) <= 0 ? ans : a[i];
            return ans;
        }

        public static void RandomShuffle<T>(T[] a, int index, int length) {
            if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();
            var last = index + length;
            if (last > a.Length) throw new ArgumentException();
            var rnd = new Random(DateTime.Now.Millisecond);
            for (var i = index + 1; i < last; i++) Swap(ref a[i], ref a[rnd.Next(index, i + 1)]);
        }

        public static void RandomShuffle<T>(T[] a) {
            RandomShuffle(a, 0, a.Length);
        }

        public static bool NextPermutation<T>(T[] a, int index, int length, Comparison<T> compare = null) {
            compare = compare ?? Comparer<T>.Default.Compare;
            if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();
            var last = index + length;
            if (last > a.Length) throw new ArgumentException();
            for (var i = last - 1; i > index; i--)
                if (compare(a[i], a[i - 1]) > 0) {
                    var j = i + 1;
                    for (; j < last; j++) if (compare(a[j], a[i - 1]) <= 0) break;
                    Swap(ref a[i - 1], ref a[j - 1]);
                    Array.Reverse(a, i, last - i);
                    return true;
                }
            Array.Reverse(a, index, length);
            return false;
        }

        public static bool NextPermutation<T>(T[] a, Comparison<T> compare = null) {
            return NextPermutation(a, 0, a.Length, compare);
        }

        public static bool PrevPermutation<T>(T[] a, int index, int length, Comparison<T> compare = null) {
            compare = compare ?? Comparer<T>.Default.Compare;
            if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();
            var last = index + length;
            if (last > a.Length) throw new ArgumentException();
            for (var i = last - 1; i > index; i--)
                if (compare(a[i], a[i - 1]) < 0) {
                    var j = i + 1;
                    for (; j < last; j++) if (compare(a[j], a[i - 1]) >= 0) break;
                    Swap(ref a[i - 1], ref a[j - 1]);
                    Array.Reverse(a, i, last - i);
                    return true;
                }
            Array.Reverse(a, index, length);
            return false;
        }

        public static bool PrevPermutation<T>(T[] a, Comparison<T> compare = null) {
            return PrevPermutation(a, 0, a.Length, compare);
        }

        public static int LowerBound<T>(IList<T> a, int index, int length, T value, Comparison<T> compare = null) {
            compare = compare ?? Comparer<T>.Default.Compare;
            if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();
            if (index + length > a.Count) throw new ArgumentException();
            var ans = index;
            var last = index + length;
            var p2 = 1;
            while (p2 <= length) p2 *= 2;
            for (p2 /= 2; p2 > 0; p2 /= 2) if (ans + p2 <= last && compare(a[ans + p2 - 1], value) < 0) ans += p2;
            return ans;
        }

        public static int LowerBound<T>(IList<T> a, T value, Comparison<T> compare = null) {
            return LowerBound(a, 0, a.Count, value, compare);
        }

        public static int UpperBound<T>(IList<T> a, int index, int length, T value, Comparison<T> compare = null) {
            compare = compare ?? Comparer<T>.Default.Compare;
            if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();
            if (index + length > a.Count) throw new ArgumentException();
            var ans = index;
            var last = index + length;
            var p2 = 1;
            while (p2 <= length) p2 *= 2;
            for (p2 /= 2; p2 > 0; p2 /= 2) if (ans + p2 <= last && compare(a[ans + p2 - 1], value) <= 0) ans += p2;
            return ans;
        }

        public static int UpperBound<T>(IList<T> a, T value, Comparison<T> compare = null) {
            return UpperBound(a, 0, a.Count, value, compare);
        }

        public static void Fill<T>(this IList<T> array, T value) where T :struct {
            for (var i = 0; i < array.Count; i++)
                array[i] = value;
        }
    }
}
