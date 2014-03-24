using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    class DoublyLinkedList<T> : System.Collections.Generic.ICollection<T>
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
                Tail = Head;
            else //we need to assign the old head's previous node to the new head
                temp.Previous = Head;
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
            {
                Tail.Next = node;
                node.Previous = Tail; //assign the old tail as the previous for the new tail
            }

            Tail = node;
            Count++;
        }
        #endregion

        #region remove
        //we no longer need to iterate over the list like in a singly linked list
        //this is a CONSTANT time operation now
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
                    Tail.Previous.Next = null; //assign null to the new tail's next
                    Tail = Tail.Previous; //reassign tail to the node before itself
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
                else
                    Head.Previous = null; //assign null to the new head's previous
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
                        else //node is in the middle somewhere
                            current.Next.Previous = prev;
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

        public IEnumerator<T> GetEnumerator()
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
