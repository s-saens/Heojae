using UnityEngine;
using System;

// mouse
[Serializable]
public class STCSyncData
{
    public VectorSERVER ball;
    public VectorSERVER wire;
    // public string time;
}
public class VectorSERVER
{
    public float x;
    public float y;
    public VectorSERVER(Vector2 vector2)
    {
        x = vector2.x;
        y = vector2.y;
    }
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
    public Vector2 Direction
    {
        get
        {
            return new Vector2(x, y);
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
