using UnityEngine;
using System;
using socket.io;
using Newtonsoft.Json;

public class SocketManager
{
    private static SocketManager instance;
    public static SocketManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SocketManager();
            }
            return instance;
        }
    }

    public string roomId;
    public Socket socket;

    private SocketManager()
    {
        Connect();
    }

    private void Connect()
    {
        string url = "";
        this.socket = Socket.Connect(url);
    }

    public void On<T>(string eventName, Action<T> callBack)
    {
        T jsonObject;
        socket.On
        (
            eventName, data =>
            {
                jsonObject = ConvertJsonToObject<T>(data);
                callBack.Invoke(jsonObject);
            }
        );
    }
    private T ConvertJsonToObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }
}