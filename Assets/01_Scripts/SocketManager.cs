using UnityEngine;
using System;
using System.Collections.Generic;
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

    public Socket socket;

    private SocketManager()
    {
        Connect();
    }

    private void Connect()
    {
        string url = "http://146.56.137.29:3000/";
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
                Debug.Log("[SOCKET ON]" + eventName + "\n\n" + data + "\n\n");
                callBack.Invoke(jsonObject);
            }
        );
    }
    private T ConvertJsonToObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    public void Emit(string eventName, Dictionary<string, object> data)
    {
        data.Add("roomId", User.Instance.RoomId);

        string jsonString = JsonConvert.SerializeObject(data);
        Debug.Log("[SOCKET EMIT]" + eventName + "\n\n" + jsonString + "\n\n");

        socket.EmitJson(eventName, jsonString);
    }
    public void Emit(string eventName)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("roomId", User.Instance.RoomId);

        string jsonString = JsonConvert.SerializeObject(data);
        Debug.Log("[SOCKET EMIT(Plain)]" + eventName + "\n\n" + jsonString + "\n\n");

        socket.EmitJson(eventName, jsonString);
    }
}