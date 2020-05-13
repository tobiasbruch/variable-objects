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
    }
}