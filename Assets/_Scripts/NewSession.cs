using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NewSession : MonoBehaviour
{
    private void OnEnable()
    {
        Simulator.OnNewSession += AddStartSession;
        Simulator.OnEndSession += AddEndSession;
    }
    private void OnDisable()
    {
        Simulator.OnNewSession -= AddStartSession;
        Simulator.OnEndSession -= AddEndSession;
    }

    public void AddStartSession(DateTime dateTime)
    {
        StartCoroutine(Uploader.UploadSessions(dateTime, "StartSession"));
    }
    public void AddEndSession(DateTime dateTime)
    {
        StartCoroutine(Uploader.UploadSessions(dateTime, "EndSession"));
    }
}
