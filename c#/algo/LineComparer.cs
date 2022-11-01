using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo {
    public class LineComparer {
        TextReader OutputReader;

        public LineComparer(TextReader outputReader) {
            this.OutputReader = outputReader;
        }

        public void WriteLine<T>(T value) {
            var output = OutputReader.ReadLine();
            if (output != value.ToString()) {
                Write.Red(value.ToString().PadRight(10) + " is wrong");
            }
        }
    }
}
