using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Score", menuName = "ScoreData")]
public class Score : ScriptableObject
{
    public int score = 0;
    public string lastTimeStamp = "01/01/2021 00:00:00 AM";

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
