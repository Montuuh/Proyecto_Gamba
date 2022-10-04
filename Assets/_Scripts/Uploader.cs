using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Uploader
{
    public static IEnumerator UploadNewPlayer(string name, string country, DateTime dateTime)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("name=" + name + "&country=" + country + "&dateTime=" + dateTime.ToString()));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~davidmm24/NewPlayer.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete! Response text from the server = " + www.downloadHandler.text);
        }
    }
}
