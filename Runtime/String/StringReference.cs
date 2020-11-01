using System;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    [System.Serializable]
    public class StringReference : Reference<string, StringVariable>
    {
        [SerializeField]
        private Mode _mode = Mode.Value;
        [SerializeField]
        [TextArea(5, 10)]
        private string _format = "{0}";
        [SerializeField]
        private FormatArgument[] _formatArguments = new FormatArgument[1];

        private object[] GetFormatArgs()
        {
            object[] args = new object[_formatArguments.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = _formatArguments[i].GetArgument();
            }
            return args;
        }
        public override string Value
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Format:
                        _value = string.Format(_format, GetFormatArgs());
                        return _value;
                    default:
                        return base.Value;
                }
            }
            set
            {
                switch (_mode)
                {
                    case Mode.Format:
                        break;
                    default:
                        base.Value = value;
                        break;
                }
            }
        }

        public override Action<string, string> EventValueChanged
        {
            get => base.EventValueChanged;
            set
            {
                switch (_mode)
                {
                    case Mode.Format:
                        if (_eventValueChanged == null)
                        {
                            for (int i = 0; i < _formatArguments.Length; i++)
                            {
                                if(_formatArguments[i].IsVariable)
                                {
                                    _formatArguments[i].EventValueChanged += OnFormatArgumentValueChanged;
                                }
                            }
                        }
                        _eventValueChanged = value;
                        break;
                    default:
                        base.EventValueChanged = value;
                        break;
                }
            }
        }

        protected void OnFormatArgumentValueChanged(object oldValueAsObject, object newValueAsObject)
        {
            string oldValue = _value;
            string newValue = Value;

            if (oldValue != newValue)
            {
                EventValueChanged?.Invoke(oldValue, newValue);
                _value = newValue;
            }
        }

#if UNITY_EDITOR
        [NonSerialized]
        private FormatArgument[] _oldFormatArguments = new FormatArgument[0];

        public override void OnValidate()
        {
            if (Application.isPlaying)
            {
                if (_oldFormatArguments != _formatArguments)
                {
                    if (_eventValueChanged != null && _mode == Mode.Format)
                    {
                        for (int i = 0; i < _oldFormatArguments.Length; i++)
                        {
                            _oldFormatArguments[i].EventValueChanged -= OnFormatArgumentValueChanged;
                        }
                        for (int i = 0; i < _oldFormatArguments.Length; i++)
                        {
                            _formatArguments[i].EventValueChanged += OnFormatArgumentValueChanged;
                        }
                        OnFormatArgumentValueChanged(default, default);
                    }
                    _oldFormatArguments = _formatArguments;
                }
            }
        }
#endif


        [System.Serializable]
        public struct FormatArgument
        {
            [SerializeField]
            private string _string;
            [SerializeField]
            private VariableBase _variable;

            public bool IsVariable { get => _variable; }
            public Action<object, object> EventValueChanged { get => _variable.EventValueAsObjectChanged; set => _variable.EventValueAsObjectChanged = value; }

            public object GetArgument()
            {
                if(_variable)
                {
                    return _variable.GetValueAsObject();
                }
                return _string;
            }
        }

        public enum Mode
        {
            Value,
            Format
        }
    }
}