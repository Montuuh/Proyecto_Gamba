using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Uploader
{
    static string serverPath = "https://citmalumnes.upc.es/";
    static string userPath = "~davidmm24/";
    static string phpFile = "default";
    static string function = "default";
    static public int lastPlayerID = 0;

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
                // LASSE
                // Call to OnNewPlayerCreated????????


                
                // Getting last player ID created
                lastPlayerID = int.Parse(www.downloadHandler.text);
                Debug.Log("Changed last player ID to: " + lastPlayerID);

                // Creating the player
                Player player = new Player(lastPlayerID, name, country, dateTime.ToString());
            }
        }
    }
    // !NEW PLAYER

    // NEW SESSION
    public static IEnumerator UploadNewSession(DateTime dateTime)
    {
        phpFile = "NewSession.php";
        
        WWWForm form = new WWWForm();
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
                // LASSE
                // Call to OnNewSessionCreated????????




                Debug.Log("NewSession.php response = " + www.downloadHandler.text);
            }
        }
    }
    // !NEW SESSION

}
