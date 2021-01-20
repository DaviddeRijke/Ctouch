using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveScore(Score scoreData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreObject score = new ScoreObject(scoreData);

        formatter.Serialize(stream, score);
        stream.Close();
    }

    public static ScoreObject LoadScore()
    {
        string path = Application.persistentDataPath + "/score.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoreObject score = formatter.Deserialize(stream) as ScoreObject;
            stream.Close();

            return score;
        }
        else
        {
            return null;
        }
    }
    public static void SaveShark(Shark shark)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shark.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SharkData sharkData = new SharkData(shark);

        formatter.Serialize(stream, sharkData);
        stream.Close();
    }

    public static SharkData LoadShark()
    {
        string path = Application.persistentDataPath + "/shark.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SharkData sharkData = formatter.Deserialize(stream) as SharkData;
            stream.Close();

            return sharkData;
        }
        else
        {
            return null;
        }
    }
}
