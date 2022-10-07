using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Session
{
    public int sessionID;
    public int playerID;
    public string dateTime;

    public Session(int sessionID, int playerID ,string dateTime)
    {
        this.sessionID = sessionID;
        this.playerID = playerID;
        this.dateTime = dateTime;

        Debug.Log("Session created: sessionID = " + sessionID + " playerID = " + playerID + " dateTime = " + dateTime);
    }

}

public class NewSession : MonoBehaviour
{
    private void OnEnable()
    {
        Simulator.OnNewSession += AddNewSession;
    }
    private void OnDisable()
    {
        Simulator.OnNewSession -= AddNewSession;
    }

    public void AddNewSession(DateTime dateTime)
    {
        Debug.Log("Received new session at " + dateTime);
        StartCoroutine(Uploader.UploadNewSession(dateTime));
    }
}
