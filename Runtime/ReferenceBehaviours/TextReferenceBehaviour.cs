using TMPro;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    public class TextReferenceBehaviour : ReferenceBehaviour<string, StringVariable, StringReference>
    { 
        [SerializeField]
        protected TMP_Text _textComponent = default;

        private void Awake()
        {
            if(!_textComponent)
            {
                _textComponent = GetComponent<TMP_Text>();
            }
        }
        protected override void OnValueChanged(string oldValue, string newValue)
        {
            _textComponent.text = newValue;
        }
    }
}