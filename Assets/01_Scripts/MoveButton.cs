using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
    None,
    Left,
    Right
}
public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Ball ball;
    public Direction direction;
    private Direction dirState;
    private float strength = 2;
    private float maxVeclocity = 10;

    public void OnPointerDown(PointerEventData e)
    {
        // Socket.Emit
        if (direction == Direction.Left)
        {
            dirState = Direction.Left;
        }
        else if (direction == Direction.Right)
        {
            dirState = Direction.Right;
        }
    }

    public void Start()
    {
        AddSocketEvent();
    }

    public void AddSocketEvent()
    {
        SocketManager.Instance.On<STCCommandData>("move", OnCommandMove);
    }

    public void OnCommandMove(STCCommandData data)
    {
        
    }

    public void OnPointerUp(PointerEventData e)
    {
        dirState = Direction.None;
    }

    private void Update()
    {
        if(dirState == Direction.Left)
        {
            ball.rigid.AddForce(this.transform.right * strength * (-1));
        }
        else if(dirState == Direction.Right)
        {
            ball.rigid.AddForce(this.transform.right * strength);
        }

        // 속도제한
        if(ball.rigid.velocity.magnitude > maxVeclocity)
        {
            ball.rigid.velocity = ball.rigid.velocity.normalized * maxVeclocity;
        }
    }
}