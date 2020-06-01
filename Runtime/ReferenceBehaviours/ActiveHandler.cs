using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace TobiasBruch.VariableObjects
{
    public class ActiveHandler : ReferenceBehaviour<bool, BoolVariable, BoolReference>
    {
        [SerializeField]
        protected GameObject _target = default;
        [SerializeField]
        private AnimationCoroutine _showAnimation = default;
        [SerializeField]
        private AnimationCoroutine _hideAnimation = default;

        private Coroutine _coroutine;
        protected override void OnValueChanged(bool oldValue, bool newValue)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            if (newValue)
            {
                _coroutine = StartCoroutine(ShowCoroutine());
            }
            else
            {
                _coroutine = StartCoroutine(HideCoroutine());
            }
        }

        private IEnumerator ShowCoroutine()
        {
            if (_target)
            {
                _target.SetActive(true);
            }
            yield return _showAnimation.Play();
        }
        private IEnumerator HideCoroutine()
        {
            yield return _hideAnimation.Play();
            if (_target)
            {
                _target.SetActive(false);
            }
        }
    }
}