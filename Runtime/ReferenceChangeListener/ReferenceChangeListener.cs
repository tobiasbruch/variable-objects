using UnityEngine;
using UnityEngine.Events;

namespace TobiasBruch.VariableObjects
{
    public abstract class ReferenceChangeListener<T1, T2, T3> : ReferenceBehaviour<T1, T2, T3> where T3 : Reference<T1, T2> where T2 : Variable<T1>
    {
        [SerializeField]
        private UnityEvent<T1> _onChange = default;

        protected override void OnValueChanged(T1 oldValue, T1 newValue)
        {
            _onChange.Invoke(newValue);
        }
    }

    public abstract class ReferenceChangeListener<T> : ReferenceChangeListener<T, Variable<T>, Reference<T, Variable<T>>> { }
}