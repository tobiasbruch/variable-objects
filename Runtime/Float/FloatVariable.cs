using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [CreateAssetMenu(fileName = "Float", menuName = "Variables/Float", order = 100)]
    public class FloatVariable : Variable<float>
    {
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