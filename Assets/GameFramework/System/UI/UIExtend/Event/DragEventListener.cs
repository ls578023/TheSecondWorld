using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragEventListener : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public EventListener.PointerDataDelegate onDrag;
    public EventListener.PointerDataDelegate onBeginDrag;
    public EventListener.PointerDataDelegate onEndDrag;
    public EventListener.PointerDataDelegate onDrop;
    public EventListener.PointerDataDelegate onClick;
    public object arg;

    private bool isDrangTriggered = false;
    static public DragEventListener Get(GameObject go, object arg = null)
    {
        DragEventListener listener = go.GetComponent<DragEventListener>();
        if (listener == null) listener = go.AddComponent<DragEventListener>();
        listener.arg = arg;
        return listener;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
            onDrag(eventData);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
            onBeginDrag(eventData);
        isDrangTriggered = true;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
            onEndDrag(eventData);
        isDrangTriggered = false;
    }
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (this.onDrop != null)
        {
            this.onDrop(eventData);
        }
        isDrangTriggered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDrangTriggered)
        {
            onClick?.Invoke(eventData);
        }
    }

}