using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    [CreateAssetMenu(fileName = "Bool", menuName = "Variables/Bool", order = 100)]
    public class BoolVariable : Variable<bool>
    {
        public void Toggle()
        {
            Value = !Value;
        }
    }
}