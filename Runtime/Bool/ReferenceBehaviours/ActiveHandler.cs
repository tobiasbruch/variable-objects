using UnityEngine;
using UnityEngine.Events;
namespace TobiasBruch.VariableObjects
{
    public class ActiveHandler : ReferenceBehaviour<bool, BoolVariable, BoolReference>
    {
        [SerializeField]
        protected GameObject _target = default;
        [SerializeField]
        protected UnityEvent _onActivate = default;
        [SerializeField]
        protected UnityEvent _onDeactivate = default;

        protected override void OnValueChanged(bool oldValue, bool newValue)
        {
            if (_target)
            {
                _target.SetActive(newValue);
            }

            (newValue ? _onActivate : _onDeactivate).Invoke();
        }
    }
}