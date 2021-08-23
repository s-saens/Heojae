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

    public float hookStrength = 2.5f;

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

    private void Update()
    {
        if(objects.ball.transform.position.y < -10)
        {
            ResetPosition(Vector2.zero);
        }
    }

    private void ResetPosition(Vector2 pos)
    {
        objects.ball.ResetTransform();
        objects.hook.Position = Vector2.zero;
    }

    private void SetRoleUI()
    {
        if(User.Instance.Character == CharacterType.archaeologist)
        {
            inputReceivers.buttonLeft.gameObject.SetActive(false);
            inputReceivers.buttonRight.gameObject.SetActive(false);
        }
        else
        {
            inputReceivers.touchScreen.gameObject.SetActive(false);
        }
    }

    private void InitControllers()
    {
        controllers.moveController.Init(objects);
        controllers.hookController.Init(objects);
        controllers.cameraController.Init(objects);
    }

    private void BindButtons()
    {
        inputReceivers.buttonLeft.BindClickStart( OnClickStartMoveButton );
        inputReceivers.buttonLeft.BindClickEnd( OnClickEndMoveButton );
        inputReceivers.buttonRight.BindClickStart(OnClickStartMoveButton);
        inputReceivers.buttonRight.BindClickEnd(OnClickEndMoveButton);
        inputReceivers.touchScreen.BindClick( OnDrag );
    }

    public void OnClickStartMoveButton(Direction direction)
    {
        controllers.moveController.OnClickStartMoveButton(direction);
    }
    public void OnClickEndMoveButton()
    {
        controllers.moveController.OnClickEndMoveButton();
    }

    public void OnDrag(Vector2 direction)
    {
        controllers.hookController.OnClickTouchScreen(direction, hookStrength);
    }

    public void Quit()
    {
        Application.Quit();
    }
}