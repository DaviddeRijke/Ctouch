﻿using System.Collections.Generic;
using System.IO;
using FishDataFolder;
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
        }

        public void AddFish(FishType ft, string fishName)
        {
            var f = new FishData()
            {
                ModelUID = ft.FishModelUID,
                Name = fishName
            };
            OwnedFish.Add(f);
            OnAddFish.Invoke(f);
        }

        public void SaveUnsavedFishInScene(Fish[] fishes)
        {
            foreach (Fish f in fishes)
            {
                OwnedFish.Add(new FishData(f));
            }
            SaveOwnedFish();
        }

        public void ResetOwnedFish()
        {
            OwnedFish.Clear();
        }

        public void ResetSaveFile()
        {
            if (File.Exists(Application.persistentDataPath + OwnedFishDataFile))
            {
                File.Delete(Application.persistentDataPath + OwnedFishDataFile);
#if UNITY_EDITOR
                Debug.Log("file removed");
                UnityEditor.AssetDatabase.Refresh();
#endif
            }
        }

        public void SaveOwnedFish()
        {
            Debug.Log("Saving " + OwnedFish.Count);
            var container = new ListContainer<FishData>(OwnedFish);
            string json = JsonUtility.ToJson(container);
            File.WriteAllText(Application.persistentDataPath + OwnedFishDataFile, json);
#if UNITY_EDITOR
            Debug.Log(Application.persistentDataPath);
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        
        public FishData[] Load()
        {
            LoadOwnedFish();
            return OwnedFish.ToArray();
        }
        
        public void LoadOwnedFish()
        {
            if (!File.Exists(Application.dataPath + OwnedFishDataFile)) return;
            string json = File.ReadAllText(Application.persistentDataPath + OwnedFishDataFile);
            var container = JsonUtility.FromJson<ListContainer<FishData>>(json);
            OwnedFish = container.dataList;
            Debug.Log("Loaded " + OwnedFish.Count);
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

        #region crud
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
        #endregion
    }
}