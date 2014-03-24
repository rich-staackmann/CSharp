using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    //everything in a BST must be compared to other values, so types need to be comparable
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private TreeNode<T> head;
        private int count;

        #region add
        //wrapper for adding a value
        public void Add(T value)
        {
            //if tree is empty, then we add the root
            if (head == null)
                head = new TreeNode<T>(value);
            else
                AddTo(head, value);
            count++;
        }
        //true adding method
        private void AddTo(TreeNode<T> node, T value)
        {
            //case 1) value is less than current node
            if (value.CompareTo(node.Value) < 0)
            {
                //if there is no left child then make one
                if (node.Left == null)
                    node.Left = new TreeNode<T>(value);
                else//there is a left child so we keep recursing
                    AddTo(node.Left, value);
            }
            //case 2) value is greater or equal to current
            else
            {
                //if there is no right child then make one
                if (node.Right == null)
                    node.Right = new TreeNode<T>(value);
                else//there is a right child so we keep recursing
                    AddTo(node.Right, value);
            }
        }
        #endregion

        #region helpers
        //uses another help to find values
        public bool Contains(T value)
        {
            TreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }
        //remember, remove needs us to keep track of the parent
        //this helper function does just that
        private TreeNode<T> FindWithParent(T value, out TreeNode<T> parent)
        {
            TreeNode<T> current = head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);

                if (result < 0) //value is less, go left
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result > 0) //value is more, go right
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break; //we have a match
                }
            }
            return current;
        }
        #endregion

        #region remove
        public bool Remove(T value)
        {
            TreeNode<T> current, parent;
            current = FindWithParent(value, out parent);

            if (current == null) //didnt find value to remove, return false
                return false;

            count--;

            //case 1, no right child
            if (current.Right == null)
            {
                if (parent == null) //we are removing root node
                    head = current.Left;
                else
                {
                    int result = parent.CompareTo(current.Value);
                    //if parent is greater than current
                    //make the current left child a left child of parent
                    if (result > 0)
                        parent.Left = current.Left;
                    //if the parent is less than current, make current left child a right child of parent
                    else if (result < 0)
                        parent.Right = current.Left;
                }
            }
            //case 2, no left child of the right child
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left; //reassign parent for left node 

                if (parent == null) //we are removing root node
                    head = current.Right;
                else
                {
                    int result = parent.CompareTo(current.Value);
                    //if parent is greater than current
                    //make the current right child a left child of parent
                    if (result > 0)
                        parent.Left = current.Right;
                    //if the parent is less than current, make current right child a right child of parent
                    else if (result < 0)
                        parent.Right = current.Right;
                }
            }
            //case 3, right child with a left child
            else
            {
                TreeNode<T> leftmost = current.Right.Left;
                TreeNode<T> leftmostparent = current.Right;

                //iterate until we find the left most child, and its parent
                while (leftmost.Left != null)
                {
                    leftmostparent = leftmost;
                    leftmost = leftmost.Left;
                }
                //the parents left subtree becomes leftmost's right subtree
                leftmostparent.Left = leftmost.Right;
                //assign leftmost's left and right as current's children
                leftmost.Right = current.Right;
                leftmost.Left = current.Left;

                if (parent == null)
                    head = leftmost;
                else
                {
                    int result = parent.CompareTo(current.Value);
                    //if parent is greater than current
                    //make leftmost the parent's left child
                    if (result > 0)
                        parent.Left = leftmost;
                    //if the parent is less than current, make leftmost the parent's right child
                    else if (result < 0)
                        parent.Right = leftmost;
                }
            }
            return true;
        }
        #endregion

        #region traversals
        public void PreOrderTraversal(Action<T> action, TreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }

        public void PostOrderTraversal(Action<T> action, TreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }

        public void InOrderTraversal(Action<T> action, TreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }
        #endregion

        #region enumerable
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
