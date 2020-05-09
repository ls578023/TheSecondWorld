using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressEventTrigger : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    public float durationThreshold = 0.7f;
    public float moveDisCancel = 10;

    public UnityEvent onLongPress = new UnityEvent();

    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;

    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                onLongPress.Invoke();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(eventData.delta, Vector2.zero) > moveDisCancel)
            isPointerDown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!longPressTriggered)
            isPointerDown = false;
    }
}