using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public static class Write
    {
        public static Action<object> Writer = Console.WriteLine;
        public static void Gray<T>(T obj) { Color(obj, ConsoleColor.Gray); }
        public static void Magenta<T>(T obj) { Color(obj, ConsoleColor.Magenta); }
        public static void Yellow<T>(T obj) { Color(obj, ConsoleColor.Yellow); }
        public static void White<T>(T obj) { Color(obj, ConsoleColor.White); }
        public static void Black<T>(T obj) { Color(obj, ConsoleColor.Black); }
        public static void Blue<T>(T obj) { Color(obj, ConsoleColor.Blue); }
        public static void Red<T>(T obj) { Color(obj, ConsoleColor.Red); }
        public static void Green<T>(T obj) { Color(obj, ConsoleColor.Green); }

        public static void Color(object obj, ConsoleColor color) {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Writer(obj);
            Console.ForegroundColor = c;
        }

        public static void SameLine() {
            Writer = Console.Write;
        }
        public static void NewLine() {
            Writer = Console.WriteLine;
        }
    }
}
