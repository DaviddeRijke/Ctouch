using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONParser : MonoBehaviour
{
    [SerializeField]
    public Score scoreData;

    private string fileName = "DisplayLog_2020-12-14.log";
    private List<string> files = new List<string>();
    private Data data;


    /// <summary>
    /// reads timestamps and settings from json file
    /// stores timestamps and settings in an array
    /// </summary>
    public Data LoadJson()
    {
        //DateTime lastDate = DateTime.Parse(scoreData.lastTimeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
        data = new Data();
        SetFileNameList();

        string json = "";
        string path = Application.dataPath + "/Scripts/Score/" + fileName;

        foreach (var file in files)
        {
            if (File.Exists(file))
            {
                using (StreamReader r = new StreamReader(file))
                {
                    if (!json.Equals(""))
                    {
                        json = json.Replace("]}", ",");
                        json += r.ReadToEnd().Replace("{\"timeStamps\":[", "");
                    }
                    else
                    {
                        json = r.ReadToEnd();
                    }
                    
                    JsonUtility.FromJsonOverwrite(json, data);
                }

            }
        }

        if (data.timeStamps != null)
        {
            foreach (TimeStamp timeStamp in data.timeStamps)
            {
                timeStamp.SetDateTime();
            }
        }

        return data;
    }

    /// <summary>
    /// set filename according to current date
    /// </summary>
    private void SetFileName()
    {
        DateTime dateTime = DateTime.Now.Date;
        fileName = "DisplayLog_" + dateTime.ToString("yyyy-MM-dd") + ".log";
    }

    private void SetFileNameList()
    {
        files = new List<string>();
        DateTime dateTime = DateTime.Now;
        DateTime lastDate = DateTime.Parse(scoreData.lastTimeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);

        for (DateTime i = lastDate; i <= dateTime; i = i.AddDays(1))
        {
            files.Add(Application.dataPath + "/Scripts/Score/" + "DisplayLog_" + i.ToString("yyyy-MM-dd") + ".log");
        }
    }
}

/// <summary>
/// Stores array of timestamps
/// </summary>
[Serializable]
public class Data
{
    public TimeStamp[] timeStamps;
}

/// <summary>
/// Stores timestamp with corresponding settings
/// </summary>
[Serializable]
public class TimeStamp
{
    public string timeStamp;
    public DateTime dateTime;
    public Settings settings;

    public void SetDateTime()
    {
        dateTime = DateTime.Parse(timeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public override string ToString()
    {
        return "timeStamp: " + timeStamp + "\n"
            + settings.ToString();
    }
}

/// <summary>
/// Settings of touchscreen
/// </summary>
[Serializable]
public class Settings
{
    public int backlight;
    public int backlight_mute;
    public string sleepTime;

    public override string ToString()
    {
        return "backlight: " + backlight + "\n"
            + "backlight_mute: " + backlight_mute;
    }
}