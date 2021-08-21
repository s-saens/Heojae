using UnityEngine;
using System.Collections.Generic;

public class HookController : MonoBehaviour
{
    private ObjectBundle objects;
    private bool testFlag = true;

    public void Init(ObjectBundle ob)
    {
        this.objects = ob;
        AddSocketEvent();
        objects.hook.InitHook(this.objects.ball);
    }

    
    // STC
    public void AddSocketEvent()
    {
        // On
        SocketManager.Instance.On<STCMouseData>("mouse", OnMouse);

        // Functions
        void OnMouse(STCMouseData data)
        {
            if (testFlag == true)
            {
                MakeRope((data.Position - objects.ball.Position).normalized);
                // testFlag = false;
            }
            else
            {
                CutRope();
                testFlag = true;
            }
        }
    }
    public void MakeRope(Vector2 direction)
    {
        objects.hook.gameObject.SetActive(true);
        objects.hook.ThrowHook(direction);
    }
    public void CutRope()
    {
        objects.hook.ReturnHook();
    }


    // CTS
    public void OnClickTouchScreen(Vector2 touchPosition)
    {
        Dictionary<string, object> ctsData = new Dictionary<string, object>();
        ctsData.Add("x", touchPosition.x);
        ctsData.Add("y", touchPosition.y);
        SocketManager.Instance.Emit("mouse", ctsData);
    }


    // Just View Update
    private void Update()
    {
        UpdateHookCollider();
    }
    private void UpdateHookCollider()
    {
        float distance = Vector2.Distance(objects.ball.Position, objects.hook.transform.position);
        objects.hook.Coll2D.enabled = (distance > objects.ball.Radius * 0.5f);
    }
    private void LateUpdate()
    {
        UpdateLine();
    }
    public void UpdateLine()
    {
        objects.line.SetPosition(0, objects.ball.transform.position);
        objects.line.SetPosition(1, objects.hook.transform.position);
    }
}