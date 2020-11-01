using UnityEngine;
using UnityEngine.Events;

namespace TobiasBruch.VariableObjects
{
    public class IntChangeListener : ReferenceChangeListener<int, IntVariable, IntReference>
    {
        [SerializeField]
        private UnityEvent<int> _onIncrease = default;
        [SerializeField]
        private UnityEvent<int> _onDecrease = default;

        protected override void OnValueChanged(int oldValue, int newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            if (newValue > oldValue)
            {
                _onIncrease.Invoke(newValue);
            }
            else if (newValue < oldValue)
            {
                _onDecrease.Invoke(newValue);
            }
        }
    }
}