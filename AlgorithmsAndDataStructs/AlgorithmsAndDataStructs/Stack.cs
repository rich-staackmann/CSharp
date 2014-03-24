using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructs
{
    public class Stack<T> : IEnumerable<T>
    {
        private System.Collections.Generic.LinkedList<T> list = new System.Collections.Generic.LinkedList<T>();
        public int Count { get { return list.Count; } }

        public void Push(T item)
        {
            list.AddFirst(item);
        }

        public T Pop()
        {
            if (list.Count == 0)
                throw new InvalidOperationException("The stack is empty.");

            T value = list.First.Value;
            list.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (list.Count == 0)
                throw new InvalidOperationException("The stack is empty.");

            return list.First.Value;
        }

        public void Clear()
        {
            list.Clear();
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
