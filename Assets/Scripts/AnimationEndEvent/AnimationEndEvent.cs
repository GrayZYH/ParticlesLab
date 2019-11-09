using UnityEngine;
using UnityEngine.Events;

public class AnimationEndEvent : MonoBehaviour
{
    /// <summary>
    /// 动画完成事件
    /// </summary>
    public UnityEvent AnimEndEvent;

    /// <summary>
    /// 动画完成激活
    /// </summary>
    public void AnimEventEnd()
    {
        AnimEndEvent.Invoke();
    }
}
