using System.Collections.Generic;
using UnityEngine;using UnityEngine.EventSystems;


/**
 * 事件渗透，当两个GameObject叠放在一起时，需要下面的组件也监听相应事件时
 */
public class Script_05_10 : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PassEvent(eventData,ExecuteEvents.pointerDownHandler);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PassEvent(eventData,ExecuteEvents.pointerUpHandler);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PassEvent(eventData,ExecuteEvents.pointerClickHandler);
    }

    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function) where T:IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data,results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < results.Count; i++)
        {
            if (current != results[i].gameObject)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, function);
                //如果只想响应渗透下去的第一个游戏对象，使用break语句跳出循环
                //break；
            }
        }
    }
}