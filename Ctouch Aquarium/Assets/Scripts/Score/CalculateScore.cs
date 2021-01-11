using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CalculateScore : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private JSONParser JSONParser;
    [SerializeField]
    private ScoreData scoreData;
    [SerializeField]
    private AnimationCurve scoreCurve;
    [SerializeField]
    private float sleepTimeScore = 30;
    [SerializeField]
    private float backlightMuteScore = 15;
    [SerializeField]
    private float offTimeScore = 20;

    private List<double> averageBacklight = new List<double>();
    private Data data;
    private Score score;

    // Start is called before the first frame update
    void Start()
    {
        score = new Score();

        //update score every hour;
        InvokeRepeating("UpdateScore", 0f, 3600f);
    }

    /// <summary>
    /// update score based on json file timestamps
    /// </summary>
    public void UpdateScore()
    {
        GetData();
        if (data.timeStamps == null)
        {
            return;
        }

        CalculateSleepTimeScore();
        CalculateOffTimeScore();
        AveragePerHour(data);
        AddScore(CalculateNewScore());
        SaveData();

        //print out average backlight per hour
        StringBuilder sb = new StringBuilder();
        foreach (float averageBacklight in averageBacklight)
        {
            sb.AppendLine(averageBacklight.ToString());
        }
        text.text = sb.ToString();

        scoreText.text = "Score: " + score.score.ToString();
    }

    /// <summary>
    /// get the json data
    /// </summary>
    public void GetData()
    {
        score.LoadScore();

        JSONParser.scoreData = score;
        data = JSONParser.LoadJson();
    }

    public void SaveData()
    {
        score.SaveScore();
    }

    /// <summary>
    /// Calculates the average backlight usage per hour
    /// </summary>
    private void AveragePerHour(Data data)
    {
        List<int> averageBacklightByHour = new List<int>();
        averageBacklight = new List<double>();
        DateTime lastDate = DateTime.Parse(score.lastTimeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
        int previousHour = lastDate.Hour;
        int previousDay = lastDate.Day;

        //iterate through timestamps and add average usage per hour
        for (int i = 0; i < data.timeStamps.Length; i++)
        {
            if (data.timeStamps[i].dateTime > lastDate)
            {
                if (averageBacklightByHour.Count > 0 && (previousHour != data.timeStamps[i].dateTime.Hour || previousDay != data.timeStamps[i].dateTime.Day))
                {
                    double average = Math.Round(averageBacklightByHour.Average(), 2);
                    averageBacklightByHour = new List<int>();

                    averageBacklight.Add(average);

                    score.averageBacklightUsage = averageBacklight;
                }
                averageBacklightByHour.Add(data.timeStamps[i].settings.backlight);

                previousHour = data.timeStamps[i].dateTime.Hour;
                previousDay = data.timeStamps[i].dateTime.Day;
            }
        }

        //get last timestamp and substract 1 hour
        DateTime newLastTimeStamp = data.timeStamps[data.timeStamps.Length - 1].dateTime.AddHours(-1);
        //set last timestamp from past hour
        score.lastTimeStamp = newLastTimeStamp.ToString();
    }

    /// <summary>
    /// Adds score based on set sleeptime
    /// </summary>
    private void CalculateSleepTimeScore()
    {
        DateTime lastDate = DateTime.Parse(score.lastTimeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
        int previousDay = lastDate.Day;

        if (previousDay != DateTime.Now.Day)
        {
            if (!data.timeStamps[data.timeStamps.Length - 1].settings.sleepTime.Equals("Off"))
            {
                score.score += sleepTimeScore;
            }
        }
    }

    /// <summary>
    /// Adds score based on offtime
    /// </summary>
    private void CalculateOffTimeScore()
    {
        for (int i = 1; i < data.timeStamps.Length; i++)
        {
            DateTime lastDate = DateTime.Parse(data.timeStamps[i].timeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DateTime secondLastDate = DateTime.Parse(data.timeStamps[i - 1].timeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
            
            float hours = (float)(lastDate - secondLastDate).TotalHours;
            if (hours >= 2)
            {
                float newScore = 0;

                if (hours < 6)
                {
                    newScore = hours * offTimeScore;
                }
                else
                {
                    newScore = 6 * offTimeScore;
                }

                score.score += newScore;
            }
        }
    }

    /// <summary>
    /// calculates score based on average backlight per hour
    /// </summary>
    /// <returns></returns>
    private float CalculateNewScore()
    {
        float points = 0;

        foreach (float averageBacklight in averageBacklight)
        {
            float newScore = 0;
            newScore = scoreCurve.Evaluate(averageBacklight / 100);
            points += (newScore * 10);
        }

        return points;
    }

    /// <summary>
    /// adds score locally
    /// </summary>
    private void AddScore(float points)
    {
        score.score += points;
    }
}
