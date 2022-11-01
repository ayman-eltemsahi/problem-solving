using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{

    public class Part
    {
        public int Count_Col { get; set; }
        public int Count_Row { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        private List<Line> L_Part;

        public Part(int Min = 0, int Max = 100000, int Col = 2, int Row = 1) {
            this.Min = Min;
            this.Max = Max;
            this.Count_Col = Col;
            this.Count_Row = Row;
            PopulateL();
        }
        public void PopulateL() {
            for (int i = 0; i < 1000; i++) { }
            PopulateL(new Random(DateTime.Now.Millisecond + 1000));
        }
        public void PopulateL(Random rand) {
            L_Part = new List<Line>();
            for (int i = 0; i < Count_Row; i++) {
                var l = new Line(Min, Max, Count_Col, rand);
                L_Part.Add(l);
            }
        }
        public override string ToString() {
            if (L_Part == null) PopulateL();
            return string.Join("\n", L_Part.Select(x => x.ToString()));
        }
        public Line this[int i] {
            get {
                CheckIndexValidity(i);
                return L_Part[i];
            }
        }

        private void CheckIndexValidity(int i) {
            if (i >= Count_Row)
                throw new ArgumentOutOfRangeException();
            if (L_Part == null) PopulateL();
        }

        public int first() { return this[0][0]; }
        public int second() { return this[0][1]; }
        public int third() { return this[0][2]; }
        public int fourth() { return this[0][3]; }
    }

    public class Single : Part
    {
        public Single(int n) : base(n, n, 1, 1) { }
    }

    public class View
    {
        List<Part> parts;
        public View(params Part[] parts) {
            this.parts = parts.ToList();
        }
        public override string ToString() {
            return string.Join("\n", parts);
        }
        public void Copy() {
            System.Windows.Forms.Clipboard.SetText(ToString());
        }
        public void Show() {
            Console.WriteLine(ToString());
        }
    }

}
