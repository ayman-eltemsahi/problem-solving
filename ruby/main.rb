# @param {Integer[]} nums
# @return {Integer}
def array_nesting(nums)
  n = nums.length
  res = 0
  n.times do |i|
    next unless nums[i] >= 0

    len = 0
    k = i
    while nums[k] >= 0
      len += 1
      g = nums[k]
      nums[k] = -1
      k = g
    end

    res = [res, len].max
  end

  res
end

nums = [5, 4, 0, 3, 1, 6, 2]
puts array_nesting(nums)
