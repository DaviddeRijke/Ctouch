using System.Collections.Generic;
using System.IO;
using FishDataFolder;
using UnityEngine;

namespace Persistence
{
    [CreateAssetMenu(fileName="Persistence")]
    public class FishPersistence : ScriptableObject
    {
        public const string OwnedFishDataFile = "/ownedFish.txt";
        
        // public void SaveUnsavedFishInScene(Fish[] fishes)
        // {
        //     if(OwnedFish == null) OwnedFish = new List<FishData>();
        //     foreach (Fish f in fishes)
        //     {
        //         OwnedFish.Add(new FishData(f));
        //     }
        //     SaveOwnedFish();
        // }
        #region legacy
//
//         public void ResetOwnedFish()
//         {
//             OwnedFish.Clear();
//         }
//
//         public void ResetSaveFile()
//         {
//             if (File.Exists(Application.persistentDataPath + OwnedFishDataFile))
//             {
//                 File.Delete(Application.persistentDataPath + OwnedFishDataFile);
// #if UNITY_EDITOR
//                 Debug.Log("file removed");
//                 UnityEditor.AssetDatabase.Refresh();
// #endif
//             }
//             ResetOwnedFish();
//         }
#endregion

        public void SaveFishes(List<Fish> f)
        {
            var data = new List<FishData>();
            foreach (Fish fish in f)
            {
                data.Add(new FishData(fish));
                var container = new ListContainer<FishData>(data);
                string json = JsonUtility.ToJson(container);
                File.WriteAllText(Application.persistentDataPath + OwnedFishDataFile, json);
            }
        }

        public FishData[] Load()
        {
            if (!File.Exists(Application.persistentDataPath + OwnedFishDataFile))
            {
                Debug.Log("it didnt");
                return new FishData[0];
            }
            string json = File.ReadAllText(Application.persistentDataPath + OwnedFishDataFile);
            var container = JsonUtility.FromJson<ListContainer<FishData>>(json);
            return container.dataList.ToArray();
        }
    }
}