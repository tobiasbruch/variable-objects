using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    public class CollectionObject<T> : ScriptableObject, IEnumerable<T>
    {
        [SerializeField]
        private List<T> _items = new List<T>();
        [SerializeField]
        private bool _reset = false;

        public Action<T> EventItemAdded = delegate{};
        public Action<T> EventItemRemoved = delegate{};
        
        public int Count { get => _items.Count; }
        
        private void OnEnable()
        {
            if (_reset)
            {
                _items = new List<T>();
            }
        }

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

        public bool Contains(T item)
        {
            return _items.Contains(item);
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