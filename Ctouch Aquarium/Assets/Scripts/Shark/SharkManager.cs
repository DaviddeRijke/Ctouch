using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using FishDataFolder;
using UnityEngine;
using Random = UnityEngine.Random;

public class SharkManager : MonoBehaviour
{
    [SerializeField] private GameObject sharkPrefab;
    [SerializeField] private int spawnChange = 1;
    [SerializeField] private int eatRate = 1;
    [SerializeField] private BoidManager boidManager;

    private Shark shark;
    private GameObject sharkObject;

    public void Start()
    {
        shark = new Shark();
        shark.LoadShark();

        if (shark.isAlive)
        {
            sharkObject = Instantiate(sharkPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            sharkObject.GetComponent<SharkObjectScript>().SetSharkManager(this);
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
            sharkObject = Instantiate(sharkPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            sharkObject.GetComponent<SharkObjectScript>().SetSharkManager(this);
            boidManager.AddObject(sharkObject);
            shark.isAlive = true;
            shark.spawnTime = DateTime.Now.ToString();
            shark.SaveShark();

            FishThoughts.MakeFishThink(FindObjectsOfType<Fish>().ToList(), FishThoughts.SharkArived);
        }
    }

    /// <summary>
    /// shark eats fish
    /// </summary>
    /// <param name="fish"></param>
    public void EatFish(string[] fish)
    {
        DateTime currentTime = DateTime.Now;
        int duration =
            (currentTime.Subtract(DateTime.Parse(shark.spawnTime, null,
                System.Globalization.DateTimeStyles.RoundtripKind))).Hours;

        if (duration >= shark.hoursAlive + eatRate)
        {
            //add random fish from array
            int r = Random.Range(0, fish.Length - 1);
            shark.fish.Add(fish[r]);

            //todo remove fish from aquarium

            shark.hoursAlive = duration;

            shark.SaveShark();
        }
    }

    /// <summary>
    /// action on tapping shark
    /// </summary>
    public void tapOnShark()
    {
        if (shark.fish.Count > 0)
        {
            //todo releaseFish()

            //remove the first fish in the shark
            shark.fish.Remove(shark.fish[0]);
        }
        else
        {
            shark.isAlive = false;
            shark.hoursAlive = 0;
            boidManager.RemoveObject(sharkObject);
            Destroy(sharkObject);

            FishThoughts.MakeFishThink(FindObjectsOfType<Fish>().ToList(), FishThoughts.SharkLeft);
        }

        shark.SaveShark();
    }
}