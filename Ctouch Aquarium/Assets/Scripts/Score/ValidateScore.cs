using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ValidateScore : MonoBehaviour
{
    [SerializeField]
    private ScoreData scoreData;
    [SerializeField]
    private int AverageTreshold = 60;
    [SerializeField]
    private int ChangeInAverageTreshold = 20;
    [SerializeField]
    private SharkManager sharkManager;

    private DateTime lastTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        lastTimeStamp = DateTime.Parse(scoreData.lastTimeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);

        //check performance every hour;
        InvokeRepeating("Validate", 0f, 3600f);
    }

    /// <summary>
    /// checks the performance of the average backlight usage
    /// </summary>
    public void Validate()
    {
        //return if score hasn't been updated in the current hour
        DateTime timeStamp = DateTime.Now;
        if(lastTimeStamp.Date == timeStamp.Date && lastTimeStamp.Hour == timeStamp.Hour)
        {
            return;
        }

        List<double> averageScore = scoreData.averageBacklightUsage;

        if(averageScore != null && averageScore.Count > 0)
        {
            double average = Math.Round(averageScore.Average(), 2);

            if(average - ChangeInAverageTreshold >= scoreData.lastAverage)
            {
                //spawngoo
                sharkManager.SpawnNewShark();
            }

            if(average >= AverageTreshold)
            {
                //spawngoo
                sharkManager.SpawnNewShark();
            }

            scoreData.lastAverage = average;
        }
    }
}
