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
        public GameObject BoidPrefab;

        public BoidManager BoidManager;

        public void SpawnFromSaveFile()
        {
            FishData[] fd = Persistence.Load();
            foreach (var f in fd)
            {
                var loadedModel = Resources.Load($"{PathToPrefabFolder}/{f.ModelUID}", typeof(GameObject));
                if(loadedModel == null) continue;
                var boidContainer = Instantiate(BoidPrefab);
                var fish = Instantiate(loadedModel as GameObject, boidContainer.transform);
                //fish.transform.position = f.LocalPosition;
                fish.transform.rotation = f.LocalRotation;
                var fComp = fish.AddComponent<Fish>();
                fComp.fishName = f.Name;
                fish.name = loadedModel.name;
                var fn = fComp.fishName;
                boidContainer.name = fn;
                var transform1 = boidContainer.transform;
                transform1.position = f.Position;
                transform1.rotation = f.Rotation;
                BoidManager.AddObject(boidContainer);

                Bounds bounds = fish.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
                BoxCollider boxCollider = fish.GetComponent<BoxCollider>();
                boxCollider.size = new Vector3(bounds.extents.z, bounds.extents.y, bounds.extents.x);
            }
        }
    }
}