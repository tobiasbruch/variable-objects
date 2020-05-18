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
        private string _animatorBool = default;
        [SerializeField]
        private bool _animatorBoolValue = true;
        [SerializeField]
        private string _animatorTrigger = default;
        [SerializeField]
        private string _animatorFinishedEventName = default;
#if DOTWEENPRO
        [SerializeField]
        private DOTweenAnimation _doTweenAnimation = default;
#endif

        [SerializeField]
        private UnityEvent _onStart = default;
        [SerializeField]
        private UnityEvent _onFinish = default;
        
        private bool _animatorIsDone = false;
        private AnimationEventRepeater _animationEventRepeater = default;
        
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
            if(!string.IsNullOrEmpty(_animatorBool))
            {
                _animator.SetBool(_animatorBool, _animatorBoolValue);
            }
            if(!string.IsNullOrEmpty(_animatorTrigger))
            {
                _animator.SetTrigger(_animatorTrigger);
            }
            _animationEventRepeater = null;
            _animationEventRepeater = _animator.GetComponent<AnimationEventRepeater>();
            if(_animationEventRepeater != null && !string.IsNullOrEmpty(_animatorFinishedEventName))
            {
                _animatorIsDone = false;
                _animationEventRepeater.onAnimationEvent.AddListener(OnAnimationEventDone);
                
                yield return new WaitUntil(()=>_animatorIsDone);
            }
        }
        
        private void OnAnimationEventDone(string eventName)
        {
            if(eventName == _animatorFinishedEventName)
            {
                _animatorIsDone = true;
                _animationEventRepeater.onAnimationEvent.RemoveListener(OnAnimationEventDone);
            }
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

        public enum Type
        {
            None,
            Animation,
            Animator
#if DOTWEENPRO
            ,DOTween
#endif
        }
    }
}