# Definition for singly-linked list.
# class ListNode
#     attr_accessor :val, :next
#     def initialize(val = 0, _next = nil)
#         @val = val
#         @next = _next
#     end
# end
# Definition for a binary tree node.
# class TreeNode
#     attr_accessor :val, :left, :right
#     def initialize(val = 0, left = nil, right = nil)
#         @val = val
#         @left = left
#         @right = right
#     end
# end

def solve(items, l, r)
  return nil if l > r
  return TreeNode.new(items[l]) if l == r

  m = (1 + l + r) / 2
  TreeNode.new(items[m], solve(items, l, m - 1), solve(items, m + 1, r))
end

# @param {ListNode} head
# @return {TreeNode}
def sorted_list_to_bst(head)
  return nil if head.nil?

  items = []
  until head.nil?
    items.push(head.val)
    head = head.next
  end

  solve(items, 0, items.length - 1)
end
