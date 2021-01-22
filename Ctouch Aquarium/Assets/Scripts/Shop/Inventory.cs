using System;
using UnityEngine;
using FishDataFolder;

namespace Shop
{
    public class Inventory : MonoBehaviour
    {
        public AquariumClicker Clicker;
        public Transform Container;
        public InventoryItem Prefab;
        public BoidManager Boids;
        
        private void Awake()
        {
            Clicker.OnRemove.AddListener(f=>AddToInventory(f.transform.parent.gameObject));
        }

        public void AddToInventory(GameObject go)
        {
            Boids.RemoveObject(go);
            go.SetActive(false);
            var f = go.GetComponentInChildren<Fish>();
            f.isInInventory = true;
            var ii = Instantiate(Prefab, Container);
            ii.f = f;
            ii.OnClick += PlaceBack;
        }

        public void PlaceBack(Fish f, GameObject g)
        {
            f.isInInventory = false;
            var p = f.transform.parent.gameObject;
            p.SetActive(true);
            GetComponent<Shop>().Close();
            Boids.AddObject(p.gameObject);
            Destroy(g.gameObject);
        }
    }
}