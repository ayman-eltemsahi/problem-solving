using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Line
    {
        public int Count { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        private List<int> L;

        public Line(int min, int max, int count_Col, Random rand) {
            Min = min;
            Max = max;
            Count = count_Col;
            PopulateL(rand);
        }

        public void PopulateL() {
            for (int i = 0; i < 1000; i++) { }
            PopulateL(new Random(DateTime.Now.Millisecond + 1000));
        }
        public void PopulateL(Random rand) {
            L = new List<int>();
            for (int i = 0; i < Count; i++) {
                int x = rand.Next(Min, Max + 1);
                //while (!H.Add(x))
                L.Add(x);
            }
        }
        public void PopulateLDistinct(Random rand) {
            var H = new HashSet<int>();
            for (int i = 0; i < Count; i++) {
                int x = rand.Next(Min, Max + 1);
                while (!H.Add(x)) x = rand.Next(Min, Max + 1);
            }
            L = new List<int>(H);
        }
        public override string ToString() {
            if (L == null) PopulateL();
            return string.Join(" ", L);
        }

        public int this[int i] {
            get {
                CheckIndexValidity(i);
                return L[i];
            }
            set {
                CheckIndexValidity(i);
                L[i] = value;
            }
        }

        private void CheckIndexValidity(int i) {
            if (i >= Count)
                throw new ArgumentOutOfRangeException();
            if (L == null) PopulateL();
        }
    }

}
