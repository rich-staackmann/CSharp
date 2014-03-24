using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    public class Queue<T> : IEnumerable<T>
    {
        System.Collections.Generic.LinkedList<T> list = new System.Collections.Generic.LinkedList<T>();
        public int Count { get { return list.Count; } }

        public void Enqueue(T item)
        {
            list.AddLast(item);
        }

        public T Dequeue()
        {
            if (list.Count == 0)
                throw new InvalidOperationException();
            T value = list.First.Value;
            list.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (list.Count == 0)
                throw new InvalidOperationException();
            return list.First.Value;
        }

        public void Clear()
        {
            list.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator(); 
        }
    }
}
