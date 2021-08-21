using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;


public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Direction direction;

    public Action<Direction> OnClickStartMoveButton;
    public Action OnClickEndMoveButton;


    public void BindClickStart(Action<Direction> action)
    {
        OnClickStartMoveButton += action;
    }
    public void BindClickEnd(Action action)
    {
        OnClickEndMoveButton += action;
    }

    // Input
    public void OnPointerDown(PointerEventData e)
    {
        OnClickStartMoveButton.Invoke(this.direction);
    }
    public void OnPointerUp(PointerEventData e)
    {
        OnClickEndMoveButton.Invoke();
    }

}