using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    class Tester
    {
        public LineWriter goodline, badline, total;
        public int goodCount, badCount = 0;

        public Tester(int s = 2) {
            goodline = new LineWriter(s, ConsoleColor.Green);
            badline = new LineWriter(s + 1, ConsoleColor.Red);
            total = new LineWriter(s + 2, ConsoleColor.Yellow);
        }
        public void Start() {
            goodline.WriteAndReset(goodCount);
            badline.Color = ConsoleColor.Gray;
            badline.WriteAndReset("Failed     : " + badCount);
        }

        public void Assert(bool p) {
            if (!p) {
                badline.Color = ConsoleColor.Red;
                badline.WriteAndReset("Failed     : " + ++badCount);
            } else {
                goodline.WriteAndReset("Successful : " + (++goodCount).ToString().PadRight(15));
            }

            total.WriteAndReset("Percentage : " + (100.0 * goodCount / (goodCount + badCount)).ToString("F2") + " %");

        }
    }
}
