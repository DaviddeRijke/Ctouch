using System.Collections.Generic;
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
    }
}