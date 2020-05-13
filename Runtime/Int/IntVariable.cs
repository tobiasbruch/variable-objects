using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [CreateAssetMenu(fileName = "Int", menuName = "Variables/Int", order = 100)]
    public class IntVariable : Variable<int>
    {
        public void Add(int value)
        {
            Value += value;
        }
        public void Subtract(int value)
        {
            Value -= value;
        }
        public void Multiply(int value)
        {
            Value *= value;
        }
        public void Divide(int value)
        {
            Value /= value;
        }
    }
}