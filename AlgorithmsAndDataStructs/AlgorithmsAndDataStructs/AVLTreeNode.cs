using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
   public class AVLTreeNode<T> : IComparable<T> where T : IComparable<T>
    {
       public AVLTreeNode(T value, AVLTreeNode<T> parent, AVLTree<T> tree)
       {
           this.Value = value;
           this.Parent = parent;
       }

       public AVLTreeNode<T> Left { get; private set; }
       public AVLTreeNode<T> Right { get; private set; }
       public AVLTreeNode<T> Parent { get; private set; }
       public T Value { get; private set; }

       public int CompareTo(T other)
       {
           return this.Value.CompareTo(other);
       }

       //balancing methods
       //internal void Balance();
       //private void LeftRotation();
       //private void RightRotation();
       //private void LeftRightRotation();
       //private void RightLeftRotation();

       //helpers
       //private int MaxChildHeight(AVLTreeNode<T> node);
       //private int LeftHeight { get; }
       //private int RightHeight { get; }
       //private int BalanceFactor { get; }
    }
}
