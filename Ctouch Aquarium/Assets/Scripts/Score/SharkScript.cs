using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkScript : MonoBehaviour
{
    [SerializeField]
    private Shark shark;
    [SerializeField]
    private int eatRate = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// shark eats fish
    /// </summary>
    /// <param name="fish"></param>
    public void EatFish(GameObject fish)
    {
        DateTime currentTime = DateTime.Now;
        int duration = (currentTime.Subtract(shark.spawnTime)).Hours;

        if (duration >= shark.hoursAlive + eatRate)
        {
            shark.fish.Add(fish);

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
    }
}
