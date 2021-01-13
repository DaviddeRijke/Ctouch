using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    //todo
    public List<string> fish = new List<string>();

    public bool isAlive = false;
    public string spawnTime = "2020-01-01T00:00:00+01:00";
    public int hoursAlive = 0;

    public void SaveShark()
    {
        SaveSystem.SaveShark(this);
    }

    public void LoadShark()
    {
        SharkData sharkData = SaveSystem.LoadShark();
        if (sharkData != null)
        {
            fish = sharkData.fish;
            isAlive = sharkData.isAlive;
            spawnTime = sharkData.spawnTime;
            hoursAlive = sharkData.hoursAlive;
        }

    }
}
