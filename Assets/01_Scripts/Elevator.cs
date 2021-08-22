using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float maxY = 5;
    public float minY = 0;
    public float speed = 5;

    public Hook hook;
    private bool ballIsOn = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ENTER: " + collision.collider.name);
        if(collision.collider.CompareTag("Ball"))
        {
            if(hook.joint.enabled == false)
            {
                ballIsOn = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("EXIT: " + other.collider.name);
        if (other.collider.CompareTag("Ball"))
        {
            ballIsOn = false;
        }
    }

    private void FixedUpdate()
    {
        if (hook.joint.enabled == true)
        {
            ballIsOn = false;
        }
        if(ballIsOn == true && this.transform.position.y < this.maxY)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * speed);
            return;
        }
        if (ballIsOn == false && this.transform.position.y > this.minY)
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
}