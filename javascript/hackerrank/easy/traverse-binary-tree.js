
// https://www.hackerrank.com/challenges/tree-preorder-traversal/problem?isFullScreen=true  

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
        
        if(root.data) result.push(root.data);
        if(root.left === null && root.right === null) {
            return;
        }
        
        if(root.left) this._preOrderRec(root.left, result);
        if(root.right) this._preOrderRec(root.right, result);
    }
}

module.exports = TraverseBinaryTree;