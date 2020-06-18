using System;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    [System.Serializable]
    public class FloatReference : FloatReferenceBase
    {
        [SerializeField]
        private MathOperation _operation = default;
        [SerializeField]
        private FloatReferenceBase.DepthOne _operand = default;

        public override float Value
        {
            get
            {
                switch (_operation)
                {
                    case MathOperation.Add:
                        return _value + _operand.Value;
                    case MathOperation.Subtract:
                        return _value - _operand.Value;
                    case MathOperation.Multiply:
                        return _value * _operand.Value;
                    case MathOperation.Divide:
                        return _value / _operand.Value;
                    case MathOperation.Modulo:
                        return _value % _operand.Value;
                    default:
                        return base.Value;
                }
            }
            set
            {
                switch (_operation)
                {
                    case MathOperation.Add:
                        break;
                    case MathOperation.Subtract:
                        break;
                    case MathOperation.Multiply:
                        break;
                    case MathOperation.Divide:
                        break;
                    case MathOperation.Modulo:
                        break;
                    default:
                        base.Value = value;
                        break;
                }
            }
        }

        public override Action<float, float> EventValueChanged
        {
            get => base.EventValueChanged;
            set
            {
                if (_eventValueChanged == null)
                {
                    _operand.EventValueChanged += OnOperandValueChanged;
                }
                base.EventValueChanged = value;
            }
        }


    }
    [System.Serializable]
    public class FloatReferenceBase : Reference<float, FloatVariable>
    {
        [System.Serializable]
        public class DepthOne : FloatReferenceBase
        {
            [SerializeField]
            private MathOperation _operation = default;
            [SerializeField]
            private DepthTwo _operand = default;

            public override float Value
            {
                get
                {
                    switch (_operation)
                    {
                        case MathOperation.Add:
                            return _value + _operand.Value;
                        case MathOperation.Subtract:
                            return _value - _operand.Value;
                        case MathOperation.Multiply:
                            return _value * _operand.Value;
                        case MathOperation.Divide:
                            return _value / _operand.Value;
                        case MathOperation.Modulo:
                            return _value % _operand.Value;
                        default:
                            return base.Value;
                    }
                }
                set
                {
                    switch (_operation)
                    {
                        case MathOperation.Add:
                            break;
                        case MathOperation.Subtract:
                            break;
                        case MathOperation.Multiply:
                            break;
                        case MathOperation.Divide:
                            break;
                        case MathOperation.Modulo:
                            break;
                        default:
                            base.Value = value;
                            break;
                    }
                }
            }

            public override Action<float, float> EventValueChanged
            {
                get => base.EventValueChanged;
                set
                {
                    if (_eventValueChanged == null)
                    {
                        _operand.EventValueChanged += OnOperandValueChanged;
                    }
                    base.EventValueChanged = value;
                }
            }
        }
        [System.Serializable]
        public class DepthTwo : FloatReferenceBase
        {
            [SerializeField]
            private MathOperation _operation = default;
            [SerializeField]
            private DepthSealed _operand = default;

            public override float Value
            {
                get
                {
                    switch (_operation)
                    {
                        case MathOperation.Add:
                            return _value + _operand.Value;
                        case MathOperation.Subtract:
                            return _value - _operand.Value;
                        case MathOperation.Multiply:
                            return _value * _operand.Value;
                        case MathOperation.Divide:
                            return _value / _operand.Value;
                        case MathOperation.Modulo:
                            return _value % _operand.Value;
                        default:
                            return base.Value;
                    }
                }
                set
                {
                    switch (_operation)
                    {
                        case MathOperation.Add:
                            break;
                        case MathOperation.Subtract:
                            break;
                        case MathOperation.Multiply:
                            break;
                        case MathOperation.Divide:
                            break;
                        case MathOperation.Modulo:
                            break;
                        default:
                            base.Value = value;
                            break;
                    }
                }
            }

            public override Action<float, float> EventValueChanged
            {
                get => base.EventValueChanged;
                set
                {
                    if (_eventValueChanged == null)
                    {
                        _operand.EventValueChanged += OnOperandValueChanged;
                    }
                    base.EventValueChanged = value;
                }
            }
        }
        [System.Serializable]
        public class DepthSealed : FloatReferenceBase
        {

        }

        protected void OnOperandValueChanged(float oldOperandValue, float newOperandValue)
        {
            float oldValue = _value;
            float newValue = Value;
            if (oldValue != newValue)
            {
                EventValueChanged?.Invoke(oldValue, newValue);
            }
        }
        protected enum MathOperation
        {
            None,
            Add,
            Subtract,
            Multiply,
            Divide,
            Modulo
        }
    }
}