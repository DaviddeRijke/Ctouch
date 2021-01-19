using System;
using System.Collections.Generic;
using System.IO;
using Persistence;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public struct FishTypePricePair
    {
        public string ModelUID;
        public int Price;

        public FishTypePricePair(string id, int price)
        {
            ModelUID = id;
            Price = price;
        }
    }

    public static class FishTypePricePairExtensions
    {
        public static string FishTypePricePairDataFile = "/availableFish.txt";
        
        public static List<FishTypePricePair> LoadFishTypePricePairs()
        {
            if (!File.Exists(Application.persistentDataPath + FishTypePricePairDataFile))
            {
                Debug.Log("not found");
                return null;
            }
            string json = File.ReadAllText(Application.persistentDataPath + FishTypePricePairDataFile);
            var container = JsonUtility.FromJson<ListContainer<FishTypePricePair>>(json);
            var r = container.dataList;
            Debug.Log("Loaded " + r.Count);
            return container.dataList;
        }
    }
}