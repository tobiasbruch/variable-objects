using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    public abstract class ReferenceBehaviour<T1, T2, T3> : MonoBehaviour where T3 : Reference<T1, T2> where T2 : Variable<T1>
    {
        [SerializeField]
        private T3 _reference = default;
        
        public T1 Value
        {
            get => _reference.Value;
            set
            {
                _reference.Value = value;
            }
        }

        protected virtual void OnEnable()
        {
            _reference.EventValueChanged += OnValueChangedThroughEvent;
            if (gameObject.activeInHierarchy)
            {
                OnValueChanged(_reference.Value, _reference.Value);
            }
        }
        protected virtual void OnDisable()
        {
            _reference.EventValueChanged -= OnValueChangedThroughEvent;
        }

        private void OnValueChangedThroughEvent(T1 oldValue, T1 newValue)
        {
#if UNITY_EDITOR
        _lastValidatedValue = newValue;
#endif
            if (gameObject.activeInHierarchy)
            {
                OnValueChanged(oldValue, newValue);
            }
        }

        protected abstract void OnValueChanged(T1 oldValue, T1 newValue);

#if UNITY_EDITOR
    private T1 _lastValidatedValue = default;
    private bool _didFirstValidation = false;
    void OnValidate() { UnityEditor.EditorApplication.delayCall += _OnValidate; }
    protected virtual void _OnValidate()
    {
        T1 newValue = _reference.Value;
        if (_didFirstValidation && !_reference.UsesVariable && Application.isPlaying)
        {
            if (_lastValidatedValue == null)
            {
                if (newValue != null)
                {
                    OnValueChanged(_lastValidatedValue, newValue);
                }
            }
            else if (!_lastValidatedValue.Equals(newValue))
            {
                T1 helper = _lastValidatedValue;
                _lastValidatedValue = newValue;
                OnValueChanged(_lastValidatedValue, newValue);
            }
        }
        _didFirstValidation = true;
        _lastValidatedValue = newValue;

        _reference.OnValidate();
    }
#endif
    }
}