using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TouchScreen : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Action<Vector2> OnTouchDragEnd;
    Vector2 touchStartPosition;
    public LineRenderer line;

    private void Start()
    {
        line.gameObject.SetActive(false);
    }

    public void BindClick(Action<Vector2> action)
    {
        OnTouchDragEnd += action;
    }

    public void OnPointerDown(PointerEventData e)
    {
        line.gameObject.SetActive(true);

        touchStartPosition = Camera.main.ScreenToWorldPoint(e.position);
        line.SetPositions(new Vector3[] { touchStartPosition, touchStartPosition });
    }

    public void OnDrag(PointerEventData e)
    {
        Vector2 nowTouchPosition = Camera.main.ScreenToWorldPoint(e.position);
        line.SetPosition(1, nowTouchPosition);
    }

    public void OnPointerUp(PointerEventData e)
    {
        line.gameObject.SetActive(false);

        Vector2 touchEndPosition = Camera.main.ScreenToWorldPoint(e.position);
        Vector2 direction = touchEndPosition - touchStartPosition;

        if(direction.magnitude <= 1)
        {
            return;
        }
        OnTouchDragEnd?.Invoke(direction.normalized);   
    }
}