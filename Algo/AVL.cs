using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class AVLTree
    {
        public AVLNode Root { get; set; }
        public void Add(int value) {
            if (Root == null) {
                Root = new AVLNode(value);
            } else {
                Root = AVLNode.Insert(Root, value);
            }
        }

        public void Delete(int value) {
            Root = AVLNode.Delete(Root, value);
        }
        public void PreOrder(List<int> L) {
            Root?.PreOrder(L);
        }
    }

    public class AVLNode
    {
        public int value, height;
        public AVLNode left, right;
        public AVLNode(int _value) {
            value = _value;
            height = 1;
        }

        private static int Height(AVLNode node) {
            return node == null ? 0 : node.height;
        }

        private static int Balance(AVLNode node) {
            return Height(node.left) - Height(node.right);
        }

        private static void UpdateHeight(AVLNode node) {
            if (node == null) return;
            node.height = 1 + Math.Max(Height(node.left), Height(node.right));
        }

        public static AVLNode Insert(AVLNode root, int new_value) {
            if (root == null) return new AVLNode(new_value);

            if (new_value > root.value) {
                root.right = Insert(root.right, new_value);
            } else {
                root.left = Insert(root.left, new_value);
            }

            root.height = 1 + Math.Max(Height(root.left), Height(root.right));

            int balance = Height(root.left) - Height(root.right);


            // Left Left Case
            if (balance > 1 && new_value < root.left.value)
                return RightRotate(root);

            // Left Right Case
            if (balance > 1 && new_value > root.left.value) {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }

            // Right Right Case
            if (balance < -1 && new_value > root.right.value)
                return LeftRotate(root);

            // Right Left Case
            if (balance < -1 && new_value < root.right.value) {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
            }

            return root;
        }
        static AVLNode minValueNode(AVLNode node) {
            AVLNode current = node;

            /* loop down to find the leftmost leaf */
            while (current.left != null)
                current = current.left;

            return current;
        }
        public static AVLNode Delete(AVLNode root, int value) {
            // Standard BST Deletion
            if (root == null) return root;

            if (value < root.value)
                root.left = Delete(root.left, value);
            else if (value > root.value)
                root.right = Delete(root.right, value);
            else {
                if ((root.left == null) || (root.right == null)) {
                    AVLNode temp = root.left != null ? root.left : root.right;

                    if (temp == null) {
                        temp = root;
                        root = null;
                    } else
                        root = temp;
                } else {
                    AVLNode temp = minValueNode(root.right);

                    root.value = temp.value;
                    root.right = Delete(root.right, temp.value);
                }
            }

            if (root == null) return root;

            UpdateHeight(root);

            int balance = Balance(root);

            // Left Left Case
            if (balance > 1 && Balance(root.left) >= 0)
                return RightRotate(root);

            // Left Right Case
            if (balance > 1 && Balance(root.left) < 0) {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }

            // Right Right Case
            if (balance < -1 && Balance(root.right) <= 0)
                return LeftRotate(root);

            // Right Left Case
            if (balance < -1 && Balance(root.right) > 0) {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
            }

            return root;
        }

        static AVLNode RightRotate(AVLNode y) {
            AVLNode x = y.left;
            AVLNode T2 = x.right;

            // Perform rotation
            x.right = y;
            y.left = T2;

            // Update heights
            y.height = Math.Max(Height(y.left), Height(y.right)) + 1;
            x.height = Math.Max(Height(x.left), Height(x.right)) + 1;

            // Return new root
            return x;
        }

        static AVLNode LeftRotate(AVLNode x) {
            AVLNode y = x.right;
            AVLNode T2 = y.left;

            // Perform rotation
            y.left = x;
            x.right = T2;

            //  Update heights
            x.height = Math.Max(Height(x.left), Height(x.right)) + 1;
            y.height = Math.Max(Height(y.left), Height(y.right)) + 1;

            // Return new root
            return y;
        }

        public void PreOrder(List<int> L) {
            if (left != null) left.PreOrder(L);
            L.Add(value);
            if (right != null) right.PreOrder(L);
        }
    }



    public class AVLNodeTest
    {
        public static void Test() {
            List<int> numbers = new List<int>();
            AVLTree tree = new AVLTree();

            LineWriter goodline = new LineWriter(2, ConsoleColor.Green);
            LineWriter badline = new LineWriter(3, ConsoleColor.Gray);
            LineWriter total = new LineWriter(4, ConsoleColor.Yellow);
            int goodCount = 0;
            int badCount = 0;

            goodline.WriteAndReset(goodCount);
            badline.WriteAndReset("Failed     : " + badCount);

            Random rand = new Random();
            HashSet<int> unique = new HashSet<int>();
            int i = 0;
            while (true) {
                i++;
                int k = 0;

                bool deleteFlag =   numbers.Count > 0 && rand.Next() % 2 == 1;
                if (deleteFlag) {
                    k = numbers[rand.Next(numbers.Count)];
                    unique.Remove(k);
                    numbers.Remove(k);
                    tree.Delete(k);
                } else {
                    do {
                        k = rand.Next();
                    } while (!unique.Add(k));

                    numbers.Add(k);

                    tree.Add(k);
                }
                if (i % 100000 != 0) continue;

                numbers.Sort();
                List<int> tmp = new List<int>();
                tree.PreOrder(tmp);
                double ll = 1.44 * Math.Log(numbers.Count + 1, 2);
                double hh = ManualHeight(tree.Root);

                if (hh <= ll && Utility.Equal(numbers, tmp)) {
                    goodline.WriteAndReset("Successful : " + (++goodCount).ToString().PadRight(15) + "Height : " + hh.ToString().PadRight(15) + "Number Of Nodes : " + numbers.Count);
                } else {
                    badline.Color = ConsoleColor.Red;
                    badline.WriteAndReset("Failed     : " + ++badCount);
                }

                total.WriteAndReset("Percentage : " + (100.0 * goodCount / (goodCount + badCount)).ToString("F2") + " %");
            }
        }

        public static int ManualHeight(AVLNode node) {
            if (node == null) return 0;
            return 1 + Math.Max(ManualHeight(node.left), ManualHeight(node.right));
        }
    }

}
