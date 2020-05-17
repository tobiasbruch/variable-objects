using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [CreateAssetMenu(fileName = "Float", menuName = "Variables/Float", order = 100)]
    public class FloatVariable : Variable<float>
    {
        public float Add(float value)
        {
            return Value += value;
        }
        public float Subtract(float value)
        {
            return Value -= value;
        }
        public float Multiply(float value)
        {
            return Value *= value;
        }
        public float Divide(float value)
        {
            return Value /= value;
        }
        
        public float Add(FloatVariable variable)
        {
            return Add(variable.Value);
        }
        public float Subtract(FloatVariable variable)
        {
            return Subtract(variable.Value);
        }
        public float Multiply(FloatVariable variable)
        {
            return Multiply(variable.Value);
        }
        public float Divide(FloatVariable variable)
        {
            return Divide(variable.Value);
        }
    }
}