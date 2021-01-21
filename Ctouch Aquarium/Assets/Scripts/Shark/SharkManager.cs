using System;
using System.Collections.Generic;
using System.Linq;
using FishDataFolder;
using Persistence;
using UnityEngine;
using Random = UnityEngine.Random;

public class SharkManager : MonoBehaviour
{
    [SerializeField] private GameObject sharkPrefab;
    [SerializeField] private int spawnChange = 1;
    [SerializeField] private int eatRate = 1;
    [SerializeField] private BoidManager boidManager;
    [SerializeField] private FishPersistence fishPersistence;
    [SerializeField] private FishSpawner fishSpawner;

    [SerializeField] private Shark shark;
    private GameObject sharkObject;
    private List<Fish> notEatenFishDataList = new List<Fish>();
    private List<Fish> fishObjects = new List<Fish>();

    public void Start()
    {
        shark.LoadShark();
        fishObjects = fishSpawner.fishObjects;

        if (shark.isAlive)
        {
            sharkObject = Instantiate(sharkPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            sharkObject.GetComponent<SharkObjectScript>().SetSharkManager(this);

            UpdateShark();
        }
        else
        {
            notEatenFishDataList = fishObjects;
        }
    }

    public void UpdateShark()
    {
        fishObjects = fishSpawner.fishObjects;

        foreach (var fish in fishObjects)
        {
            if (!fish.GetComponent<Fish>().isEaten)
            {
                notEatenFishDataList.Add(fish);
            }
        }
    }

    /// <summary>
    /// tap on shark
    /// </summary>
    private void OnMouseDown()
    {
        tapOnShark();
    }

    /// <summary>
    /// Spawn a shark in the aquarium
    /// </summary>
    public void SpawnNewShark()
    {
        //return if shark already spawned
        if (shark.isAlive)
        {
            return;
        }

        //random change to spawn a shark
        if (Random.Range(0, spawnChange) == 0)
        {
            UpdateShark();
            sharkObject = Instantiate(sharkPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            sharkObject.GetComponent<SharkObjectScript>().SetSharkManager(this);
            boidManager.AddObject(sharkObject);
            shark.isAlive = true;
            shark.spawnTime = DateTime.Now.ToString();
            fishObjects = fishSpawner.fishObjects;
            shark.SaveShark();

            FishThoughts.MakeFishThink(FindObjectsOfType<Fish>().ToList(), ThoughtEnum.SharkArived);
        }
    }

    /// <summary>
    /// shark eats fish
    /// </summary>
    public void EatFish()
    {
        UpdateShark();
        DateTime currentTime = DateTime.Now;
        DateTime spawnTime = DateTime.Parse(shark.spawnTime, null,
                System.Globalization.DateTimeStyles.RoundtripKind);
        int duration = (int)(currentTime.Subtract(spawnTime)).TotalHours;
        int fishEatenCount = duration / eatRate;

        if (fishEatenCount > notEatenFishDataList.Count)
        {
            fishEatenCount = notEatenFishDataList.Count;
        }

        if (notEatenFishDataList.Count > 0)
        {
            for (int i = 0; i < fishEatenCount; i++)
            {
                //add random fish from array
                int r = Random.Range(0, notEatenFishDataList.Count);
                Fish fish = notEatenFishDataList[r];

                //add to shark
                shark.fish.Add(new SharkEatenData(fish.fishName, fish.name));

                //remove from eaten list
                notEatenFishDataList.RemoveAt(r);
                fish.GetComponent<Fish>().isEaten = true;

                //remove fish from aquarium
                boidManager.RemoveObject(fish.transform.parent.gameObject);
                fishSpawner.fishObjects.Remove(fish);
                Destroy(fish.transform.parent.gameObject);

            }
        }

        shark.hoursAlive = duration;

        shark.SaveShark();
    }

    /// <summary>
    /// action on tapping shark
    /// </summary>
    public void tapOnShark()
    {
        if (shark.fish.Count > 0)
        {
            //remove the first fish in the shark
            SharkEatenData eatenFish = shark.fish[0];

            //spawn fish in aquarium
            GameObject fish = fishSpawner.SpawnFishObject(eatenFish.modelName, eatenFish.name);
            boidManager.AddObject(fish);
            fish.GetComponentInChildren<Fish>().isEaten = false;
            shark.fish.RemoveAt(0);
        }
        else
        {
            shark.isAlive = false;
            shark.hoursAlive = 0;
            boidManager.RemoveObject(sharkObject);
            Destroy(sharkObject);

            FishThoughts.MakeFishThink(FindObjectsOfType<Fish>().ToList(), ThoughtEnum.SharkLeft);
        }

        shark.SaveShark();
    }
}