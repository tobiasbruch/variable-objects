
using System;
using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [System.Serializable]
    public class Reference<T1, T2> : ReferenceBase where T2 : Variable<T1>
    {
        [SerializeField]
        protected T1 _value = default;
        [SerializeField]
        private T2 _variable = default;

        public bool UsesVariable { get => _variable; }

        protected Action<T1, T1> _eventValueChanged = delegate { };
        public virtual Action<T1, T1> EventValueChanged
        {
            get => _eventValueChanged;
            set
            {
                if (_eventValueChanged == null && _variable)
                {
                    _variable.EventValueChanged += OnValueChanged;
                }
                _eventValueChanged = value;
            }
        }

        public virtual T1 Value
        {
            get
            {
                if (_variable)
                {
                    return _value = _variable.Value;
                }
                else
                {
                    return _value;
                }
            }
            set
            {
                if (value == null)
                {
                    if (Value != null)
                    {
                        if (_variable)
                        {
                            _value = _variable.Value = value;
                        }
                        else
                        {
                            OnValueChanged(_value, _value = value);
                        }
                    }
                }
                else
                {
                    if (!value.Equals(Value))
                    {
                        if (_variable)
                        {
                            _value = _variable.Value = value;
                        }
                        else
                        {
                            OnValueChanged(_value, _value = value);
                        }
                    }
                }
            }
        }

        private void OnValueChanged(T1 oldValue, T1 newValue)
        {
            EventValueChanged?.Invoke(oldValue, newValue);
        }

        public sealed override object GetValueAsObject()
        {
            return Value;
        }

#if UNITY_EDITOR
    public virtual void OnValidate() { }
#endif
    }
    [System.Serializable]
    public abstract class ReferenceBase
    {
        public abstract object GetValueAsObject();
    }
}