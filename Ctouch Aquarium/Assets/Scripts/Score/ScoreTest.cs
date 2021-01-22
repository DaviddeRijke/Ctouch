using System;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    [SerializeField]
    private Score score;

    public void OnClick()
    {
        DateTime lastDate = DateTimeConverter.ParseRequestDate(score.lastTimeStamp);
        lastDate = lastDate.AddDays(-1);
        score.lastTimeStamp = lastDate.ToString();
    }
}
