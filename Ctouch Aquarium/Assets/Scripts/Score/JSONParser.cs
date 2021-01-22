using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class JSONParser : MonoBehaviour
{
    [SerializeField]
    public Score scoreData;

    private string fileName = "DisplayLog_2020-12-14.log";
    private List<string> files = new List<string>();
    private Data data;
    private string path = "C:/Windows/SysWOW64/config/systemprofile/AppData/Local/CTOUCH/";

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
        DateTime dateTime = DateTime.UtcNow.Date;
        fileName = "DisplayLog_" + dateTime.ToString("yyyy-MM-dd") + ".log";
    }

    private void SetFileNameList()
    {
        files = new List<string>();
        DateTime dateTime = DateTime.UtcNow;
        DateTime lastDate = DateTimeConverter.ParseRequestDate(scoreData.lastTimeStamp);
        
        for (DateTime i = lastDate; i <= dateTime; i = i.AddDays(1))
        {
            files.Add(path + "DisplayLog_" + i.ToString("yyyy-MM-dd") + ".log");
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
        dateTime = DateTimeConverter.ParseRequestDate(timeStamp);
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

public static class DateTimeConverter
{
    public static DateTime ParseRequestDate(string dateTime)
    {
        DateTime dateValue;
        long dtLong;

        var formatStrings = new string[] { "MM/dd/yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss", "dd-MM-yyyy hh:mm:ss", "MM/dd/yyyy hh:mm:ss" };
        if (DateTime.TryParseExact(dateTime, formatStrings, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            return dateValue;
        else if (DateTime.TryParseExact(dateTime, formatStrings, new CultureInfo("nl-NL"), DateTimeStyles.None, out dateValue))
            return dateValue;
        else if (DateTime.TryParse(dateTime, out dateValue))
            return dateValue;

        throw new Exception("Cant parse");
    }
}