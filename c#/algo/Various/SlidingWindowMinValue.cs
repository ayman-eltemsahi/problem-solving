using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Various
{
    public class SlidingWindowMinValue
    {
        public static void Start() {
            int[] A = { 4, 2, 5, 7, 2, 1, 6, 4, 2, 1, 0, 9, 5, 6, 4, 2, 1, 2, 5, 1, 6 };
            int k = 3;

            Write.Writer = Console.Write;
            MyQueue Q = new MyQueue();

            for (int i = 0; i < A.Length; i++) {

                AddToSlidingWindow(A, k, Q, i);

                var min = A[Q.PeekFront()];
                Console.WriteLine(min);
            }

        }

        private static void AddToSlidingWindow(int[] A, int k, MyQueue Q, int i) {
            while (!Q.Empty && Q.PeekFront() + k <= i)
                Q.PopFront();

            while (!Q.Empty && A[Q.PeekBack()] >= A[i])
                Q.PopBack();

            Q.Enqueue(i);
        }

        class MyQueue
        {
            List<int> items = new List<int>();
            int back = 0;
            public int Count { get { return items.Count; } }
            public bool Empty { get { return items.Count == back; } }
            public void Enqueue(int v) {
                items.Add(v);
            }

            public int PopFront() {
                if (items.Count == back) throw new ArgumentException("Queue is Empty");
                int r = items[back];
                back++;
                CheckEmptyCells();
                return r;
            }

            public int PopBack() {
                if (items.Count == back) throw new ArgumentException("Queue is Empty");
                int r = items[items.Count - 1];
                items.RemoveAt(items.Count - 1);
                return r;
            }

            public int PeekFront() {
                if (items.Count == back) throw new ArgumentException("Queue is Empty");
                return items[back];
            }

            public int PeekBack() {
                if (items.Count == back) throw new ArgumentException("Queue is Empty");
                return items[items.Count - 1];
            }

            internal void display() {
                Write.Writer = Console.Write;
                for (int i = back; i < items.Count; i++) {
                    Write.Yellow(items[i] + " ");
                }
                Console.WriteLine();
            }

            private void CheckEmptyCells() {
                int n = items.Count;
                if (n < 50 || n / 2 >= back) return;

                for (int i = back; i < n; i++) {
                    items[i - back] = items[i];
                }
                items.RemoveRange(n - back, back);
                back = 0;
            }
        }
    }
}
