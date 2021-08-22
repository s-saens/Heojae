using UnityEngine;
using System.Collections.Generic;
using System;

public class SyncController : MonoBehaviour
{
    private ObjectBundle objects;

    public void Init(ObjectBundle ob)
    {
        this.objects = ob;
        AddSocketEvent();
    }

    private void AddSocketEvent()
    {
        GameSocketManager.Instance.On<STCSyncData>("sync", OnSync);

        void OnSync(STCSyncData data)
        {
            objects.ball.Position = new Vector2(data.ball.x, data.ball.y);
            objects.hook.Position = new Vector2(data.wire.x, data.wire.y); ;
        }
    }


    public void FixedUpdate()
    {
        if(User.Instance.Character == CharacterType.archaeologist)
        {
            Dictionary<string, object> ctsData = new Dictionary<string, object>();
            ctsData.Add("ball", new VectorSERVER(objects.ball.Position));
            ctsData.Add("wire", new VectorSERVER(objects.hook.Position));
            ctsData.Add("time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            GameSocketManager.Instance.Emit("sync", ctsData);
        }
    }
}
