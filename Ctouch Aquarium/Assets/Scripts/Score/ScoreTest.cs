using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    [SerializeField]
    private Score score;

    public void OnClick()
    {
        DateTime lastDate = DateTime.Parse(score.lastTimeStamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
        lastDate = lastDate.AddDays(-1);
        score.lastTimeStamp = lastDate.ToString();
    }
}
