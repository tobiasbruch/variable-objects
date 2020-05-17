using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [CreateAssetMenu(fileName = "Int", menuName = "Variables/Int", order = 100)]
    public class IntVariable : Variable<int>
    {
        public int Add(int value)
        {
            return Value += value;
        }
        public int Subtract(int value)
        {
            return Value -= value;
        }
        public int Multiply(int value)
        {
            return Value *= value;
        }
        public int Divide(int value)
        {
            return Value /= value;
        }
        
        public int Add(IntVariable variable)
        {
            return Add(variable.Value);
        }
        public int Subtract(IntVariable variable)
        {
            return Subtract(variable.Value);
        }
        public int Multiply(IntVariable variable)
        {
            return Multiply(variable.Value);
        }
        public int Divide(IntVariable variable)
        {
            return Divide(variable.Value);
        }
    }
}