using UnityEngine;

public class Link : MonoBehaviour
{
    public GameObject coll;
    public HingeJoint2D hinge;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        this.hinge = this.GetComponent<HingeJoint2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        coll = collision.gameObject;
        this.hinge.anchor = new Vector2(0, 0.5f);

        Debug.Log(coll.name);
    }
}