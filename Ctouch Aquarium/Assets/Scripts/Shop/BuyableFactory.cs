using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyableFactory : MonoBehaviour
{
        public string PathToFolder = "SO/FishTypes";
        public Buyable Prefab;
        public Transform Parent;

        public void Load(UnityAction onClick)
        {
                var res = Resources.LoadAll<FishType>(PathToFolder);
                foreach (var fishType in res)
                {
                        var p = Instantiate(Prefab, Parent);
                        p.Type = fishType;
                        p.GetComponentInChildren<Button>().onClick.AddListener(onClick);
                }
        }
}