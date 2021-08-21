using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainSceneManager : MonoBehaviour
{
    public Camera cam;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SocketManager s = SocketManager.Instance;
    }

    // STC
    private void AddSocketEvent()
    {
        SocketManager.Instance.On<STCRenderData>("render", OnRender);

        void OnRender(STCRenderData data)
        {
            User.Instance.Character = data.character;
            SceneManager.LoadScene((int)SceneType.Game);
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