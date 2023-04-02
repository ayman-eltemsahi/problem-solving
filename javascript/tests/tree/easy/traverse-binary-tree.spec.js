const expect = require('chai').expect;
const BinaryTree = require('../../../tree/easy/binary-tree').Tree;
const Node = require('../../../tree/easy/binary-tree').Node;
const Traversal = require('../../../tree/easy/traverse-binary-tree');

const buildTree = function(array) {
  const tree = new BinaryTree();
  for (let i = 0; i < array.length; i++) {
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

    it('should return values level ordered', function () {
      const tree = buildTree([1, 2, 5, 3, 4, 6]);
      const traversal = new Traversal(tree);
      const result = traversal.levelOrder(tree);
      expect(result).to.have.ordered.members([1, 2, 5, 3, 6, 4])
    });

    it('should return tree hight', function () {
      const array = [3, 5, 2, 1, 4, 6, 7];
      const tree = new BinaryTree();
      for (let i = 0; i < array.length; i++) {
          tree.root = tree.insert(tree.root, array[i]);
      }
      
      const level = tree.height(tree.root);
      expect(level).to.be.eql(3);

    });

    it('should return node that match specific value', function () {
      const tree = buildTree([1, 2, 5, 3, 4, 6]);
      const traversal = new Traversal(tree);
      
      const node = traversal.findNode(5);
      expect(node).to.be.an.instanceof(Node);
      expect(node.data).to.be.eql(5);
    });
    /*
      10
     / \
    5    15
   / \    /  \
 3    7   13    18
/    /  
1   6
*/
    it('should return sum of nodes that their values in the giving range', function () {
      const tree = buildTree([10,5,15,3,7,13,18,1,null,6]);
      const traversal = new Traversal(tree);
      
      const sum = traversal.rangeSumBST(6, 10);
      expect(sum).to.be.eql(23);
    });
});