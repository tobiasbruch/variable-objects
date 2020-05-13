using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    public class TextReferenceBehaviour<T1, T2, T3> : ReferenceBehaviour<T1, T2, T3> where T2 : Variable<T1> where T3 : Reference<T1, T2>
    {
        [SerializeField]
        protected TextMeshProUGUI _textComponent = default;
        [SerializeField]
        protected string _text = default;

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnValueChanged(Value, Value);
                }
            }
        }

        protected override void OnValueChanged(T1 oldValue, T1 newValue)
        {
            UnityEngine.Debug.Log("NEW " + newValue);
            if (string.IsNullOrWhiteSpace(_text))
            {
                _textComponent.text = newValue?.ToString();
            }
            else
            {
                _textComponent.text = _text.Replace("{value}", newValue?.ToString()).Replace("\\n", "\n");
            }
        }
    }
}