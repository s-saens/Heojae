using UnityEngine;
using System.Collections.Generic;

public class MainSceneManager : MonoBehaviour
{
    public Camera cam;

    // STC


    // CTS
    public void GameStartHost()
    {

    }
    public void GameStartJoin()
    {
        SocketManager.Instance.Emit("join");
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

        Debug.Log(hue);
        cam.backgroundColor = Color.HSVToRGB(hue /360, 0.5f, 0.75f);
    }
}