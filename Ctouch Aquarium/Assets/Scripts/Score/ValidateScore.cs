using Pollution;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ValidateScore : MonoBehaviour
{
    [SerializeField]
    private Score scoreData;
    [SerializeField]
    private int AverageTreshold = 60;
    [SerializeField]
    private int ChangeInAverageTreshold = 20;
    [SerializeField]
    private SharkManager sharkManager;
    [SerializeField]
    private PollutionManager pollutionManager;
    [SerializeField]
    private float updateTime = 10f;

    private DateTime lastTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        lastTimeStamp = DateTimeConverter.ParseRequestDate(scoreData.lastTimeStamp);

        //check performance every hour;
        InvokeRepeating("Validate", 0f, updateTime);
    }
    
    /// <summary>
    /// checks the performance of the average backlight usage
    /// </summary>
    public void Validate()
    {
        lastTimeStamp = DateTimeConverter.ParseRequestDate(scoreData.lastTimeStamp);
        DateTime timeStamp = DateTime.UtcNow;

        //return if score hasn't been updated in the current hour
        if(lastTimeStamp.Date == timeStamp.Date && lastTimeStamp.Hour == timeStamp.Hour -1)
        {
            return;
        }

        List<double> averageScore = scoreData.averageBacklightUsage;

        if(averageScore != null && averageScore.Count > 0)
        {
            double average = Math.Round(averageScore.Average(), 2);

            int amountGoop = 0;
            foreach (var score in averageScore)
            {
                if(score> AverageTreshold)
                {
                    amountGoop++;
                }
            }

            if(average - ChangeInAverageTreshold >= scoreData.lastAverage)
            {
                pollutionManager.CreateGoop(amountGoop);
                sharkManager.SpawnNewShark();
            }

            if(average >= AverageTreshold)
            {
                pollutionManager.CreateGoop(amountGoop);
                sharkManager.SpawnNewShark();
            }

            //scoreData.averageBacklightUsage = new List<double>();
            scoreData.lastAverage = average;
        }
    }
}
