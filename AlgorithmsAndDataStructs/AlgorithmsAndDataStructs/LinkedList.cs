using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    public class LinkedList<T> : System.Collections.Generic.ICollection<T>
    {
        public LinkedListNode<T> Head
        {
            get;
            private set;
        }
        public LinkedListNode<T> Tail
        {
            get;
            private set;
        }

        #region add
        //wrapper
        public void AddFirst(T value)
        {
            AddFirst(new LinkedListNode<T>(value)); 
        }
        //in a linked list this operation is done in CONSTANT time
        //if we were using an array, we would need to copy the array to move all the data to the right
        public void AddFirst(LinkedListNode<T> node)
        {
            //Save head so we don't lose it
            LinkedListNode<T> temp = Head;

            //point head to our new node
            Head = node;

            Head.Next = temp;

            Count++;

            //if the list is only one item then the tail and head point to the same node
            if (Count == 1)
            {
                Tail = Head;
            }
        }

        //wrapper
        public void AddLast(T value)
        {
            AddLast(new LinkedListNode<T>(value));
        }
        //again this is CONSTANT time
        public void AddLast(LinkedListNode<T> node)
        {
            //if the list was empty, then head and tail will point to the same node
            if (Count == 0)
                Head = node;
            else
                Tail.Next = node;

            Tail = node;
            Count++;
        }
        #endregion

        #region remove
        //we have to iterate over the entire list to do this operation, so time is N
        public void RemoveLast()
        {
            if (Count != 0)
            {
                if (Count == 1)
                {
                    Head = null;
                    Tail = null;
                }
                else
                {
                    //we need to iterate to the second to last node, and the set it's Next to null
                    //tail then becomes the second to last node
                    LinkedListNode<T> current = Head;
                    while (current.Next != Tail)
                        current = current.Next;
                    current.Next = null;
                    Tail = current;
                }
                Count--;
            }
        }

        //CONSTANT time operation
        public void RemoveFirst()
        {
            if (Count != 0)
            {
                Head = Head.Next;
                Count--;

                if (Count == 0)
                    Tail = null;
            }
        }
        #endregion
 
        #region icollection
        public void Add(T item)
        {
            AddFirst(item);
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            //several cases to worry about
            //1) empty list - do nothing
            //2) Single node list (prev will be null)
            //3) many node list
            //  a) node to remove is first
            //  b) node to remove is in middle or last

            LinkedListNode<T> prev = null;
            LinkedListNode<T> current = Head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    if (prev != null) //this means the list has many nodes
                    {
                        //case 3b
                        prev.Next = current.Next;
                        //node was at end so update tail
                        if (current.Next == null)
                            Tail = prev;
                        Count--;
                    }
                    else
                        RemoveFirst(); //case 2 or 3a
                    return true;
                }
                prev = current;
                current = current.Next;
            }
            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.Generic.IEnumerable<T>)this).GetEnumerator();
        }
        #endregion
    }
}
