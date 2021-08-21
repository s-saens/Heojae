using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UniRx;

public class GameManager : MonoBehaviour
{
    public ObjectBundle objects;
    public ControllerBundle controllers;
    public InputReceiverBundle inputReceivers;

    private void Awake()
    {
        // SocketManager 초기화용 코드
        SocketManager s = SocketManager.Instance;
    }

    private void Start()
    {
        SocketManager.Instance.Emit("renderComplete");
        InitControllers();
        BindButtons();
    }

    private void InitControllers()
    {
        controllers.moveController.Init(objects);
        controllers.hookController.Init(objects);
        controllers.cameraController.Init(objects);
    }

    private void BindButtons()
    {
        // Player - 고고학자: 움직이기
        inputReceivers.buttonLeft.BindClickStart( OnClickStartMoveButton );
        inputReceivers.buttonLeft.BindClickEnd( OnClickEndMoveButton );
        inputReceivers.touchScreen.BindClick( OnClickTouchScreen );
    }

    public void OnClickStartMoveButton(Direction direction)
    {
        controllers.moveController.OnClickStartMoveButton(direction);
    }
    public void OnClickEndMoveButton()
    {
        controllers.moveController.OnClickEndMoveButton();
    }

    public void OnClickTouchScreen(Vector2 touchPosition)
    {
        controllers.hookController.OnClickTouchScreen(touchPosition);
    }

}