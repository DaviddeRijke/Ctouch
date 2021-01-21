using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private static string FishTypePricePairDataFile = "/availableFish.txt";

        public static void SaveFishTypePricePairs()
        {
            var list = new List<FishTypePricePair>();
            var r = Resources.LoadAll("FishModels", typeof(GameObject));
            Debug.Log($"r: {r.Count()}");
            list.AddRange(r.Select(o => new FishTypePricePair (o.name, 10)));
            var obj = new ListContainer<FishTypePricePair>(list);
            File.WriteAllText(Application.persistentDataPath + FishTypePricePairDataFile, JsonUtility.ToJson(obj));
        }

        public static List<FishTypePricePair> LoadFishTypePricePairs()
        {
            string json;
            if (!File.Exists(Application.persistentDataPath + FishTypePricePairDataFile))
            {
                Debug.Log("using default");
                json = ((TextAsset)Resources.Load("defaultShopFish", typeof(TextAsset))).text;
            }
            else
            {
                json = File.ReadAllText(Application.persistentDataPath + FishTypePricePairDataFile);
            }

            var container = JsonUtility.FromJson<ListContainer<FishTypePricePair>>(json);
            var r = container.dataList;
            Debug.Log("Loaded " + r.Count);
            return container.dataList;
        }
    }
}