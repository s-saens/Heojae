using UnityEngine;
using System;


// Move
[Serializable]
public class CTSMoveData
{
    string roomId;
    Direction direction;
}

// cut
[Serializable]
public class CTSCutData
{
}

// mouse
[Serializable]
public class CTSMouseData
{
    public string roomId;
    public float x;
    public float y;
    public Vector2 position;
    public Vector2 Position
    {
        get
        {
            if(position == null)
            {
                position = new Vector2(x, y);
            }
            return position;
        }
    }
}

// host
[Serializable]
public class CTSHostData
{
    public CharacterType character;
}

// join
[Serializable]
public class CTSJoinData
{
    public string roomId;
}

// renderComplete
[Serializable]
public class CTSRenderCompleteData
{
    public string roomId;
}

// finish
[Serializable]
public class CTSFinishData
{
    public string roomId;
    public bool isClear;
    public int time;
}