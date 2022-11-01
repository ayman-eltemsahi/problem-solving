using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class LineWriter
    {
        public static Action<object> Writer = Console.Write;
        private static readonly object ConsoleWriterLock = new object();
        public int Row { get; set; }
        public int Col { get; set; }
        public ConsoleColor Color { get; set; }
        public LineWriter(int Row = 0) {
            this.Row = Row;
            Color = ConsoleColor.Gray;
            Console.CursorVisible = false;
        }
        public LineWriter(int Row, ConsoleColor color) {
            this.Row = Row;
            this.Color = color;
            Console.CursorVisible = false;
        }

        public void Write(object obj, ConsoleColor color) {
            lock (ConsoleWriterLock) {
                WriteUtil(obj, color);
            }
        }

        public void Write(object obj) {
            Write(obj, Color);
        }
        private void WriteUtil(object obj, ConsoleColor color) {
            int old_r = Console.CursorTop;
            int old_c = Console.CursorLeft;
            ConsoleColor old_color = Console.ForegroundColor;

            Console.CursorTop = Row;
            Console.CursorLeft = Col;
            Console.ForegroundColor = color;

            Writer(obj);

            Col = Console.CursorLeft;

            Console.CursorTop = old_r;
            Console.CursorLeft = old_c;
            Console.ForegroundColor = old_color;

        }

        public void Reset() {
            Col = 0;
        }

        public void WriteAndReset(object obj) {
            Reset();
            Write(obj);
        }
    }
}
