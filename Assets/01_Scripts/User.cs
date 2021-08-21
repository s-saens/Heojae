using UnityEngine;

public class User
{
    private static User instance;
    public static User Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new User();
            }
            return instance;
        }
    }

    private User()
    {
        AddSocketEvent();
    }

    private void AddSocketEvent()
    {
        SocketManager.Instance.On<STCInviteCodeData>("invite", OnInvite);

        void OnInvite(STCInviteCodeData data)
        {
            this.RoomId = data.roomId;
        }
    }

    public string RoomId {get; set;}
    public bool isHost;

    public CharacterType Character {get; set;}
}