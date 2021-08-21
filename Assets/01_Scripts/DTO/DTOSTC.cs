using UnityEngine;
using System;

// mouse
[Serializable]
public class STCMoveData
{
    public Direction direction;
}

// cut
[Serializable]
public class STCCutData
{
}

// mouse
[Serializable]
public class STCMouseData
{
    public float x;
    public float y;
    private Vector2 position;
    public Vector2 Position
    {
        get
        {
            if (position == null)
            {
                position = new Vector2(x, y);
            }
            return position;
        }
    }
}

// inviteCode
[Serializable]
public class STCInviteCodeData
{
    public string roomId;
}

// render
[Serializable]
public class STCRenderData
{
    public CharacterType character;
}

// start
[Serializable]
public class STCStartData
{
    public float x;
    public float y;
}
