using UnityEngine;
using System;
using System.Collections;


public class Hook : MonoBehaviour
{
    private float range = 5;
    private float throwSpeed = 6;
    private float returnSpeed = 6;
    private DistanceJoint2D joint;

    private IEnumerator throwCoroutine;
    private IEnumerator returnCoroutine;


    public Ball ball;
    public LineRenderer line;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        this.joint = this.GetComponent<DistanceJoint2D>();
        ReturnHook();
    }

    public void ThrowHook(Vector2 direction)
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        if (throwCoroutine != null)
        {
            StopCoroutine(throwCoroutine);
        }

        Vector2 endPoint = ball.Position + direction * range;
        throwCoroutine = ThrowCoroutine(endPoint);
        StartCoroutine(throwCoroutine);
    }
    public void ReturnHook()
    {
        if(throwCoroutine != null)
        {
            StopCoroutine(throwCoroutine);
        }
        if(returnCoroutine == null)
        {
            returnCoroutine = ReturnCoroutine();
        }

        this.joint.enabled = false;
        StartCoroutine(returnCoroutine);
        
    }
    private IEnumerator ThrowCoroutine(Vector2 endPoint)
    {
        Debug.Log("ThrowCoroutine Start");
        while (Vector2.Distance(ball.Position, this.transform.position) < range && this.joint.enabled == false)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, endPoint, Time.deltaTime * range * throwSpeed);
            yield return 0;
        }
        Debug.Log("ThrowEnd");
        ReturnHook();
    }
    private IEnumerator ReturnCoroutine()
    {
        Debug.Log("Return");
        while (true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, ball.Position, Time.deltaTime * range * returnSpeed);
            yield return 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Stake"))
        {
            if(throwCoroutine != null)
            {
                StopCoroutine(throwCoroutine);
            }
            if(returnCoroutine != null)
            {
                StopCoroutine(returnCoroutine);
            }
            this.joint.distance = Vector2.Distance(ball.Position, this.transform.position);
            this.joint.enabled = true;
        }
        else if(coll.CompareTag("Ball"))
        {
            // DO NOTHING
        }
        else
        {
            ReturnHook();
            this.joint.enabled = false;
        }
    }
}