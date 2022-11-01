using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo {
    public static class DotWriter {
        static bool forward = true;
        static char[] current;
        static int max = 90, index, size = 50;

        static DotWriter() {
            setMax(size);
        }
        public static void setMax(int i) {
            Console.CursorVisible = false;
            size = i;
            current = new char[max];
            index = 0;
            for (int j = 0; j < size; j++) current[j] = '.';
            for (int j = size; j < max; j++) current[j] = ' ';
        }

        public static void write() {
            move();

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(current);
        }

        private static void move() {
            current[index] = ' ';
            current[(index + size) % max] = '.';
            index++;
            index %= max;
        }
    }
}
