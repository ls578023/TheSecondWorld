using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class EventListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler, IPointerClickHandler, IUpdateSelectedHandler, ISelectHandler, IMoveHandler
{
    public delegate void BaseDataDelegate(BaseEventData eventData);
    public delegate void PointerDataDelegate(PointerEventData eventData);    
    public delegate void AxisDataDelegate(AxisEventData eventData);
    

    public PointerDataDelegate onClick;
    public PointerDataDelegate onDown;
    public PointerDataDelegate onEnter;
    public PointerDataDelegate onExit;
    public PointerDataDelegate onUp;
    public BaseDataDelegate onSelect;
    public BaseDataDelegate onUpdateSelect;
    public AxisDataDelegate onMove;

    static public EventListener Get(GameObject go)
    {     
        EventListener listener = go.GetComponent<EventListener>();
        if (listener == null) listener = go.AddComponent<EventListener>();
        return listener;
    }
    static public EventListener Get(Component go)
    {
        return Get(go.gameObject);
    }
    
    public  void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(eventData);
    }
    public  void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(eventData);
    }
    public  void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(eventData);
    }
    public  void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(eventData);
    }
    public  void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(eventData);
    }
    public  void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(eventData);
    }
    public  void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(eventData);
    }
    public  void OnMove(AxisEventData eventData)
    {
        if (onMove != null) onMove(eventData);
    }

}