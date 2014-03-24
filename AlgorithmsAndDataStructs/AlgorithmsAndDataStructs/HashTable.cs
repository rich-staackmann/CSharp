using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    public struct KeyValue<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }

    public class HashTable<K, V>
    {
        LinkedList<KeyValue<K, V>>[] array;
        int count;
        const double maxFillFactor = 0.75;
        private int maxItemsAtSize;
        //ctor
        public HashTable(int size)
        {
            array = new LinkedList<KeyValue<K, V>>[size];
            count = 0;
            maxItemsAtSize = array.Length - (int)(array.Length * maxFillFactor);
        }
        //hashing func....very generic
        private int GetIndex(K key)
        {
            int index = key.GetHashCode() % array.Length;
            return Math.Abs(index);
        }
        //finds the value for a certain key, returns default if nothing is found
        public V Find(K key)
        {
            int position = GetIndex(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            foreach (KeyValue<K, V> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }

            return default(V);
        }
        //wrapper for adding items
        public void Add(K key, V value)
        {
            if (count >= maxItemsAtSize)
            {
                //create temp arr that is twice as big as current array
                LinkedList<KeyValue<K, V>>[] tempArr = new LinkedList<KeyValue<K, V>>[array.Length * 2];
                foreach (LinkedList<KeyValue<K, V>> list in array)
                {
                    foreach (KeyValue<K, V> pair in list)
                        Add(pair.Key, pair.Value, tempArr);
                }
                array = tempArr;
                maxItemsAtSize = array.Length - (int)(array.Length * maxFillFactor);
            }
            Add(key, value, this.array);
        }
        //must specify array we want to add to, used for growing array as well as normal adding
        private void Add(K key, V value, LinkedList<KeyValue<K, V>>[] array)
        {
            int position = GetIndex(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(array, position);
            KeyValue<K, V> item = new KeyValue<K, V>() { Key = key, Value = value };
            linkedList.AddLast(item);
            count++;
        }

        public void Remove(K key)
        {
            int position = GetIndex(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            bool itemFound = false;
            KeyValue<K, V> foundItem = default(KeyValue<K, V>);
            foreach (KeyValue<K, V> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }

            if (itemFound)
            {
                linkedList.Remove(foundItem);
            }
        }
        //Gets a chain of KeyValues if one exists, otherwise
        //this methods creates a new chain
        protected LinkedList<KeyValue<K, V>> GetLinkedList(int position)
        {
            LinkedList<KeyValue<K, V>> linkedList = array[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<K, V>>();
                array[position] = linkedList;
            }

            return linkedList;
        }
        //helper, requires an array param. I use this in the add method
        //growing the array requires building a temp array, this helps with that
        protected LinkedList<KeyValue<K, V>> GetLinkedList(LinkedList<KeyValue<K, V>>[] array, int position)
        {
            LinkedList<KeyValue<K, V>> linkedList = array[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<K, V>>();
                array[position] = linkedList;
            }

            return linkedList;
        }
    }
}
