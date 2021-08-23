using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float Radius
    {
        get
        {
            return this.transform.localScale.y;
        }
    }

    public Vector2 Position
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        this.rigid = this.GetComponent<Rigidbody2D>();
    }

    public void ResetTransform()
    {
        Position = Vector2.zero;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        
    }

    public void Left()
    {
        rigid.AddForce(Vector2.left * 100);
    }

    public void Right()
    {
        rigid.AddForce(Vector2.right * 100);
    }
}