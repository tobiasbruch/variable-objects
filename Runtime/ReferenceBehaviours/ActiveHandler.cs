using UnityEngine;
using System.Collections;
using System;

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
                _coroutine = StartCoroutine(ShowAsync());
            }
            else
            {
                _coroutine = StartCoroutine(HideAsync());
            }
        }

        public IEnumerator ShowAsync(Action onFinish = null)
        {
            gameObject.SetActive(true);            
            onFinish?.Invoke();
            yield return _showAnimation.Play();
        }
        public IEnumerator HideAsync(Action onFinish = null)
        {
            yield return _hideAnimation.Play();
            onFinish?.Invoke();
            gameObject.SetActive(false);
        }
    }
}