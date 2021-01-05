using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : ScriptableObject
{
    //todo
    public List<GameObject> fish = new List<GameObject>();

    public DateTime spawnTime = DateTime.Now;
    public int hoursAlive = 0;
}
