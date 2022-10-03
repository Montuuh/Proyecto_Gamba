using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NewPlayer : MonoBehaviour
{
    private void OnEnable()
    {
        Simulator.OnNewPlayer += AddNewPlayer;
    }
    private void OnDisable()
    {
        Simulator.OnNewPlayer -= AddNewPlayer;
    }
    
    private void AddNewPlayer(string name, string country, DateTime dateTime)
    {
        Debug.Log("Received new player: " + name + " from " + country + " at " + dateTime);
        StartCoroutine(Uploader.UploadNewPlayer(name, country, dateTime));
    }
}
