using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TouchScreen : MonoBehaviour, IPointerDownHandler
{
    public Action<Vector2> OnClickTouchScreen;

    public void Bind(Action<Vector2> action)
    {
        OnClickTouchScreen += action;
    }

    public void OnPointerDown(PointerEventData e)
    {
        var worldTouchedPosition = Camera.main.ScreenToWorldPoint(e.position);
        OnClickTouchScreen?.Invoke(worldTouchedPosition);
    }
}