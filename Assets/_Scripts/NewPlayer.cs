using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Numerics;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.GraphView.GraphView;

[Serializable]
public class Player
{
    public int playerID;
    public int lastSessionID;
    public string name;
    public string country;
    public string dateTime;

    public Player(int playerID, string name, string country, string dateTime)
    {
        this.playerID = playerID;
        this.lastSessionID = 0;
        this.name = name;
        this.country = country;
        this.dateTime = dateTime;

        Debug.Log("Player created: playerID = " + playerID + " name = " + name + " country = " + country + " dateTime = " + dateTime);
    }

    public int GetLastSessionID() { return lastSessionID; }
    public void SetLastSessionID(int lastSessionID) { this.lastSessionID = lastSessionID; }
}

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
