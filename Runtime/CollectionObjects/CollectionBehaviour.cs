using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    public abstract class CollectionBehaviour<T1, T2> : MonoBehaviour where T2 : CollectionObject<T1>
    {
        [SerializeField]
        private T2 _collection = default;

        protected virtual void OnEnable()
        {
            foreach (T1 item in _collection)
            {
                OnItemAdded(item);
            }
            _collection.EventItemAdded += OnItemAdded;
            _collection.EventItemRemoved += OnItemRemoved;
        }
        protected virtual void OnDisable()
        {
            foreach (T1 item in _collection)
            {
                OnItemRemoved(item);
            }
            _collection.EventItemAdded -= OnItemAdded;
            _collection.EventItemRemoved -= OnItemRemoved;
        }

        protected abstract void OnItemAdded(T1 item);
        protected abstract void OnItemRemoved(T1 item);
    }
}