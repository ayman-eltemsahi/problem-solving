const expect = require('chai').expect;
const BinaryTree = require('../../../hackerrank/easy/binary-tree');
const Traversal = require('../../../hackerrank/easy/traverse-binary-tree');

const buildTree = function(array) {
  var tree = new BinaryTree();
  for (var i = 0; i < array.length; i++) {
      tree.root = tree.insert(tree.root, array[i]);
  }

  return tree.root;
}

describe('TraverseBinaryTree', function () {
  /*
  [1, 2, 5, 3, 4, 6 ]

    1
      \
       2
        \
         5
        /  \
       3    6
        \
         4  

  */
    it('should return values pre ordered', function () {
        const tree = buildTree([1, 2, 5, 3, 4, 6 ]);
        const traversal = new Traversal(tree);
        const result = traversal.preOrder(tree);
        expect(result).to.have.ordered.members([1, 2, 5, 3, 4, 6])
    });

    it('should return values in ordered', function () {
      const tree = buildTree([1, 2, 5, 3, 4, 6 ]);
      const traversal = new Traversal(tree);
      const result = traversal.inOrder(tree);
      expect(result).to.have.ordered.members([1, 2, 3, 4, 5, 6])
    });

    it('should return values post ordered', function () {
      const tree = buildTree([1, 2, 5, 3, 4, 6]);
      const traversal = new Traversal(tree);
      const result = traversal.postOrder(tree);
      expect(result).to.have.ordered.members([4, 3, 6, 5, 2, 1])
    });
});