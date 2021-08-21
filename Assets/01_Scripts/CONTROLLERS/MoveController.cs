using UnityEngine;
using System.Collections.Generic;

public class MoveController : MonoBehaviour
{
    private Direction dirState;
    private float strength = 2;
    private float maxVeclocity = 10;
    private ObjectBundle objects;

    public void Init(ObjectBundle ob)
    {
        this.objects = ob;
    }

    // STC
    private void Start()
    {
        AddSocketEvent();
    }

    public void AddSocketEvent()
    {
        // On
        SocketManager.Instance.On<STCMoveData>("move", OnMove);

        // Functions
        void OnMove(STCMoveData data)
        {
            Debug.Log(data.direction);
            dirState = data.direction;
        }
    }

    // CTS
    public void OnClickStartMoveButton(Direction direction)
    {
        Dictionary<string, object> ctsData = new Dictionary<string, object>();

        if (dirState == Direction.None) // 다른 방향키 안눌린 상태
        {
            if (direction == Direction.Left)
            {
                ctsData.Add("direction", Direction.Left);
            }
            else if (direction == Direction.Right)
            {
                ctsData.Add("direction", Direction.Right);
            }
        }
        else // 이미 다른 키 눌려있는 상태, 즉 두 키가 동시에 눌린 상태
        {
            ctsData.Add("direction", Direction.None);
        }
        SocketManager.Instance.Emit("move", ctsData);
    }

    public void OnClickEndMoveButton()
    {
        Dictionary<string, object> ctsData = new Dictionary<string, object>();
        ctsData.Add("direction", Direction.None);
        SocketManager.Instance.Emit("move", ctsData);
    }

    private void Update()
    {
        if (dirState == Direction.Left)
        {
            objects.ball.rigid.AddForce(this.transform.right * strength * (-1));
        }
        else if (dirState == Direction.Right)
        {
            objects.ball.rigid.AddForce(this.transform.right * strength);
        }
    }
}