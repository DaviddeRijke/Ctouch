using System.Collections.Generic;
using Persistence;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        public Score currency;
        public bool OrderByName;
        public GameObject target;
        public Spawn spawn;
        public FishPersistence persistence;
        public UnityEvent OnBuy;
        private List<Buyable> buyables;
        
        public void Start()
        {
            buyables = GetComponent<BuyableFactory>().Load();
            foreach (var buyable in buyables)
            {
                var btn = buyable.GetComponentInChildren<Button>();
                btn.onClick.AddListener(() => BuyFish(buyable));
            }
        }

        public bool IsActive => target.activeSelf;
    
        public void Open()
        {
            target.SetActive(true);
            RefreshAvailability();

        }

        private void RefreshAvailability()
        {
            buyables.ForEach(b => b.GetComponentInChildren<Button>().interactable = b.Type.price <= currency.score);
        }

        public void Close()
        {
            target.SetActive(false);
        }

        public bool BuyFish(Buyable type)
        {
            if (currency.score < type.Type.price) return false;
            currency.score -= type.Type.price;
            OnBuy.Invoke();
            spawn.SpawnFish();
            RefreshAvailability();
            return true;
        }

        public void Buy()
        {
            persistence.AddFish(null, "");
            Close();
            spawn.SpawnFish();
        }
    }
}
