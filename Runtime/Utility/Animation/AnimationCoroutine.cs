#if DOTWEENPRO
using DG.Tweening;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TobiasBruch.VariableObjects
{
    [System.Serializable]
    public class AnimationCoroutine
    {
        [SerializeField]
        private Type _type = Type.None;
        [SerializeField]
        private Animation _animation = default;
        [SerializeField]
        private string _animationName = default;
        [SerializeField]
        private Animator _animator = default;
        [SerializeField]
        private string _animatorState = default;        
        [SerializeField]
        private int _animatorLayer = 0;
        [SerializeField]
        private UnityEventAction _customCallbackEvent = default;
#if DOTWEENPRO
        [SerializeField]
        private DOTweenAnimation _doTweenAnimation = default;
#endif

        [SerializeField]
        private UnityEvent _onStart = default;
        [SerializeField]
        private UnityEvent _onFinish = default;
                
        public IEnumerator Play()
        {
            if (_onStart != null)
            {
                _onStart.Invoke();
            }

            switch (_type)
            {
                case Type.Animation:
                    yield return PlayAnimation();
                    break;
                case Type.Animator:
                    yield return PlayAnimator();
                    break;
                case Type.Custom:
                    yield return PlayCustom();
                    break;
#if DOTWEENPRO
                case Type.DOTween:
                    yield return PlayDoTween();
                    break;
#endif
                default:
                    yield break;
            }

            if (_onFinish != null)
            {
                _onFinish.Invoke();
            }
        }
        private IEnumerator PlayAnimation()
        {
            _animation.Play(_animationName);
            yield return new WaitForSeconds(_animation.GetClip(_animationName).length);
        }
        private IEnumerator PlayAnimator()
        {
            _animator.Play(_animatorState, _animatorLayer);
            yield return new WaitForEndOfFrame();
            AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(_animatorLayer);
            yield return new WaitForSeconds( state.length - Time.deltaTime );
        }
        private IEnumerator PlayCustom()
        {
            bool isDone = false;
            
            System.Action callback = () => isDone = true;
            _customCallbackEvent.Invoke(callback);
            
            yield return new WaitUntil(()=> isDone);
        }
        
#if DOTWEENPRO
        private IEnumerator PlayDoTween()
        {
            _doTweenAnimation.gameObject.SetActive(true);

            List<Tween> tweens = _doTweenAnimation.GetTweens();
            foreach (Tween tween in tweens)
            {
                tween.Restart();
            }
            foreach (Tween tween in tweens)
            {
                if (tween != null && tween.IsActive())
                {
                    yield return tween.WaitForCompletion();
                }
            }
            
            _doTweenAnimation.gameObject.SetActive(false);
        }
#endif
        
        [System.Serializable]
        private class UnityEventAction : UnityEvent<System.Action>{}
        
        public enum Type
        {
            None,
            Animation,
            Animator,
            Custom
#if DOTWEENPRO
            ,DOTween
#endif
        }
    }
}