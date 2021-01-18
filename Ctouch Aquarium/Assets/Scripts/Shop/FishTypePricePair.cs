using System.Collections.Generic;
using System.IO;
using Persistence;
using UnityEngine;

namespace Shop
{
    public struct FishTypePricePair
    {
        public string ModelUID;
        public int Price;
    }

    public static class FishTypePricePairExtensions
    {
        public static string FishTypePricePairDataFile;
        
        public static void SaveFishTypePricePairs(List<FishTypePricePair> ftpps)
        {
            var container = new ListContainer<FishTypePricePair>(ftpps);
            string json = JsonUtility.ToJson(container);
            File.WriteAllText(Application.persistentDataPath + FishTypePricePairDataFile, json);
#if UNITY_EDITOR
            Debug.Log(Application.persistentDataPath);
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        public static List<FishTypePricePair> LoadFishTypePricePairs()
        {
            if (!File.Exists(Application.dataPath + FishTypePricePairDataFile)) return null;
            string json = File.ReadAllText(Application.persistentDataPath + FishTypePricePairDataFile);
            var container = JsonUtility.FromJson<ListContainer<FishTypePricePair>>(json);
            return container.dataList;
        }
    }
}