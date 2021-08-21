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
}

// inviteCode
[Serializable]
public class STCInviteCodeData
{
    string roomId;
}

// render
[Serializable]
public class STCRenderData
{
    CharacterType character;
}

// start
[Serializable]
public class STCStartData
{
}
