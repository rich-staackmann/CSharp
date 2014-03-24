using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    public class TreeNode<T> : IComparable<T> where T : IComparable<T>
    {
        public TreeNode(T value)
        {
            Value = value;
        }

        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public T Value { get; set; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}
