using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScoreData")]
public class Score : ScriptableObject
{
    public float score = 0;
    public string lastTimeStamp = "2020-01-01T00:00:00+01:00";

    public double lastAverage = 0;
    public List<double> averageBacklightUsage = new List<double>();

    public void SaveScore()
    {
        SaveSystem.SaveScore(this);
    }

    public void LoadScore()
    {
        ScoreObject score = SaveSystem.LoadScore();
        if (score != null)
        {
            this.score = score.score;
            lastTimeStamp = score.lastTimeStamp;
            lastAverage = score.lastAverage;
            averageBacklightUsage = score.averageBacklightUsage;
        }

    }
}
