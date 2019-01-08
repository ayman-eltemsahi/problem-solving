using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> array = new List<T>();

        public void Add(T element) {
            array.Add(element);
            int c = array.Count - 1;
            int parent = (c - 1) >> 1;
            while (c > 0 && array[c].CompareTo(array[parent]) < 0) {
                T tmp = array[c];
                array[c] = array[parent];
                array[parent] = tmp;
                c = parent;
                parent = (c - 1) >> 1;
            }
        }

        public T RemoveMin() {
            T ret = array[0];
            array[0] = array[array.Count - 1];
            array.RemoveAt(array.Count - 1);

            int c = 0;
            while (c < array.Count) {
                int min = c;
                if (2 * c + 1 < array.Count && array[2 * c + 1].CompareTo(array[min]) == -1)
                    min = 2 * c + 1;
                if (2 * c + 2 < array.Count && array[2 * c + 2].CompareTo(array[min]) == -1)
                    min = 2 * c + 2;

                if (min == c)
                    break;
                else {
                    T tmp = array[c];
                    array[c] = array[min];
                    array[min] = tmp;
                    c = min;
                }
            }

            return ret;
        }

        public T Peek() {
            return array[0];
        }

        public int Count {
            get {
                return array.Count;
            }
        }
    }
}
