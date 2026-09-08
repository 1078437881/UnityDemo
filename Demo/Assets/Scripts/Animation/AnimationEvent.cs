using UnityEngine;
using UnityEngine.Events;

namespace Animation
{
    public class AnimationEvent: UnityEvent<int>
    { 
        
    }

    public class AnimationListener : MonoBehaviour
    {
        public static AnimationEvent animationEvent = new AnimationEvent();

        public void MyCustomEvent(int intValue)
        {
            animationEvent.Invoke(intValue);
        }
    }
}