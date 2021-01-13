using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

[CreateAssetMenu(fileName = "API")]
public class API : ScriptableObject
{
    public string IP;
    public int Port;
    public string URL = "/managementapi";
    private string api_url => IP + ":" + Port.ToString() + URL;

    public string Token;

    void GetRequest()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("{0}?id={1}&APPID={1}", "CityId", "API_KEY"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new NullReferenceException());
        string jsonResponse = reader.ReadToEnd();
       // var info = JsonUtility.FromJson<APIRequest>(jsonResponse);
    }

    public bool GetFloatValue(string property, out float value)
    {
        value = 0;
        return false;
    }

    public bool GetStringValue(string property, out string value)
    {
        value = "";
        return false;
    }

    public bool SetValue()
    {
        return false;
    }

    public bool ExecuteCommand()
    {
        return false;
    }
}
