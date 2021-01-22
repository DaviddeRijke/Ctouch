﻿using System;
using FishDataFolder;
using Shop;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Persistence
{
    public class FishSpawner : MonoBehaviour
    {
        public bool LoadOnAwake = true;
        public FishPersistence Persistence;
        private static string PathToPrefabFolder = "FishModels";
        public GameObject BoidPrefab;
        public BoidManager BoidManager;

        public Inventory Inventory;
        
        public Transform FishParentTransform;
        public List<Fish> fishObjects = new List<Fish>();

        public List<Fish> GetFishObjects() => fishObjects;

        public void Awake()
        {
            if(LoadOnAwake) SpawnFromSaveFile();
        }

        /// <summary>
        /// Let op: fish is nu het containerobject ipv alleen het object met het model! fish wordt dus geregistreerd bij de manager
        /// </summary>
        public void SpawnFromSaveFile()
        {
            FishData[] fd = Persistence.Load();
            foreach (var f in fd)
            {
                var fish = SpawnFishObject(f.ModelUID, f.Name, f.Rotation);
                var transform1 = fish.transform;
                Debug.Log(f.Position);
                transform1.position = f.Position;
                transform1.rotation = f.Rotation;
                var fishComponent = fish.GetComponentInChildren<Fish>();
                fishComponent.isEaten = f.IsEaten;

                if (fish.GetComponent<Boid>() == null) Debug.LogWarning("No boid component found on this object!");
                BoidManager.AddObject(fish);

                if (f.IsEaten)
                {
                    fish.transform.gameObject.SetActive(false);
                }
                if (f.IsInInventory)
                {
                    Inventory.AddToInventory(fish);
                }
            }
        }

        //overload for when rotationOffset is irrelevant
        private GameObject SpawnFishObject(string ModelUID, string nameOfFish) { return SpawnFishObject(ModelUID, nameOfFish, Quaternion.identity); }
        private GameObject SpawnFishObject(string ModelUID, string nameOfFish, Quaternion rotationOffset)
        {
            var loadedModel = Resources.Load($"{PathToPrefabFolder}/{ModelUID}", typeof(GameObject));
            if (loadedModel == null) return null;
            var boidContainer = Instantiate(BoidPrefab, transform);
            var fish = Instantiate(loadedModel as GameObject, boidContainer.transform);
            nameOfFish = fish.GetComponent<FishName>().fishName;
            var f = fish.AddComponent<Fish>();
            boidContainer.name = f.fishName = nameOfFish;
            fish.tag = "Fish";

            fishObjects.Add(fish.GetComponent<Fish>());

            /*@Kelvin, heb een ifstatement gemaakt die dit skipt als deze elementen niet bestaan.
             Dit is in principe alleen waar voor de niet-vis objecten (mossel, zeester etc)
             maar het maakt debug makkelijker omdat ik zo alle prefabs kan gebruiken.
            */
            var smr = fish.GetComponentInChildren<SkinnedMeshRenderer>();
            var boxCollider = fish.GetComponent<BoxCollider>();
            if (smr == null || boxCollider == null) return boidContainer;
            Bounds bounds = smr.localBounds;
            boxCollider.size = new Vector3(bounds.extents.y, bounds.extents.x, bounds.extents.z);

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
            fish.transform.Rotate(0, 90, 0);
        }

        private Vector3 GetSpawnPos()
        {
            var p = FishParentTransform.position;
            return new Vector3(p.x + Random.Range(-2f, 2f), p.y, p.z + Random.Range(-1f, 1f));
        }
    }
}