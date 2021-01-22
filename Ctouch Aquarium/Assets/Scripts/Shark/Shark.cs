using UnityEngine;

[CreateAssetMenu(fileName = "Shark", menuName = "SharkData")]
public class Shark : ScriptableObject
{
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
            isAlive = sharkData.isAlive;
            spawnTime = sharkData.spawnTime;
            hoursAlive = sharkData.hoursAlive;
        }

    }
}
