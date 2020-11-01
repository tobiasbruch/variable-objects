using UnityEngine;
using UnityEngine.Events;

namespace TobiasBruch.VariableObjects
{
    public class FloatChangeListener: ReferenceChangeListener<float, FloatVariable, FloatReference>
    {
        [SerializeField]
        private UnityEvent<float> _onIncrease = default;
        [SerializeField]
        private UnityEvent<float> _onDecrease = default;

        protected override void OnValueChanged(float oldValue, float newValue)
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