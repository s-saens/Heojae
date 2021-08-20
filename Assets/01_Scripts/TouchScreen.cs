using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TouchScreen : MonoBehaviour, IPointerDownHandler
{
    public Action<Vector2> OnClickTouchScreen;

    public void Bind(Action<Vector2> action)
    {
        OnClickTouchScreen += action;
        Debug.Log("Bind TouchScreen");
    }

    public void OnPointerDown(PointerEventData e)
    {
        OnClickTouchScreen?.Invoke(e.position);
        Debug.Log("Position: " + Camera.main.ScreenToWorldPoint(e.position));
    }
}