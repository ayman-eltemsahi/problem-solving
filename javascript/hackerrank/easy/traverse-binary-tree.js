/*
 Problems Links:
 https://www.hackerrank.com/challenges/tree-preorder-traversal/problem?isFullScreen=true  
 https://www.hackerrank.com/challenges/tree-postorder-traversal/problem?isFullScreen=true
 https://www.hackerrank.com/challenges/tree-inorder-traversal/problem?isFullScreen=true
 https://www.hackerrank.com/challenges/tree-level-order-traversal/problem?isFullScreen=true

 This article is great to explain the 4 Types of Tree Traversal Algorithms
 https://towardsdatascience.com/4-types-of-tree-traversal-algorithms-d56328450846
 */

class TraverseBinaryTree {
    constructor(root) {
        this.root = root;
    }

    preOrder() {
        const result = [];
        this._preOrderRec(this.root, result);
        return result;
    }
    
    _preOrderRec(root, result) {
        if(root === null) {
            return;
        }

        result.push(root.data)
        if(root.left) this._preOrderRec(root.left, result);
        if(root.right) this._preOrderRec(root.right, result);
    }

    inOrder() {
        const result = [];
        this._inOrderRec(this.root, result);
        return result;
    }
    
    _inOrderRec(root, result) {
        
        if(root ===  null) {
            return;
        }
    
        if(root.left) this._inOrderRec(root.left, result);
        result.push(root.data);
        if(root.right) this._inOrderRec(root.right, result);
    }

    postOrder() {
        const result = [];
        this._postOrderRec(this.root, result);
        return result;
    }

    _postOrderRec(root, result) {
        
        if(root ===  null) {
            return;
        }
    
        if(root.left) this._postOrderRec(root.left, result);
        if(root.right) this._postOrderRec(root.right, result);
        result.push(root.data);
    }

    levelOrder() {
        const stack = [];
        stack.push(this.root);
        const queue = [];
        queue.push(this.root.data);
        while(stack.length !== 0) {
            const node = stack.shift();
            if(node.left) {
                stack.push(node.left);
                queue.push(node.left.data);
            }

            if(node.right) {
                stack.push(node.right);
                queue.push(node.right.data);
            }
        }
        return queue;
    }
}

module.exports = TraverseBinaryTree;