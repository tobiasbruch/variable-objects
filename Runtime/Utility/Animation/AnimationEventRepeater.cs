using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TobiasBruch.VariableObjects
{
    public class AnimationEventRepeater : MonoBehaviour
    {
        public UnityStringEvent onAnimationEvent = default;
        
        public void RepeatAnimationEvent(string eventName)
        {
            onAnimationEvent.Invoke(eventName);
        }
    }
    
    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string>{}
}