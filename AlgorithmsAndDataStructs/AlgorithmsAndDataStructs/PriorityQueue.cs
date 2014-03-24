using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    public class PriorityQueue<T> : IEnumerable<T> where T: IComparable<T>
    {
        System.Collections.Generic.LinkedList<T> list = new System.Collections.Generic.LinkedList<T>();
        public int Count {get {return list.Count();}}

        public void Enqueue(T item)
        {
            //if list is empty, just add the item
            if (list.Count == 0)
                list.AddLast(item);
            else
            {
                //find proper insert point
                var current = list.First;
                //iterate while we arent at the end of the list
                //and current is greater than the value being added
                while (current != null && current.Value.CompareTo(item) > 0)
                    current = current.Next;
                //we got to the end of the list, no values were smaller
                if (current == null)
                    list.AddLast(item);
                else
                    list.AddBefore(current, item); //we found an item that was smaller
            }
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
