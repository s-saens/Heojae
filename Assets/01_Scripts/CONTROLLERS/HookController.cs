using UnityEngine;

public class HookController : MonoBehaviour
{
    private ObjectBundle objects;

    public void Init(ObjectBundle ob)
    {
        this.objects = ob;
        objects.hook.InitHook(this.objects.ball);
    }

    private bool testFlag = true;
    public void OnClickTouchScreen(Vector2 touchPosition)
    {
        if (testFlag == true)
        {
            MakeRope((touchPosition - objects.ball.Position).normalized);
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
        objects.hook.gameObject.SetActive(true);
        objects.hook.ThrowHook(direction);
    }

    public void CutRope()
    {
        objects.hook.ReturnHook();
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

    private void Update()
    {
        UpdateHookCollider();
    }

    private void UpdateHookCollider()
    {
        float distance = Vector2.Distance(objects.ball.Position, objects.hook.transform.position);
        objects.hook.Coll2D.enabled = (distance > objects.ball.Radius * 0.5f);
    }
}