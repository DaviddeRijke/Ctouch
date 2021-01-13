using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreObject
{
    public float score = 0;
    public string lastTimeStamp = "2000-01-01T00:00:00+01:00";

    public double lastAverage = 0;
    public List<double> averageBacklightUsage = new List<double>();

    public ScoreObject(Score scoreData)
    {
        score = scoreData.score;
        lastTimeStamp = scoreData.lastTimeStamp;
        lastAverage = scoreData.lastAverage;
        averageBacklightUsage = scoreData.averageBacklightUsage;
    }
}
