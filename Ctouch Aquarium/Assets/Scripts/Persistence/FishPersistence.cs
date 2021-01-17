using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FishData;
using Shop;
using UnityEngine;

namespace Persistence
{
    [CreateAssetMenu(fileName="Persistence")]
    public class FishPersistence : ScriptableObject
    {
        public const string FishTypeDataFile = "/fishTypes.txt";
        public const string OwnedFishDataFile = "/ownedFish.txt";
        public const string PathToFishFolder = "FishTypes";
        
        [SerializeField] private List<FishData> OwnedFish;

        public FishDataEvent OnRemoveFish = new FishDataEvent();
        public FishDataEvent OnAddFish = new FishDataEvent();

        private void Awake()
        {
            OwnedFish = new List<FishData>();
            OwnedFish.Add(new FishData(){ModelUID = "a", Name = "piet"});
            SaveOwnedFish();
            LoadOwnedFish();
        }

        public void AddFish(FishType ft, string name)
        {
            var f = new FishData()
            {
                ModelUID = ft.FishModelUID,
                Name = name
            };
            OwnedFish.Add(f);
            OnAddFish.Invoke(f);
        }

        public void Save(Fish[] fishes)
        {
            
        }

        public void Reset()
        {
        }

        public void SaveOwnedFish()
        {
            Debug.Log("Saving " + OwnedFish.Count);
            var container = new ListContainer(OwnedFish);
            string json = JsonUtility.ToJson(container);
            File.WriteAllText(Application.persistentDataPath + OwnedFishDataFile, json);
        }
        
        public void LoadOwnedFish()
        {
            if (File.Exists(Application.dataPath + OwnedFishDataFile))
            {
                string json = File.ReadAllText(Application.persistentDataPath + OwnedFishDataFile);
                var container = JsonUtility.FromJson<ListContainer>(json);
                OwnedFish = container.dataList;
                Debug.Log("Loaded " + OwnedFish.Count);
            }
            else
            {
                Debug.Log("ownedFish file not found");
            }
        }

        //removes first occurence of fish with given name
        public bool RemoveFishByName(string name)
        {
            var match = OwnedFish.Find(f => f.Name == name);
            if (OwnedFish.Contains(match))
            {
                OnRemoveFish.Invoke(match);
                OwnedFish.Remove(match);
                return true;
            }
            return false;
        }

        //removes first occurence of fish with given model
        public bool RemoveFishByModel(string model)
        {
            var match = OwnedFish.Find(f => f.ModelUID == model);
            if (OwnedFish.Contains(match))
            {
                OnRemoveFish.Invoke(match);
                OwnedFish.Remove(match);
                return true;
            }
            return false;
        }

        // public void SaveFishTypes()
        // {
        //     List<FishType> fishTypes = Resources.LoadAll<FishType>(PathToFishFolder).ToList();
        //     Debug.Log("Saving " + fishTypes.Count);
        //     string json = JsonUtility.ToJson(fishTypes);
        //     string json2 = JsonUtility.ToJson(fishTypes.ToArray());
        //     Debug.Log(json);
        //     Debug.Log(json2);
        //     File.WriteAllText(Application.dataPath + FishTypeDataFile, json);
        // }
        //
        // public void LoadFishTypes()
        // {
        //     if (File.Exists(Application.dataPath + FishTypeDataFile))
        //     {
        //         string json = File.ReadAllText(Application.dataPath + FishTypeDataFile);
        //         Debug .Log(json);
        //         var fishTypes = JsonUtility.FromJson<List<FishType>>(json);
        //         Debug.Log("Loading " + fishTypes.Count);
        //     }
        //     else
        //     {
        //         Debug.Log("fishType file not found");
        //     }
        //}
    }
}