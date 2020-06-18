using System;
using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [CreateAssetMenu(fileName = "Float", menuName = "Variables/Float", order = 100)]
    public class FloatVariable : Variable<float>
    {
        [SerializeField]
        protected bool _clamp = false;
        [SerializeField]
        private float _minValue = 0f;
        [SerializeField]
        private float _maxValue = 1f;

        public override float Value
        {
            get
            {
                float value = base.Value;
                if(_clamp)
                {
                    return Mathf.Clamp(value, _minValue, _maxValue);
                }
                return value;
            }
            set
            {
                if (_clamp)
                {
                    base.Value = Mathf.Clamp(value, _minValue, _maxValue);
                }
                else
                {
                    base.Value = value;
                }
            }
        }
        public void Add(float value)
        {
            Value += value;
        }
        public void Subtract(float value)
        {
            Value -= value;
        }
        public void Multiply(float value)
        {
            Value *= value;
        }
        public void Divide(float value)
        {
            Value /= value;
        }
        
        public void Add(FloatVariable variable)
        {
            Add(variable.Value);
        }
        public void Subtract(FloatVariable variable)
        {
            Subtract(variable.Value);
        }
        public void Multiply(FloatVariable variable)
        {
            Multiply(variable.Value);
        }
        public void Divide(FloatVariable variable)
        {
            Divide(variable.Value);
        }
    }
}