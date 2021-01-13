using Persistence;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        public PrototypeShop.Currency currency;
        public bool OrderByName;
        public GameObject target;
        public Spawn spawn;
        public FishPersistence persistence;
    
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
            persistence.AddFish(null, "");
            Close();
            spawn.SpawnFish();
        }
    }
}
