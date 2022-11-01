using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo
{
    class Segment_Tree {

        public static void Segment_Tree_RMQ(int[] mainArray = null) {

            if (mainArray == null) mainArray = new int[] { 2, 3, 6, 7, 8, 9, 9, 0, 4, 3, 6, 6, 7, 84, 6, 4, 45, 6, 7, 8, 7, 9, 9, 6, 4, 43, 3, 2, 2, 3, 4, 6, 6, 7, 8, 9, 9 };


            int n = mainArray.Length;

            // get the size of the segment tree array
            int size = (int)(Math.Ceiling(Math.Log(n, 2)));
            size = (2 * (int)Math.Pow(2, size)) - 1;

            int[] segArray = new int[size];
            for (int i = 0; i < size; i++) segArray[i] = int.MaxValue;

            ConstructSegmentTree(mainArray, segArray, 0, n - 1, 0);

            Console.WriteLine(searchInRange(segArray, 8, n - 1, n));
        }

        static int ConstructSegmentTree(int[] mainArray, int[] segArray, int from, int to, int seg) {
            if (from == to) {
                segArray[seg] = mainArray[from];
                return segArray[seg];
            }

            int mid = from + (to - from) / 2;
            segArray[seg] = Math.Min(ConstructSegmentTree(mainArray, segArray, from, mid, 2 * seg + 1),
                                     ConstructSegmentTree(mainArray, segArray, mid + 1, to, 2 * seg + 2));
            return segArray[seg];
        }

        static int searchInRange(int[] segArray, int qL, int qR, int n) {
            if (qL < 0 || qR >= n || qR < qL) {
                throw new IndexOutOfRangeException();
            }

            return searchInRangeUtil(segArray, qL, qR, 0, n - 1, 0);
        }
        static int searchInRangeUtil(int[] segArray, int qL, int qR, int from, int to, int seg) {
            // completely overlaps
            if (from >= qL && to <= qR) {
                return segArray[seg];
            }

            // no overlap
            if (from > qR || to < qL) {
                return int.MaxValue;
            }

            // partial overlap
            int mid = from + (to - from) / 2;
            return Math.Min(searchInRangeUtil(segArray, qL, qR, from, mid, 2 * seg + 1),
                            searchInRangeUtil(segArray, qL, qR, mid + 1, to, 2 * seg + 2));
        }

    }
}