using UnityEngine;
using System.Collections;

namespace TobiasBruch.VariableObjects
{
    public class ActiveHandler : ReferenceBehaviour<bool, BoolVariable, BoolReference>
    {
        [SerializeField]
        private AnimationCoroutine _showAnimation = default;
        [SerializeField]
        private AnimationCoroutine _hideAnimation = default;

        private Coroutine _coroutine;

        protected override UpdateMode Update => UpdateMode.WhileAlive;

        protected override void OnValueChanged(bool oldValue, bool newValue)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            if (newValue)
            {
                gameObject.SetActive(true);
                _coroutine = StartCoroutine(ShowCoroutine());
            }
            else
            {
                _coroutine = StartCoroutine(HideCoroutine());
            }
        }

        private IEnumerator ShowCoroutine()
        {
            yield return _showAnimation.Play();
        }
        private IEnumerator HideCoroutine()
        {
            yield return _hideAnimation.Play();
            gameObject.SetActive(false);
        }
    }
}