using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TouchScreen : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Action<Vector2> OnTouchDragEnd;
    Vector2 touchStartPosition_canvas;
    Vector2 touchStartPosition;
    Vector2 nowTouchPosition_canvas;
    Vector2 nowTouchPosition;
    public LineRenderer line;


    private bool isPointerDown = false;

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
        isPointerDown = true;
        line.gameObject.SetActive(true);


        touchStartPosition_canvas = e.position;
        touchStartPosition = Camera.main.ScreenToWorldPoint(touchStartPosition_canvas);

        nowTouchPosition_canvas = touchStartPosition_canvas;

        line.SetPositions(new Vector3[] { touchStartPosition, touchStartPosition });
    }

    public void OnDrag(PointerEventData e)
    {
        nowTouchPosition_canvas = e.position;
        nowTouchPosition = Camera.main.ScreenToWorldPoint(nowTouchPosition_canvas);
        line.SetPosition(1, nowTouchPosition);
    }

    public void OnPointerUp(PointerEventData e)
    {
        isPointerDown = false;
        line.gameObject.SetActive(false);

        Vector2 touchEndPosition = Camera.main.ScreenToWorldPoint(e.position);
        Vector2 direction = touchEndPosition - touchStartPosition;

        if(direction.magnitude <= 1)
        {
            return;
        }
        OnTouchDragEnd?.Invoke(direction.normalized);   
    }

    public void Update()
    {
        if(isPointerDown)
        {
            touchStartPosition = Camera.main.ScreenToWorldPoint(touchStartPosition_canvas);
            nowTouchPosition = Camera.main.ScreenToWorldPoint(nowTouchPosition_canvas);
            line.SetPosition(0, touchStartPosition);
            line.SetPosition(1, nowTouchPosition);
        }
    }
}