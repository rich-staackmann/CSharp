using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndDataStructs
{
    public class Set<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _items = new List<T>();
        public int Count { get { return _items.Count; } }

        #region ctor
        public Set()
        {

        }

        public Set(IEnumerable<T> items)
        {
            this.AddRange(items);
        }
        #endregion

        #region add
        public void Add(T item)
        {
            //no duplicates allowed in our set
            if (this.Contains(item))
                throw new InvalidOperationException("Item already in Set");

            _items.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                this.Add(item);
        }

        private void AddRangeSkipDuplicates(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                if(!this.Contains(item)) //we want to silently skip duplicates, not throw
                    this.Add(item);
            }
        }
        #endregion

        #region remove
        public bool Remove(T item)
        {
            return _items.Remove(item);
        }
        #endregion

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        #region ienumerable
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion

        #region setmethods
        public Set<T> Union(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);
            result.AddRangeSkipDuplicates(other._items);
            return result;
        }

        public Set<T> Intersection(Set<T> other)
        {
            Set<T> result = new Set<T>();

            foreach (T item in _items)
            {
                if (other._items.Contains(item))
                    result.Add(item); 
            }

            return result;
        }

        public Set<T> Difference(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);

            foreach (T item in other._items)
                result.Remove(item);

            return result;
        }

        public Set<T> SymmetricDifference(Set<T> other)
        {
            Set<T> intersect = this.Intersection(other);
            Set<T> union = this.Union(other);

            return union.Difference(intersect);
        }
        #endregion
    }
}
