using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo {
    public class Ran {
        static Random random;

        static Ran() {
            Fix();
        }

        public static void Fix(int seed = 0) { random = new Random(seed); }
        public static void Randomize() { random = new Random(); }

        #region Int
        public static int Int() { return random.Next(); }
        public static int Int(int maxValue) { return random.Next(maxValue); }
        public static int Int(int minValue, int maxValue) { return random.Next(minValue, maxValue); }
        #endregion

        #region Double
        public static double Double() { return random.NextDouble(); }
        public static double Double(int maxValue) { return maxValue * random.NextDouble(); }
        public static double Double(int minValue, int maxValue) { return minValue + maxValue * random.NextDouble(); }
        #endregion

        #region IntArray
        public static int[] IntArray(int size) {
            int[] array = new int[size]; for (int i = 0; i < size; i++) array[i] = random.Next(); return array;
        }
        public static int[] IntArray(int size, int maxValue) {
            int[] array = new int[size]; for (int i = 0; i < size; i++) array[i] = random.Next(maxValue); return array;
        }
        public static int[] IntArray(int size, int minValue, int maxValue) {
            int[] array = new int[size]; for (int i = 0; i < size; i++) array[i] = random.Next(minValue, maxValue); return array;
        }
        #endregion
    }
}
