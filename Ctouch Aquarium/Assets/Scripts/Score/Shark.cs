using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharkData", menuName = "Shark")]
public class Shark : ScriptableObject
{
    //todo
    public List<GameObject> fish = new List<GameObject>();

    public bool isAlive = false;
    public DateTime spawnTime = DateTime.Now;
    public int hoursAlive = 0;
}
