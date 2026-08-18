using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class UGUIEventListener : EventTrigger
{
    public UnityAction<GameObject> onClick;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

    static public UGUIEventListener Get(GameObject gameObj)
    {
        UGUIEventListener listener = gameObj.GetComponent<UGUIEventListener>();
        if (listener == null)
        {
            listener = gameObj.AddComponent<UGUIEventListener>();
        }

        return listener;
    }
}