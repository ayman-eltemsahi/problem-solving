struct Solution {}

// Definition for a binary tree node.
// #[derive(Debug, PartialEq, Eq)]
// pub struct TreeNode {
//   pub val: i32,
//   pub left: Option<Rc<RefCell<TreeNode>>>,
//   pub right: Option<Rc<RefCell<TreeNode>>>,
// }
//
// impl TreeNode {
//   #[inline]
//   pub fn new(val: i32) -> Self {
//     TreeNode {
//       val,
//       left: None,
//       right: None
//     }
//   }
// }

use std::cell::RefCell;
use std::rc::Rc;

impl Solution {
  fn solve(
    i: usize,
    j: usize,
    inorder: &Vec<i32>,
    postorder: &mut Vec<i32>,
  ) -> Option<Rc<RefCell<TreeNode>>> {
    if i > j {
      return None;
    }

    let val = postorder.pop().unwrap();
    if i == j {
      return Some(Rc::new(RefCell::new(TreeNode::new(val))));
    }

    let mut c = i;
    while inorder[c] != val {
      c += 1;
    }

    let mut node = TreeNode::new(val);
    node.right = Solution::solve(c + 1, j, &inorder, postorder);
    node.left = Solution::solve(i, c - 1, &inorder, postorder);

    return Some(Rc::new(RefCell::new(node)));
  }

  pub fn build_tree(inorder: Vec<i32>, postorder: Vec<i32>) -> Option<Rc<RefCell<TreeNode>>> {
    let mut v = postorder.to_vec();
    return Solution::solve(0, inorder.len() - 1, &inorder, &mut v);
  }
}
