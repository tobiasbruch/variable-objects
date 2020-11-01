using TMPro;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    public class TextReferenceBehaviour : ReferenceBehaviour<string, StringVariable, StringReference>
    { 
        [SerializeField]
        protected TextMeshProUGUI _textComponent = default;

        protected override void OnValueChanged(string oldValue, string newValue)
        {
            _textComponent.text = newValue;
        }
    }
}