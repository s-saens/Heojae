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
        GameSocketManager s = GameSocketManager.Instance;
    }

    private void Start()
    {
        GameSocketManager.Instance.Emit("renderComplete");
        InitControllers();
        BindButtons();
        Debug.Log(User.Instance.Character);
    }

    private void InitControllers()
    {
        controllers.moveController.Init(objects);
        controllers.hookController.Init(objects);
        controllers.cameraController.Init(objects);
        controllers.syncController.Init(objects);
    }

    private void BindButtons()
    {
        inputReceivers.buttonLeft.BindClickStart( OnClickStartMoveButton );
        inputReceivers.buttonLeft.BindClickEnd( OnClickEndMoveButton );
        inputReceivers.buttonRight.BindClickStart(OnClickStartMoveButton);
        inputReceivers.buttonRight.BindClickEnd(OnClickEndMoveButton);
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

    private void FixedUpdate()
    {
        SetRoleUI();
    }

    private void SetRoleUI()
    {
        if (User.Instance.Character == CharacterType.archaeologist)
        {
            objects.ball.GetComponent<Rigidbody2D>().isKinematic = false;
            inputReceivers.touchScreen.gameObject.SetActive(false);

            if (objects.hook.joint.enabled == true)
            {
                inputReceivers.buttonLeft.gameObject.SetActive(true);
                inputReceivers.buttonRight.gameObject.SetActive(true);
            }
            else
            {
                inputReceivers.buttonLeft.gameObject.SetActive(false);
                inputReceivers.buttonRight.gameObject.SetActive(false);
            }
        }
        else if (User.Instance.Character == CharacterType.assistant)
        {
            objects.ball.GetComponent<Rigidbody2D>().isKinematic = true;
            inputReceivers.buttonLeft.gameObject.SetActive(false);
            inputReceivers.buttonRight.gameObject.SetActive(false);

            if (objects.hook.joint.enabled == true)
            {
                inputReceivers.touchScreen.gameObject.SetActive(false);
            }
            else
            {
                inputReceivers.touchScreen.gameObject.SetActive(true);
            }
        }
    }
}