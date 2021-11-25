
using System;

namespace AVLtree
{
    using System;
    using System.IO;

    public class Node
    {
        public int key, height;
        public string data;
        public Node left, right;

        public Node(int d, string num)
        {
            key = d;
            data = num;
            height = 1;
        }
    }

    public class AVLTree
    {
        Node root;

        static void Print_Tree_Horizontal(Node node, int space)
        {
            if (node != null)
            {
                Print_Tree_Horizontal(node.right, space + 6);
                for (int i = 1; i <= space; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(node.key);
                Print_Tree_Horizontal(node.left, space + 6);
            }
        }


        // A utility function to get height of the tree
        int height(Node N)
        {
            if (N == null)
                return 0;
            return N.height;
        }

        // A utility function to
        // get maximum of two integers
        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // A utility function to right
        // rotate subtree rooted with y
        // See the diagram given above.
        Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            // Perform rotation
            x.right = y;
            y.left = T2;

            // Update heights
            y.height = max(height(y.left), height(y.right)) + 1;
            x.height = max(height(x.left), height(x.right)) + 1;

            // Return new root
            return x;
        }

        // A utility function to left
        // rotate subtree rooted with x
        // See the diagram given above.
        Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            // Perform rotation
            y.left = x;
            x.right = T2;

            // Update heights
            x.height = max(height(x.left), height(x.right)) + 1;
            y.height = max(height(y.left), height(y.right)) + 1;

            // Return new root
            return y;
        }

        // Get Balance factor of node N
        int getBalance(Node N)
        {
            if (N == null)
                return 0;
            return height(N.left) - height(N.right);
        }

        Node insert(Node node, int key, string data)
        {
            /* 1. Perform the normal BST rotation */
            if (node == null)
                return (new Node(key, data));

            if (key < node.key)
                node.left = insert(node.left, key, data);
            else if (key > node.key)
                node.right = insert(node.right, key, data);
            else // Equal keys not allowed
                return node;

            /* 2. Update height of this ancestor node */
            node.height = 1 + max(height(node.left),
                                height(node.right));

            /* 3. Get the balance factor of this ancestor
            node to check whether this node became
            Wunbalanced */
            int balance = getBalance(node);

            // If this node becomes unbalanced, then
            // there are 4 cases Left Left Case
            if (balance > 1 && key < node.left.key)
                return rightRotate(node);

            // Right Right Case
            if (balance < -1 && key > node.right.key)
                return leftRotate(node);

            // Left Right Case
            if (balance > 1 && key > node.left.key)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && key < node.right.key)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            /* return the (unchanged) node pointer */
            return node;
        }

        /* Given a non-empty binary search tree, return the
        node with minimum key value found in that tree.
        Note that the entire tree does not need to be
        searched. */
        Node minValueNode(Node node)
        {
            Node current = node;

            /* loop down to find the leftmost leaf */
            while (current.left != null)
                current = current.left;

            return current;
        }
        Node maxValueNode(Node node)
        {
            Node current = node;

            /* loop down to find the leftmost leaf */
            while (current.right != null)
                current = current.right;

            return current;
        }
        bool Сontains(Node node, int key)
        {
            Node current = node;

            while (current != null)
            {

                if (key == current.key)
                {
                    return true;
                }
                else if (key < current.key)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }

            return false;
        }
        string FindNode(Node node, int key)
        {
            Node current = node;
            int couter = 0;
            while (current != null)
            {

                if (key == current.key)
                {
                    return current.key + "," + current.data + " iterations: " + couter;
                }
                else if (key < current.key)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
                couter++;
            }

            return "Not found";
        }
        void UpdateValue(Node node, int key, string data)
        {
            Node current = node;
            int couter = 0;
            while (current != null)
            {

                if (key == current.key)
                {
                    current.data = data;
                    break;
                }
                else if (key < current.key)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
                couter++;
            }
        }

        Node deleteNode(Node root, int key)
        {
            // STEP 1: PERFORM STANDARD BST DELETE
            if (root == null)
                return root;

            // If the key to be deleted is smaller than
            // the root's key, then it lies in left subtree
            if (key < root.key)
                root.left = deleteNode(root.left, key);

            // If the key to be deleted is greater than the
            // root's key, then it lies in right subtree
            else if (key > root.key)
                root.right = deleteNode(root.right, key);

            // if key is same as root's key, then this is the node
            // to be deleted
            else
            {

                // node with only one child or no child
                if ((root.left == null) || (root.right == null))
                {
                    Node temp = null;
                    if (temp == root.left)
                        temp = root.right;
                    else
                        temp = root.left;

                    // No child case
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else // One child case
                        root = temp; // Copy the contents of
                                     // the non-empty child
                }
                else
                {

                    // node with two children: Get the inorder
                    // successor (smallest in the right subtree)
                    Node temp = minValueNode(root.right);

                    // Copy the inorder successor's data to this node
                    root.key = temp.key;

                    // Delete the inorder successor
                    root.right = deleteNode(root.right, temp.key);
                }
            }

            // If the tree had only one node then return
            if (root == null)
                return root;

            // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE
            root.height = max(height(root.left),
                        height(root.right)) + 1;

            // STEP 3: GET THE BALANCE FACTOR
            // OF THIS NODE (to check whether
            // this node became unbalanced)
            int balance = getBalance(root);

            // If this node becomes unbalanced,
            // then there are 4 cases
            // Left Left Case
            if (balance > 1 && getBalance(root.left) >= 0)
                return rightRotate(root);

            // Left Right Case
            if (balance > 1 && getBalance(root.left) < 0)
            {
                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case
            if (balance < -1 && getBalance(root.right) <= 0)
                return leftRotate(root);

            // Right Left Case
            if (balance < -1 && getBalance(root.right) > 0)
            {
                root.right = rightRotate(root.right);
                return leftRotate(root);
            }
            return root;
        }

        // A utility function to print preorder traversal of
        // the tree. The function also prints height of every
        // node
        void preOrder(Node node, ref string text)
        {
            if (node != null)
            {
                Console.Write(node.key + " ");
                text += node.key + "," + node.data + "\n";
                preOrder(node.left, ref text);
                preOrder(node.right, ref text);
            }
        }
        public static void Main()
        {
            AVLTree tree = new AVLTree();
            string text = "";
            string fromFileText = System.IO.File.ReadAllText("DB.txt");
            string[] fromFileNodes = fromFileText.Split("\n");

            foreach (var item in fromFileNodes)
            {
               if(item=="")
                {
                    break;
                }
                string[] currentNodeDataArr = item.Split(",");
                tree.root = tree.insert(tree.root, Int32.Parse(currentNodeDataArr[0]), currentNodeDataArr[1]);
            }
            int maxKey = 0;

            while (true)
            {
                Console.WriteLine(" List of commands: \n " +
               "Enter 1 to add Element\n " +
               "Enter 2 to delete Element\n " +
               "Enter 3 to find Element by key\n " +
               "Enter 4 to edit Element by key\n " +
               "Enter 5 to stop and save\n " +
               "Enter 6 to print Tree\n");
                string input = Console.ReadLine();
                maxKey = tree.maxValueNode(tree.root).key;
                if (input == "1")
                {
                    Console.WriteLine("Enter value to add:\n");
                    string valueToAdd = Console.ReadLine();
                    maxKey++;
                    tree.root = tree.insert(tree.root, maxKey, valueToAdd);
                    Console.WriteLine("Value added succesfully");
                    continue;
                }
                if (input == "2")
                {
                    Console.WriteLine("Enter key to delete:\n");
                    string keyToDeleteStr = Console.ReadLine();
                    int keyToDelete;
                    Int32.TryParse(keyToDeleteStr, out keyToDelete);
                    if (tree.Сontains(tree.root, keyToDelete))
                    {
                        tree.root = tree.deleteNode(tree.root, keyToDelete);
                        Console.WriteLine("Value deleted succesfully\n");
                    }
                    else
                    {
                        Console.WriteLine("Value deleted succesfully\n");
                    }
                    continue;
                }
                if (input == "3")
                {
                    Console.WriteLine("Enter key to find:\n");
                    string keyToFindStr = Console.ReadLine();
                    int keyToFind;
                    Int32.TryParse(keyToFindStr, out keyToFind);
                    if (tree.Сontains(tree.root, keyToFind))
                    {
                        Console.WriteLine("Value was found succesfully: " + tree.FindNode(tree.root, keyToFind) + "\n");
                    }
                    else
                    {
                        Console.WriteLine("No key was found\n");
                    }
                    continue;
                }
                if (input == "4")
                {
                    Console.WriteLine("Enter key to update:\n");
                    string keyToFindStr = Console.ReadLine();
                    Console.WriteLine("Enter new data:\n");
                    string data = Console.ReadLine();
                    int keyToFind;
                    Int32.TryParse(keyToFindStr, out keyToFind);
                    if (tree.Сontains(tree.root, keyToFind))
                    {
                        tree.UpdateValue(tree.root, keyToFind, data);
                        Console.WriteLine("Value was updated succesfully: " + tree.FindNode(tree.root, keyToFind) + "\n");
                    }
                    else
                    {
                        Console.WriteLine("No key was found\n");
                    }
                    continue;

                }
                if (input == "5")
                {
                    Console.WriteLine("Final pre order:\n");
                    break;
                }
                if (input == "6")
                {
                    Print_Tree_Horizontal(tree.root, 6);
                    continue;
                }
                Console.WriteLine("Unrecognized command\n");
            }
            /*for (int i = 1; i <= 10000; i++)
            {
                tree.root = tree.insert(tree.root,i, "223");
            }*/


            tree.preOrder(tree.root, ref text);
            File.WriteAllText("DB.txt", text.TrimEnd('\r', '\n'));


        }
    }
}
