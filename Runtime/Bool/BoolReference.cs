using System;
using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [System.Serializable]
    public sealed class BoolReference : BoolReferenceBase
    {
        public BoolReference(bool value)
        {
            _value = value;
        }
        
        #region Logic
        [SerializeField]
        private BoolReferenceBase.DepthOne[] _conditions = default;

        protected override bool GetValueMultipleConditions()
        {
            switch (_logicOperator)
            {
                case LogicalOperator.AND:
                    foreach (BoolReferenceBase.DepthOne boolRef in _conditions)
                    {
                        if (!boolRef.Value)
                        {
                            return false;
                        }
                    }
                    return true;
                case LogicalOperator.OR:
                    foreach (BoolReferenceBase.DepthOne boolRef in _conditions)
                    {
                        if (boolRef.Value)
                        {
                            return true;
                        }
                    }
                    return false;
            }
            return false;
        }

        protected override void SubscribeMultipleConditions()
        {
            foreach (BoolReferenceBase.DepthOne condition in _conditions)
            {
                condition.EventValueChanged += OnConditionChanged;
            }
        }
        #endregion

#if UNITY_EDITOR
        [NonSerialized]
        private BoolReferenceBase.DepthOne[] _oldConditions = new BoolReferenceBase.DepthOne[0];
        [NonSerialized]
        private VariableBase _oldFirstOperandVariable;
        [NonSerialized]
        private VariableBase _oldSecondOperandVariable;
        [NonSerialized]
        private bool _oldInvert;

        public override void OnValidate()
        {
            if (Application.isPlaying)
            {
                if (_oldConditions != _conditions)
                {
                    if (_eventValueChanged != null && _mode == Mode.MultipleConditions)
                    {
                        foreach (BoolReferenceBase.DepthOne condition in _oldConditions)
                        {
                            condition.EventValueChanged -= OnConditionChanged;
                        }
                        foreach (BoolReferenceBase.DepthOne condition in _conditions)
                        {
                            condition.EventValueChanged += OnConditionChanged;
                        }
                        OnConditionChanged(_value, _value);
                    }
                    _oldConditions = _conditions;
                }

                if (_oldFirstOperandVariable != _firstOperandVariable)
                {
                    if (_eventValueChanged != null && _mode == Mode.Comparison)
                    {
                        if (_oldFirstOperandVariable != null)
                        {
                            _oldFirstOperandVariable.EventValueAsObjectChanged -= OnValueAsObjectChanged;
                        }
                        if (_firstOperandVariable != null)
                        {
                            _firstOperandVariable.EventValueAsObjectChanged += OnValueAsObjectChanged;
                        }
                        _oldFirstOperandVariable = _firstOperandVariable;

                        object oldValue = null;
                        if (_oldFirstOperandVariable != null)
                        {
                            oldValue = _oldFirstOperandVariable.GetValueAsObject();
                        }
                        object newValue = null;
                        if (_firstOperandVariable != null)
                        {
                            newValue = _firstOperandVariable.GetValueAsObject();
                        }
                        if (oldValue != newValue)
                        {
                            OnValueAsObjectChanged(oldValue, newValue);
                        }
                    }
                }

                if (_oldSecondOperandVariable != _secondOperandVariable)
                {
                    if (_eventValueChanged != null && _mode == Mode.Comparison)
                    {
                        if (_oldSecondOperandVariable != null)
                        {
                            _oldSecondOperandVariable.EventValueAsObjectChanged -= OnValueAsObjectChanged;
                        }
                        if (_secondOperandVariable != null)
                        {
                            _secondOperandVariable.EventValueAsObjectChanged += OnValueAsObjectChanged;
                        }
                        object oldValue = null;
                        if(_oldSecondOperandVariable != null)
                        {
                            oldValue = _oldSecondOperandVariable.GetValueAsObject();
                        }
                        object newValue = null;
                        if(_secondOperandVariable != null)
                        {
                            newValue = _secondOperandVariable.GetValueAsObject();
                        }
                        if (oldValue != newValue) 
                        {
                            OnValueAsObjectChanged(oldValue, newValue);
                        }

                        _oldSecondOperandVariable = _secondOperandVariable;
                    }
                }

                bool invert = _invert && base.UsesVariable;
                if (_oldInvert != invert)
                {
                    bool newValue = Value;
                    bool oldValue = !newValue;
                    EventValueChanged?.Invoke(oldValue, newValue);
                    _oldInvert = _invert;
                }
            }
        }
#endif
    }

    public abstract class BoolReferenceBase : Reference<bool, BoolVariable>
    {
        /// <summary>
        /// Workaround for Unity serialization limitation. This way serialization is finite
        /// </summary>
        [System.Serializable]
        public class DepthOne : BoolReferenceBase
        {
            [SerializeField]
            private DepthTwo[] _conditions = default;

            protected override bool GetValueMultipleConditions()
            {
                switch (_logicOperator)
                {
                    case LogicalOperator.AND:
                        foreach (DepthTwo boolRef in _conditions)
                        {
                            if (!boolRef.Value)
                            {
                                return false;
                            }
                        }
                        return true;
                    case LogicalOperator.OR:
                        foreach (DepthTwo boolRef in _conditions)
                        {
                            if (boolRef.Value)
                            {
                                return true;
                            }
                        }
                        return false;
                }
                return false;
            }

            protected override void SubscribeMultipleConditions()
            {
                foreach (DepthTwo condition in _conditions)
                {
                    condition.EventValueChanged += OnConditionChanged;
                }
            }
        }
        [System.Serializable]
        public class DepthTwo : BoolReferenceBase
        {
            [SerializeField]
            private DepthSealed[] _conditions = default;

            protected override bool GetValueMultipleConditions()
            {
                switch (_logicOperator)
                {
                    case LogicalOperator.AND:
                        foreach (DepthSealed boolRef in _conditions)
                        {
                            if (!boolRef.Value)
                            {
                                return false;
                            }
                        }
                        return true;
                    case LogicalOperator.OR:
                        foreach (DepthSealed boolRef in _conditions)
                        {
                            if (boolRef.Value)
                            {
                                return true;
                            }
                        }
                        return false;
                }
                return false;
            }

            protected override void SubscribeMultipleConditions()
            {
                foreach (DepthSealed condition in _conditions)
                {
                    condition.EventValueChanged += OnConditionChanged;
                }
            }
        }
        [System.Serializable]
        public class DepthSealed : BoolReferenceBase
        {
            protected override bool GetValueMultipleConditions()
            {
                return false;
            }

            protected override void SubscribeMultipleConditions()
            {
            }
        }

        [SerializeField]
        protected Mode _mode = Mode.Value;
        [SerializeField]
        protected bool _invert = false;

        public override bool Value
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Comparison:
                        _value = GetValueComparison();
                        return _invert ? !_value : _value;
                    case Mode.MultipleConditions:
                        _value = GetValueMultipleConditions();
                        return _invert ? !_value : _value;
                    default:
                        bool invert = _invert && base.UsesVariable;
                        return invert ? !base.Value : base.Value;
                }
            }
            set
            {
                switch (_mode)
                {
                    case Mode.Comparison:
                        break;
                    case Mode.MultipleConditions:
                        break;
                    default:
                        bool invert = _invert && base.UsesVariable;
                        base.Value = invert ? !value : value;
                        break;
                }
            }
        }

        public override Action<bool, bool> EventValueChanged
        {
            get => base.EventValueChanged;
            set
            {
                switch (_mode)
                {
                    case Mode.Comparison:
                        if (_eventValueChanged == null)
                        {
                            _firstOperandVariable.EventValueAsObjectChanged += OnValueAsObjectChanged;
                            if (_secondOperandType == OperandType.Variable)
                            {
                                _secondOperandVariable.EventValueAsObjectChanged += OnValueAsObjectChanged;
                            }
                        }
                        _eventValueChanged = value;
                        break;
                    case Mode.MultipleConditions:
                        if (_eventValueChanged == null)
                        {
                            SubscribeMultipleConditions();
                        }
                        _eventValueChanged = value;
                        break;
                    default:
                        base.EventValueChanged = value;
                        break;
                }
            }
        }

        protected void OnValueAsObjectChanged(object oldValueAsObject, object newValueAsObject)
        {
            bool oldValue = _value;
            bool newValue = Value;

            if (oldValue != newValue)
            {
                EventValueChanged?.Invoke(oldValue, newValue);
            }
        }
        protected void OnConditionChanged(bool oldConditionValue, bool newConditionValue)
        {
            OnValueAsObjectChanged(oldConditionValue, newConditionValue);
        }

        #region Comparison
        [SerializeField]
        protected VariableBase _firstOperandVariable = default;

        [SerializeField]
        protected Comparator _comparator = Comparator.Equals;

        [SerializeField]
        protected OperandType _secondOperandType = OperandType.Variable;
        [SerializeField]
        protected VariableBase _secondOperandVariable = default;
        [SerializeField]
        protected float _secondOperandFloat = default;
        [SerializeField]
        protected int _secondOperandInt = default;
        [SerializeField]
        protected string _secondOperandString = default;

        private bool GetValueComparison()
        {
            object firstValue = null;
            if (_firstOperandVariable != null)
            {
                firstValue = _firstOperandVariable.GetValueAsObject();
            }
            else
            {
                return false;
            }
            object secondValue = null;
            switch (_secondOperandType)
            {
                case OperandType.Variable:
                    if (_secondOperandVariable != null)
                    {
                        secondValue = _secondOperandVariable.GetValueAsObject();
                    }
                    break;
                case OperandType.Float:
                    secondValue = _secondOperandFloat;
                    break;
                case OperandType.Int:
                    secondValue = _secondOperandInt;
                    break;
                case OperandType.String:
                    secondValue = _secondOperandString;
                    break;
            }

            if (firstValue is IComparable firstComparable && secondValue is IComparable secondComparable)
            {
                switch (_comparator)
                {
                    case Comparator.Equals:
                        return firstComparable.CompareTo(secondComparable) == 0;
                    case Comparator.NotEquals:
                        return firstComparable.CompareTo(secondComparable) != 0;
                    case Comparator.IsGreater:
                        return firstComparable.CompareTo(secondComparable) > 0;
                    case Comparator.IsLesser:
                        return firstComparable.CompareTo(secondComparable) < 0;
                    case Comparator.IsGreaterEquals:
                        return firstComparable.CompareTo(secondComparable) >= 0;
                    case Comparator.IsLesserEquals:
                        return firstComparable.CompareTo(secondComparable) <= 0;
                }
            }
            else
            {
                switch (_comparator)
                {
                    case Comparator.Equals:
                        return firstValue.Equals(secondValue);
                    case Comparator.NotEquals:
                        return !firstValue.Equals(secondValue);
                }
            }
            return false;
        }

        public enum OperandType
        {
            Variable,
            Float,
            Int,
            String
        }
        protected enum Comparator
        {
            Equals,
            NotEquals,
            IsGreater,
            IsLesser,
            IsGreaterEquals,
            IsLesserEquals
        }
        #endregion

        #region Logic
        [SerializeField]
        protected LogicalOperator _logicOperator = LogicalOperator.AND;

        protected abstract bool GetValueMultipleConditions();
        protected abstract void SubscribeMultipleConditions();
        protected enum LogicalOperator
        {
            AND,
            OR
        }
        #endregion

        public enum Mode
        {
            Value,
            Comparison,
            MultipleConditions
        }
    }
}