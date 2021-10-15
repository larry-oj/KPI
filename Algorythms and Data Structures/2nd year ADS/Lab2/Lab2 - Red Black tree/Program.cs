using System.IO;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_RB_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"";

            RedBlackTree tree = new RedBlackTree(path);

            // . . .

            tree.DumpData();
        }

        static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "qwertyuiopasdfghjklzxcvbnm";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static void Shuffle(IList<int> list)
        {
            var random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    
        static void RandomizeData(ref RedBlackTree tree)
        {
            var rndNums = new List<int>();
            for (int i = 1; i <= 10000; i++)
            {
                rndNums.Add(i);
            }
            Shuffle(rndNums);

            for (int i = 0; i < 10000; i++)
            {
                tree.Insert(rndNums[i], RandomString(10));
            }
        }
    }


    enum Color
    {
        Red,
        Black
    }

    
    class RedBlackTree
    {
        public class Data
        {
            public int PK { get; set; }
            public string Content { get; set; }
        }

        public class Node
        {
            public Color colour { get; set; }
            public Node left { get; set; }
            public Node right { get; set; }
            public Node parent { get; set; }
            public Data data { get; set; }

            public Node(Data data) 
            {
                this.data = data; 
            }

            public Node(Color colour) 
            { 
                this.colour = colour; 
            }

            public Node(Data data, Color colour) 
            { 
                this.data = data; 
                this.colour = colour; 
            }
        }

        public Node root { get; set; } // Root node of the tree (both reference & pointer)

        public MemoryDump Dump;

        private string path;

        public RedBlackTree(string filePath)
        {
            this.Dump = new MemoryDump();

            this.path = filePath;

            if (!File.Exists(path))
            {
                var fs = File.Create(path);
                fs.Close();
            }

            using (var sr = new StreamReader(path))
            {
                var data = sr.ReadLine();
                try
                {
                    Dump = JsonSerializer.Deserialize<MemoryDump>(data);
                }
                catch { }
            }

            foreach (var item in Dump.Data)
            {
                this.InsertNoDump(item);
            }
        }

        public void DumpData()
        {
            using (var sw = new StreamWriter(path, false))
            {
                var data = JsonSerializer.Serialize<MemoryDump>(Dump);
                sw.WriteLine(data);
            }
        }

        private void LeftRotate(Node X)
        {
            Node Y = X.right; // set Y
            X.right = Y.left;//turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            if (Y != null)
            {
                Y.parent = X.parent;//link X's parent to Y
            }
            if (X.parent == null)
            {
                root = Y;
            }
            if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X; //put X on Y's left
            if (X != null)
            {
                X.parent = Y;
            }

        }

        private void RightRotate(Node Y)
        {
            // right rotate is simply mirror code from left rotate
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;//put Y on X's right
            if (Y != null)
            {
                Y.parent = X;
            }
        }

        public Node Find(int key)
        {
            try
            {
                bool isFound = false;
                Node temp = root;
                Node item = null;
                while (!isFound)
                {
                    if (temp == null)
                    {
                        break;
                    }
                    if (key < temp.data.PK)
                    {
                        temp = temp.left;
                    }
                    if (key > temp.data.PK)
                    {
                        temp = temp.right;
                    }
                    if (key == temp.data.PK)
                    {
                        isFound = true;
                        item = temp;
                    }
                }
                if (isFound)
                {
                    return temp;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public void Edit(int key, string data)
        {
            if (Find(key) == null) throw new Exception("PK is non-existent");

            var editedItem = new Data {
                PK = key,
                Content = data
            };

            bool isFound = false;
            Node temp = root;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                if (editedItem.PK < temp.data.PK)
                {
                    temp = temp.left;
                }
                if (editedItem.PK > temp.data.PK)
                {
                    temp = temp.right;
                }
                if (editedItem.PK == temp.data.PK)
                {
                    isFound = true;
                    var index = Dump.Data.IndexOf(temp.data);
                    Dump.Data[index] = editedItem;
                    temp.data = editedItem;
                }
            }
        }

        public void InsertNoDump(Data item)
        {
            Node newItem = new Node(item);
            if (root == null)
            {
                root = newItem;
                root.colour = Color.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.data.PK < X.data.PK)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.data.PK < Y.data.PK)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.colour = Color.Red; //colour the new node red

            //check for violations and fix
            
        }
        public void Insert(int key, string data)
        {
            var item = new Data {
                PK = key,
                Content = data
            };

            if (Find(item.PK) != null) throw new Exception("PK already exsts");

            this.Dump.Data.Add(item);

            InsertNoDump(item);
        }

        public void InsertFixUp(Node newItem)
        {
            while (newItem != root && newItem.parent.colour == Color.Red)
            {
                /*We have a violation*/
                if (newItem.parent == newItem.parent.parent.left)
                {
                    Node Y = newItem.parent.parent.right;
                    if (Y != null && Y.colour == Color.Red) //Case 1: uncle is red
                    {
                        newItem.parent.colour = Color.Black;
                        Y.colour = Color.Black;
                        newItem.parent.parent.colour = Color.Red;
                        newItem = newItem.parent.parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (newItem == newItem.parent.right)
                        {
                            newItem = newItem.parent;
                            LeftRotate(newItem);
                        }
                        //Case 3: recolour & rotate
                        newItem.parent.colour = Color.Black;
                        newItem.parent.parent.colour = Color.Red;
                        RightRotate(newItem.parent.parent);
                    }

                }
                else
                {
                    //mirror image of code above
                    Node X = null;

                    X = newItem.parent.parent.left;
                    if (X != null && X.colour == Color.Black)//Case 1
                    {
                        newItem.parent.colour = Color.Red;
                        X.colour = Color.Red;
                        newItem.parent.parent.colour = Color.Black;
                        newItem = newItem.parent.parent;
                    }
                    else //Case 2
                    {
                        if (newItem == newItem.parent.left)
                        {
                            newItem = newItem.parent;
                            RightRotate(newItem);
                        }
                        //Case 3: recolour & rotate
                        newItem.parent.colour = Color.Black;
                        newItem.parent.parent.colour = Color.Red;
                        LeftRotate(newItem.parent.parent);

                    }

                }
                root.colour = Color.Black;//re-colour the root black as necessary
            }
        }

        public void Delete(int key)
        {
            if (Find(key) == null) throw new Exception("PK non-exstent");

            //first find the node in the tree to delete and assign to item pointer/reference
            Node item = Find(key);
            Node X = null;
            Node Y = null;

            Dump.Data.Remove(item.data);

            if (item == null)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (item.left == null || item.right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            if (Y.left != null)
            {
                X = Y.left;
            }
            else
            {
                X = Y.right;
            }
            if (X != null)
            {
                X.parent = Y;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.left = X;
            }
            if (Y != item)
            {
                item.data = Y.data;
            }
            if (Y.colour == Color.Black)
            {
                DeleteFixUp(X);
            }

            
        }

        private void DeleteFixUp(Node X)
        {

            while (X != null && X != root && X.colour == Color.Black)
            {
                if (X == X.parent.left)
                {
                    Node W = X.parent.right;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black; //case 1
                        X.parent.colour = Color.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.colour == Color.Black && W.right.colour == Color.Black)
                    {
                        W.colour = Color.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else if (W.right.colour == Color.Black)
                    {
                        W.left.colour = Color.Black; //case 3
                        W.colour = Color.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.parent.right; //case 3
                    }
                    W.colour = X.parent.colour; //case 4
                    X.parent.colour = Color.Black; //case 4
                    W.right.colour = Color.Black; //case 4
                    LeftRotate(X.parent); //case 4
                    X = root; //case 4
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    Node W = X.parent.left;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black;
                        X.parent.colour = Color.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.colour == Color.Black && W.left.colour == Color.Black)
                    {
                        W.colour = Color.Black;
                        X = X.parent;
                    }
                    else if (W.left.colour == Color.Black)
                    {
                        W.right.colour = Color.Black;
                        W.colour = Color.Red;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.colour = X.parent.colour;
                    X.parent.colour = Color.Black;
                    W.left.colour = Color.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            if (X != null)
                X.colour = Color.Black;
        }
        
        private Node Minimum(Node X)
        {
            while (X.left.left != null)
            {
                X = X.left;
            }
            if (X.left.right != null)
            {
                X = X.left.right;
            }
            return X;
        }
        
        private Node TreeSuccessor(Node X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }
        }
    }

    class MemoryDump
    {
        public List<RedBlackTree.Data> Data { get; set; }

        public MemoryDump()
        {
            this.Data = new List<RedBlackTree.Data>();
        }
    }
}