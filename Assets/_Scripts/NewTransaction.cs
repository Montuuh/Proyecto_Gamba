using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTransaction : MonoBehaviour
{
    private void OnEnable()
    {
        Simulator.OnBuyItem += AddTransaction;
    }
    private void OnDisable()
    {
        Simulator.OnBuyItem -= AddTransaction;
    }

    public void AddTransaction(int productID, DateTime dateTime)
    {
        Debug.Log("Received buy of amount " + productID + " at " + dateTime);
        StartCoroutine(Uploader.UploadNewTransaction(productID, dateTime));
    }
}
