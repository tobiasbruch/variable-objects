using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    public class Collection<T> : ScriptableObject, IEnumerable<T>
    {
        [SerializeField]
        private List<T> _items = new List<T>();
        [SerializeField]
        private Action<T> EventItemAdded = delegate{};
        [SerializeField]
        private Action<T> EventItemRemoved = delegate{};
        
        public int Count { get => _items.Count; }
        
        public void Add(T item)
        {
            _items.Add(item);
            EventItemAdded(item);
        }
        public bool Remove(T item)
        {
            bool result = _items.Remove(item);
            if(result)
            {
                EventItemRemoved(item);
            }
            return result;
        }
        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}