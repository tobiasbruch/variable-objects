using UnityEngine;
using UnityEngine.Events;

namespace TobiasBruch.VariableObjects
{
    public class BoolChangeListener : ReferenceChangeListener<bool, BoolVariable, BoolReference>
    {
        [SerializeField]
        private UnityEvent<bool> _onChangeInverted = default;
        [SerializeField]
        private UnityEvent _onChangeToTrue = default;
        [SerializeField]
        private UnityEvent _onChangeToFalse = default;

        protected override void OnValueChanged(bool oldValue, bool newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            _onChangeInverted.Invoke(!newValue);

            if (newValue)
            {
                _onChangeToTrue.Invoke();
            }
            else
            {
                _onChangeToFalse.Invoke();
            }
        }
    }
}