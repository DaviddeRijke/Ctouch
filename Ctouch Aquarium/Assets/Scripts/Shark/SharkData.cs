using Persistence;
using System.Collections.Generic;

[System.Serializable]
public class SharkData
{
    public bool isAlive = false;
    public string spawnTime = "2020-01-01T00:00:00+01:00";
    public int hoursAlive = 0;

    public SharkData(Shark shark)
    {
        isAlive = shark.isAlive;
        spawnTime = shark.spawnTime;
        hoursAlive = shark.hoursAlive;
    }
}
