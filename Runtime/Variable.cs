using System;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    public class Variable<T> : VariableBase
    {
        [SerializeField]
        private bool _resetToDefaultValue = true;
        [SerializeField]
        private T _defaultValue = default;
        [SerializeField]
        protected T _value = default;
        public virtual T Value
        {
            get => _value;
            set
            {
                if (value == null)
                {
                    if (_value != null)
                    {
                        T oldValue = _value;
                        EventValueChanged(_value, _value = value);
                        EventValueAsObjectChanged(oldValue, _value);
                    }
                }
                else if (!value.Equals(_value))
                {
                    T oldValue = _value;
                    EventValueChanged(_value, _value = value);
                    EventValueAsObjectChanged(oldValue, _value);
                }
#if UNITY_EDITOR
            _lastSetValue = value;
#endif
            }
        }
        public Action<T, T> EventValueChanged = delegate { };


        private void OnEnable()
        {
            ResetToDefaultValue();
        }

        public void ResetToDefaultValue()
        {
            if (_resetToDefaultValue)
            {
                Value = _defaultValue;
            }
        }

        public override sealed object GetValueAsObject()
        {
            return Value;
        }

#if UNITY_EDITOR
    private T _lastSetValue = default;
    private void OnValidate()
    {
        if (_lastSetValue == null)
        {
            if (_value != null)
            {
                if(!_value.Equals(Value))
                {
                    Value = _value;
                }
                else
                {
                    EventValueChanged(_lastSetValue, _value);
                    EventValueAsObjectChanged(_lastSetValue, _value);
                }
            }
        }
        else if (!_lastSetValue.Equals(_value))
        {
            if(!Value.Equals(_value))
            {
                Value = _value;
            }
            else
            {
                EventValueChanged(_lastSetValue, _value);
                EventValueAsObjectChanged(_lastSetValue, _value);
            }
        }

        _lastSetValue = _value;
    }
#endif
    }
    /// <summary>
    /// Do not extend. This class is a workaround for creating generic custom inspectors since Unity does not support that
    /// </summary>
    public abstract class VariableBase : ScriptableObject
    {
        public Action<object, object> EventValueAsObjectChanged = delegate { };
        public abstract object GetValueAsObject();
    }
}