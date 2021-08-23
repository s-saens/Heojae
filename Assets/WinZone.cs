using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public GameObject winPopup;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Ball"))
        {
            winPopup.SetActive(true);
            
        }
    }
}
