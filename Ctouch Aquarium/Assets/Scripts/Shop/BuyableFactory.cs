using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Shop.FishTypePricePairExtensions;

namespace Shop
{
        public class BuyableFactory : MonoBehaviour
        {
                public string PathToFolder = "SO/FishTypes";
                public BuyableUI Prefab;
                public Transform Parent;
                
                public List<BuyableUI> Load()
                {
                        var res2 = LoadFishTypePricePairs();
                        if (res2 == null) return null;
                        var ps = new List<BuyableUI>();
                        foreach (var ftpp in res2)
                        {
                                var p = Instantiate(Prefab, Parent);
                                p.nameText.text = ftpp.ModelUID;
                                p.priceText.text = ftpp.Price.ToString();
                        }
                        return ps;
                }
        }
}