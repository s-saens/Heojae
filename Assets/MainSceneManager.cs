using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainSceneManager : MonoBehaviour
{
    public Camera cam;
    public Text inviteCode;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SocketManager s = SocketManager.Instance;
        User u = User.Instance;
        AddSocketEvent();
    }

    // STC
    private void AddSocketEvent()
    {
        SocketManager.Instance.On<STCRenderData>("render", OnRender);
        SocketManager.Instance.On<STCInviteCodeData>("inviteCode", OnInviteCode);

        void OnRender(STCRenderData data)
        {
            User.Instance.Character = data.character;
            SceneManager.LoadScene((int)SceneType.Game);
        }
        void OnInviteCode(STCInviteCodeData data)
        {
            inviteCode.text = "INVITE CODE: " + data.roomId;
        }
    }

    // CTS
    public void GameStartHost(int characterType)
    {
        Dictionary<string, object> ctsData = new Dictionary<string, object>();
        ctsData.Add("character", characterType);
        SocketManager.Instance.Emit("host", ctsData);
    }
    public void GameStartJoin(string roomId)
    {
        Dictionary<string, object> ctsData = new Dictionary<string, object>();
        ctsData.Add("roomId", roomId);
        SocketManager.Instance.Emit("join", ctsData);
    }
    public void Settings()
    {
        //
    }
    public void Quit()
    {
        Application.Quit();
    }



    // color change

    private void Update()
    {
        UpdateCameraBackgroundColor();
    }

    public float hue = 60;
    public float colorTransitionSpeed;
    private void UpdateCameraBackgroundColor()
    {
        hue += Time.deltaTime * colorTransitionSpeed;
        hue = hue % 360;
        cam.backgroundColor = Color.HSVToRGB(hue /360, 0.5f, 0.75f);
    }
}