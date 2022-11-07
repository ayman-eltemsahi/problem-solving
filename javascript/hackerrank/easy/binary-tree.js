class Node {
    constructor(data) {
        this.data = data;
        this.left = null;
        this.right = null; 
    }
}

class Tree {
    constructor() {
        this.root = null;
    }

    insert(node, data) {
        if (node == null){
            node = new Node(data);
        }
         else if (data < node.data){
            node.left  = this.insert(node.left, data);
        }
        else{
            node.right = this.insert(node.right, data);   
        }
    
        return node;
    }
}

module.exports = Tree;