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


    public Rope rope;

    public Player myPlayer;

    private void Start()
    {
        BindButtons();
    }

    private void BindButtons()
    {
        // Player - 고고학자: 움직이기
        buttonLeft.OnClickAsObservable().Subscribe( _ => OnClickLeft() );
        buttonRight.OnClickAsObservable().Subscribe( _ => OnClickRight() );
        touchScreen.Bind( OnClickTouchScreen );
    }

    private void OnClickLeft()
    {
        ball.Left();
    }
    private void OnClickRight()
    {
        ball.Right();
    }

    bool testFlag = true;
    private void OnClickTouchScreen(Vector2 touchPosition)
    {
        Debug.Log("OnClickTouchScreen!");
        if(testFlag == true)
        {
            Debug.Log("YEAH!");
            MakeRope((touchPosition - ball.Position).normalized);
            testFlag = false;
        }
        else
        {
            CutRope();
            testFlag = true;
        }
    }

    public void MakeRope(Vector2 direction)
    {
        rope.Shoot(ball.Position, direction);
    }

    public void CutRope()
    {
        rope.Cut();
    }
}