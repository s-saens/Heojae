using UnityEngine;
using System.Collections.Generic;

public class HookController : MonoBehaviour
{
    private ObjectBundle objects;
    private bool testFlag = true;

    public void Init(ObjectBundle ob)
    {
        this.objects = ob;
        objects.hook.InitHook(this.objects.ball);
    }

    public void MakeRope(Vector2 direction, float strength)
    {
        objects.hook.gameObject.SetActive(true);
        objects.hook.ThrowHook(direction, strength);
    }
    public void CutRope()
    {
        objects.hook.ReturnHook();
    }


    // CTS
    public void OnClickTouchScreen(Vector2 direction, float strength)
    {
        if (testFlag == true)
        {
            MakeRope(direction, strength);
            // testFlag = false;
        }
        else
        {
            CutRope();
            testFlag = true;
        }
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