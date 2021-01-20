using System;
using System.Security.Cryptography.X509Certificates;
using FishDataFolder;
using Shop;
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

        public Transform FishParentTransform;
        
        /// <summary>
        /// Let op: fish is nu het containerobject ipv alleen het object met het model! fish wordt dus geregistreerd bij de manager
        /// </summary>
        public void SpawnFromSaveFile()
        {
            FishData[] fd = Persistence.Load();
            foreach (var f in fd)
            {
                var fish = SpawnFishObject(f.ModelUID, f.Name, f.LocalRotation);
                var transform1 = fish.transform;
                transform1.position = f.Position;
                transform1.rotation = f.Rotation;
                
                if(fish.GetComponent<Boid>() == null) Debug.LogWarning("No boid component found on this object!");
                BoidManager.AddObject(fish);
            }
        }

        //overload for when rotationOffset is irrelevant
        private GameObject SpawnFishObject(string ModelUID, string nameOfFish){ return SpawnFishObject(ModelUID, nameOfFish, Quaternion.identity);}
        private GameObject SpawnFishObject(string ModelUID, string nameOfFish, Quaternion rotationOffset)
        {
            var loadedModel = Resources.Load($"{PathToPrefabFolder}/{ModelUID}", typeof(GameObject));
            if (loadedModel == null) return null;
            var boidContainer = Instantiate(BoidPrefab, FishParentTransform);
            var fish = Instantiate(loadedModel as GameObject, boidContainer.transform);
            fish.name = loadedModel.name;
            boidContainer.name = fish.AddComponent<Fish>().fishName = nameOfFish;
            fish.transform.rotation = rotationOffset;

            /*@Kelvin, heb een ifstatement gemaakt die dit skipt als deze elementen niet bestaan.
             Dit is in principe alleen waar voor de niet-vis objecten (mossel, zeester etc)
             maar het maakt debug makkelijker omdat ik zo alle prefabs kan gebruiken.
            */
            var smr = fish.GetComponentInChildren<SkinnedMeshRenderer>();
            var boxCollider = fish.GetComponent<BoxCollider>();
            if (smr == null || boxCollider == null) return boidContainer;
            Bounds bounds = fish.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
            boxCollider.size = new Vector3(bounds.extents.z, bounds.extents.y, bounds.extents.x);

            return boidContainer;
        }

        public void SpawnNewFish(string ModelUID, string nameOfFish, bool direct = false)
        {
            var fish = SpawnFishObject(ModelUID, nameOfFish);
            fish.transform.position = GetSpawnPos();
            if (!direct)
            {
                fish.GetComponentInChildren<Fish>().gameObject.AddComponent<BubbleBehaviour>().StartBehaviour(fish, f => BoidManager.AddObject(f));
            }
            else
            {
                BoidManager.AddObject(fish);
            }
        }

        private Vector3 GetSpawnPos()
        {
            var p = transform.position;
            return new Vector3(p.x + Random.Range(-2f, 2f), p.y, p.z + Random.Range(-1f, 1f));
        }
    }
}