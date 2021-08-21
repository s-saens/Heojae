using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UniRx;

public class GameController : MonoBehaviour
{
    public Player p1;
    public Player p2;

    public Button buttonLeft;
    public Button buttonRight;
    public TouchScreen touchScreen;

    public Ball ball;
    public Hook hook;


    public Player myPlayer;

    private void Awake()
    {
        // SocketManager 초기화용 코드
        SocketManager s = SocketManager.Instance;
    }

    private void Start()
    {
        BindButtons();
    }

    private void BindButtons()
    {
        // Player - 고고학자: 움직이기
        // buttonLeft.OnClickAsObservable().Subscribe( _ => OnClickLeft() );
        // buttonRight.OnClickAsObservable().Subscribe( _ => OnClickRight() );
        touchScreen.Bind( OnClickTouchScreen );
    }

    public void OnClickLeft()
    {
        ball.Left();
    }
    public void OnClickRight()
    {
        ball.Right();
    }

    bool testFlag = true;
    public void OnClickTouchScreen(Vector2 touchPosition)
    {
        if(testFlag == true)
        {
            MakeRope((touchPosition - ball.Position).normalized);
            // testFlag = false;
        }
        else
        {
            CutRope();
            testFlag = true;
        }
    }

    public void MakeRope(Vector2 direction)
    {
        hook.gameObject.SetActive(true);
        hook.ThrowHook(direction);
    }

    public void CutRope()
    {
        hook.ReturnHook();
    }


    public LineRenderer line;

    private void LateUpdate()
    {
        UpdateLine();
    }
    public void UpdateLine()
    {
        line.SetPosition(0, ball.transform.position);
        line.SetPosition(1, hook.transform.position);
    }
}