using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Uploader
{
    public static IEnumerator UploadNewPlayer(string name, string country, DateTime dateTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("country", country);
        form.AddField("dateTime", dateTime.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/NewPlayer.php", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete! Response text from the server = " + www.downloadHandler.text);
            }
        }
    }
}
