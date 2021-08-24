using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainSceneManager : MonoBehaviour
{
    public Camera cam;
    public Text inviteCode;
    public InputField roomIdInputField;
    public GameObject waitingPopup;

    private void Awake()
    {
        Application.targetFrameRate = 40;
        Init();
    }

    private void Init()
    {
        GameSocketManager s = GameSocketManager.Instance;
        User u = User.Instance;
        AddSocketEvent();
    }

    // STC
    private void AddSocketEvent()
    {
        GameSocketManager.Instance.On<STCRenderData>("render", OnRender);
        GameSocketManager.Instance.On<STCInviteCodeData>("inviteCode", OnInviteCode);

        void OnRender(STCRenderData data)
        {
            User.Instance.Character = data.character;
            SceneManager.LoadScene((int)SceneType.Game);
        }
        void OnInviteCode(STCInviteCodeData data)
        {
            User.Instance.RoomId = data.roomId;
            waitingPopup.SetActive(true);
            inviteCode.text = "INVITE CODE: " + data.roomId;
        }
    }

    // CTS
    public void GameStartHost(int characterType)
    {
        Dictionary<string, object> ctsData = new Dictionary<string, object>();
        User.Instance.isHost = true;
        ctsData.Add("character", characterType);
        GameSocketManager.Instance.Emit("host", ctsData);
    }
    public void GameStartJoin()
    {
        User.Instance.isHost = false;
        User.Instance.RoomId = roomIdInputField.text;
        Debug.Log(User.Instance.RoomId);
        GameSocketManager.Instance.Emit("join");
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

    private void FixedUpdate()
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