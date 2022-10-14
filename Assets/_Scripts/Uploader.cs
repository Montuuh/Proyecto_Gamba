using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public static class Uploader
{
    static string serverPath = "https://citmalumnes.upc.es/";
    static string userPath = "~davidmm24/";
    static string phpFile = "default";
    static string function = "default";
    static public int lastPlayerID = 0;
    static public Player player;
    
    // NEW PLAYER
    public static IEnumerator UploadNewPlayer(string name, string country, DateTime dateTime)
    {
        function = "NewPlayer";
        phpFile = "NewPlayer.php";
        
        WWWForm form = new WWWForm();
        form.AddField("function", function);
        form.AddField("name", name);
        form.AddField("country", country);
        form.AddField("dateTime", dateTime.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(serverPath + userPath + phpFile, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                try
                {
                    lastPlayerID = int.Parse(www.downloadHandler.text);
                }
                catch
                {
                    Debug.Log("Error parsing last player ID: Response " + www.downloadHandler.text);
                }
                    

                // Creating the player
                player = new Player(lastPlayerID, name, country, dateTime.ToString());
                
                CallbackEvents.OnAddPlayerCallback?.Invoke((uint)lastPlayerID);
            }
        }
    }
    // !NEW PLAYER

    // NEW SESSION
    public static IEnumerator UploadNewSession(DateTime dateTime, string sessionType)
    {
        if (sessionType == "start")
            function = "start";
        else if (sessionType == "end")
            function = "end";
        else
            Debug.Log("Error: sessionType not recognized");

        Debug.Log("Sending session " + function + " to server at " + dateTime);

        phpFile = "NewSession.php";
        
        WWWForm form = new WWWForm();
        form.AddField("playerID", player.playerID);
        form.AddField("dateTime", dateTime.ToString());
        if (function == "end")
            form.AddField("sessionID", player.GetLastSessionID());
        form.AddField("function", function);

        using (UnityWebRequest www = UnityWebRequest.Post(serverPath + userPath + phpFile, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (function == "start")
                {
                    int lastSessionID = int.Parse(www.downloadHandler.text);
                    player.SetLastSessionID(lastSessionID);
                    CallbackEvents.OnNewSessionCallback?.Invoke((uint)player.GetLastSessionID());
                }
                else if (function == "end")
                {
                    CallbackEvents.OnEndSessionCallback?.Invoke((uint)player.GetLastSessionID());
                }
            }
        }
    }
    // !NEW SESSION

    // NEW TRANSACTION
    public static IEnumerator UploadNewTransaction(int productID, DateTime dateTime)
    {
        function = "AddTransaction";
        phpFile = "NewTransaction.php";

        WWWForm form = new WWWForm();
        form.AddField("playerID", player.playerID);
        form.AddField("sessionID", player.GetLastSessionID());
        form.AddField("dateTime", dateTime.ToString());
        form.AddField("productID", productID);
        form.AddField("function", function);

        using (UnityWebRequest www = UnityWebRequest.Post(serverPath + userPath + phpFile, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Transaction added. Response: " + www.downloadHandler.text);
                CallbackEvents.OnItemBuyCallback?.Invoke();
            }
        }
    }
}
