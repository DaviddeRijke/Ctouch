using System;
using FishDataFolder;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Persistence
{
    public class FishSpawner : MonoBehaviour
    {
        public FishPersistence Persistence;
        private static string PathToPrefabFolder = "FishModels";
        public GameObject wrapperPrefab;

        private string[] staticNames = {"Hendrik-Jan", "Freddy", "Hans"};

        public void SpawnFromSaveFile()
        {
            FishData[] fd = Persistence.Load();
            foreach (var f in fd)
            {
                var loadedModel = Resources.Load($"{PathToPrefabFolder}/{f.ModelUID}", typeof(GameObject));
                if(loadedModel == null) continue;
                var wrapper = Instantiate(wrapperPrefab);
                var fish = Instantiate(loadedModel as GameObject, wrapper.transform, false);
                fish.transform.position = f.LocalPosition;
                fish.transform.rotation = f.LocalRotation;
                var fComp = fish.AddComponent<Fish>();
                fComp.fishName = f.Name;
                fish.name = loadedModel.name;
                var fn = fComp.fishName;
                wrapper.name = fn;
                wrapper.transform.position = f.Position;
                wrapper.transform.rotation = f.Rotation;
            }
        }
    }
}