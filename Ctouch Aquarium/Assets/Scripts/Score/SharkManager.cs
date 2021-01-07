using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SharkManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sharkPrefab;
    [SerializeField]
    private int spawnChange = 1;
    [SerializeField]
    private int eatRate = 1;
    [SerializeField]
    private Shark shark;
    
    private GameObject sharkObject;

    public void Start()
    {
        if (shark.isAlive)
        {
            sharkObject = Instantiate(sharkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
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
            sharkObject = Instantiate(sharkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            shark.isAlive = true;
            shark.spawnTime = DateTime.Now;
        }
    }

    /// <summary>
    /// shark eats fish
    /// </summary>
    /// <param name="fish"></param>
    public void EatFish(GameObject[] fish)
    {
        DateTime currentTime = DateTime.Now;
        int duration = (currentTime.Subtract(shark.spawnTime)).Hours;

        if (duration >= shark.hoursAlive + eatRate)
        {
            //add random fish from array
            int r = Random.Range(0, fish.Length - 1);
            shark.fish.Add(fish[r]);

            //todo remove fish from aquarium

            shark.hoursAlive = duration;
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
            Destroy(sharkObject);
        }
    }
}
