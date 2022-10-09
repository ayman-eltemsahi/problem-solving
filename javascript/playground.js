/**
 * @param {number[]} nums
 * @return {number[]}
 */
var findDisappearedNumbers = function (nums) {
  const s = new Set(nums);
  const res = 0;
  return Array(nums.length)
    .fill(0)
    .map((_, i) => (s.has(i + 1) ? 0 : i + 1))
    .filter(Boolean);
};
