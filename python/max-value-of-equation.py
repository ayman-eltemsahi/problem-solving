import heapq
from typing import List

class Solution:
  def findMaxValueOfEquation(self, points: List[List[int]], k: int) -> int:
    n = len(points)
    q = []

    res = float('-inf')
    for i in range(0, n):
      while len(q) > 0 and points[i][0] - q[0][1] > k:
        heapq.heappop(q)

      if len(q) > 0:
        res = max(res, points[i][0] + points[i][1] - q[0][0])

      heapq.heappush(q, [-points[i][1] + points[i][0], points[i][0]])

    return int(res)


points = [[-19,-12],[-13,-18],[-12,18],[-11,-8],[-8,2],[-7,12],[-5,16],[-3,9],[1,-7],[5,-4],[6,-20],[10,4],[16,4],[19,-9],[20,19]]
k = 6
res = Solution().findMaxValueOfEquation(points, k)

print("\033[92m Result:", res, '\033[0m')
assert(res == 35)



# # C++
# class Solution {
#  public:
#   int findMaxValueOfEquation(vector<vector<int>>& points, int k) {
#     int n = points.size();
#     int res = INT_MIN;

#     auto cmp = [&](vector<int>& a, vector<int>& b) {
#       return a[0] == b[0] ? a[1] > b[1] : a[0] < b[0];
#     };
#     priority_queue<vector<int>, vector<vector<int>>, decltype(cmp)> q(cmp);
#     for (int i = 0; i < n; i++) {
#       while (!q.empty() && points[i][0] - q.top()[1] > k) {
#         q.pop();
#       }

#       if (!q.empty()) {
#         res = max(res, points[i][0] + points[i][1] + q.top()[0]);
#       }

#       q.push({points[i][1] - points[i][0], points[i][0]});
#     }

#     return res;
#   }
# };
