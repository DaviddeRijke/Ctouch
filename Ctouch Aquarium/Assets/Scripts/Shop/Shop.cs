using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Currency currency;
    public bool OrderByName;
    public GameObject target;
    public Spawn spawn;
    
    public void Start()
    {
        GetComponent<BuyableFactory>().Load(Buy);
    }

    public bool IsActive => target.activeSelf;
    
    public void Open()
    {
        target.SetActive(true);
    }

    public void Close()
    {
        target.SetActive(false);
    }

    public void Buy()
    {
        Close();
        spawn.SpawnFish();
    }
}
