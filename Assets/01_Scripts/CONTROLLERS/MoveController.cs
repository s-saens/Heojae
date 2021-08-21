using UnityEngine;
using System.Collections.Generic;

public class MoveController : MonoBehaviour
{
    private Direction dirState;
    private ObjectBundle objects;

    public void Init(ObjectBundle ob)
    {
        this.objects = ob;
    }

    private void Update()
    {
        if (dirState == Direction.Left)
        {
            objects.ball.rigid.AddForce(this.transform.right * Time.deltaTime * 60 * (-1));
        }
        else if (dirState == Direction.Right)
        {
            objects.ball.rigid.AddForce(this.transform.right * Time.deltaTime * 60);
        }
    }

    public void OnClickStartMoveButton(Direction direction)
    {
        dirState = direction;
    }

    public void OnClickEndMoveButton()
    {
        dirState = Direction.None;
    }
}