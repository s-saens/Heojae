using UnityEngine;
using System;
using System.Collections;


public class Hook : MonoBehaviour
{
    private Collider2D coll2D;
    public Collider2D Coll2D
    {
        get
        {
            if(coll2D == null)
            {
                coll2D = this.GetComponent<Collider2D>();
            }
            return this.coll2D;
        }
    }

    private float throwDuration = 1;
    private float range = 2.5f;
    private float throwSpeed = 15;
    private float returnSpeed = 15;

    private IEnumerator throwCoroutine;
    private IEnumerator returnCoroutine;

    public Vector2 Position
    {
        get
        {
            return new Vector2(this.transform.position.x, this.transform.position.y);
        }
        set
        {
            this.transform.position = value;
        }
    }

    private Ball ball;
    public DistanceJoint2D joint;

    public void InitHook(Ball ball)
    {
        this.ball = ball;
        this.joint = this.GetComponent<DistanceJoint2D>();
        ReturnHook();
    }

    public void ThrowHook(Vector2 direction, float strength)
    {
        this.range = strength;
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        if (throwCoroutine != null)
        {
            StopCoroutine(throwCoroutine);
        }

        throwCoroutine = ThrowCoroutine(direction);
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
    private IEnumerator ThrowCoroutine(Vector2 direction)
    {
        Debug.Log("ThrowCoroutine Start");
        for(float i=0 ; i < throwDuration ; i+=Time.deltaTime)
        {
            if(this.joint.enabled == true)
            {
                break;
            }
            if(Vector2.Distance(ball.Position, this.transform.position) > this.range)
            {
                break;
            }
            this.transform.Translate(direction * Time.deltaTime * throwSpeed);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("ThrowEnd");
        ReturnHook();
    }
    private IEnumerator ReturnCoroutine()
    {
        Debug.Log("Return");
        while (true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, ball.Position, Time.deltaTime * throwDuration * returnSpeed);
            yield return new WaitForFixedUpdate();
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